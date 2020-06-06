using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SVU.Database.IService;
using SVU.Logging.IServices;
using SVU.Web.UI.Controllers.Base;
using SVU.Web.UI.Extensions;
using SVU.Web.UI.Models;
using SVU.Web.UI.Models.Homework;
using SVU.Web.UI.Static;
using SVU.Web.UI.ViewModels;
using SVU.Web.UI.ViewModels.Account;
using SVU.Web.UI.ViewModels.Health;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SVU.Web.UI.Controllers
{

    /// <summary>
    /// The controller to execute the homeworks 
    /// </summary>
    public class HomeworkController : BaseController
    {
        #region Properties
        public IDataSetDatabaseService DataSetDatabaseService { get; private set; }
        public IMemoryCache MemoryCache { get; private set; }
        public IHealthAccountService HealthAccountService { get; private set; }

        /// <summary>
        /// Constent keys
        /// </summary>
        public static readonly string AVG_KEY = "Average";
        public static readonly string STD_KEY = "StandardDeviation";
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public HomeworkController(IDataSetDatabaseService dataSetDatabaseService, ILoggingService loggingService, IMemoryCache memoryCache, IHealthAccountService healthAccountService)
            : base(loggingService)
        {
            DataSetDatabaseService = dataSetDatabaseService;
            MemoryCache = memoryCache;
            HealthAccountService = healthAccountService;
        }
        #endregion

        #region GET Requests

        /// <summary>
        /// The adm course home work for classification using ID3 and bayes
        /// </summary>
        /// <param name="id">The id (name) of the homework</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ADM(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                switch (id.ToLower())
                {
                    case "tennis":
                        return View(id, new HomeworkTennisViewModel
                        {
                            TennisRecords = await DataSetDatabaseService.GetTennisRecords()
                        });
                    case "heartdisease":
                        return View(id, new HomeworkHeartDiseaseViewModel
                        {
                            HeartDiseasesRecords = await DataSetDatabaseService.GetHeartDiseaseRecords()
                        });

                    default:
                        break;
                }
            }
            return View(StaticViewNames.NOTFOUND);
        }
        /// <summary>
        /// Gets the health AWP homwork view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> AWP()
        {
            var viewModel = new HomeworkAWPAccountViewModel();
            //Check if the user is authenitcated
            if (User.Identity.IsAuthenticated)
            {
                //Try to parse the user id
                if (Guid.TryParse(User.GetClaimValue(ClaimTypes.NameIdentifier), out Guid id))
                {
                    //Get the hole user information
                    var user = await HealthAccountService.GetUser(id);
                    //Fill up the values for the view model
                    viewModel.UserViewModel = new HealthUserViewModel()
                    {
                        DOB = user.DOB,
                        Email = user.Email,
                        Gender = user.Gender,
                        Id = user.Id,
                        MedicalHistory = user.MedicalHistory,
                        PhoneNumber = user.PhoneNumber,
                        Username = user.Username,
                    };
                }
            }
            return View(StaticViewNames.AWP_HEALTH, viewModel);
        }
        #endregion

        #region POST Requests
        /// <summary>
        /// Dose the calculation for the sent alog name as the id paramter
        /// </summary>
        /// <param name="id">The name of the alog to execute</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CalculateBayes([FromBody] CalculateTargetModel model)
        {
            //Check if the data is send correctly
            if (ModelState.IsValid)
            {
                try
                {
                    //The dictionaty to hold the calvulation for each column
                    //      dic => columnNam => (result,errorRate)
                    //NOTE: All the calcuations are done in the controller side just for easy look 
                    //  a better approach would be taking them into a service side interface
                    var calcuationsCache = await MemoryCache.GetOrCreate(string.Join('-', "Bayes", model.DbSet, model.Target, model.IgnoreProperties), async options =>
                      {
                          //Get the records from the db
                          var dbSet = await DataSetDatabaseService.GetDbSet(model.DbSet);
                          var calcuations = new Dictionary<string, Dictionary<string, float>>
                          {
                            //Add the target as it is a spical value
                            { model.Target, new Dictionary<string, float>() }
                          };
                          try
                          {
                              //try to get the target values based on the name of the property that is provided in the target
                              var targetValues = dbSet.Select(item => item.GetType().GetProperty(model.Target).GetValue(item).ToString());

                              //Get the distinct values
                              var targetDistinctValues = targetValues.Distinct();

                              //Get the count of each occurrence of the target value
                              var targetCount = new Dictionary<string, int>();
                              foreach (var target in targetDistinctValues)
                              {
                                  if (!targetCount.ContainsKey(target.ToString()))
                                  {
                                      targetCount.Add(target.ToString(), targetValues.Where(t => t.ToString() == target.ToString()).Count());
                                  }
                                  if (!calcuations[model.Target].ContainsKey(target))
                                  {
                                      calcuations[model.Target].Add(target, targetCount[target] / (float)targetValues.Count());
                                  }
                              }
                              //Loop throught the items
                              foreach (var item in dbSet)
                              {
                                  //Get there properties
                                  foreach (var property in item.GetType().GetProperties())
                                  {
                                      //Do not do any calcuations on any ignored properties
                                      if (!model.IgnoreProperties.Contains(property.Name))
                                      {
                                          //Check if the property is a string value or an int due to change of calcualtions
                                          if (property.PropertyType == typeof(string))
                                          {
                                              //Get the value of the property from teh main object
                                              var propValue = property.GetValue(item).ToString();

                                              //Get the calcuations for each target
                                              foreach (var target in targetDistinctValues)
                                              {
                                                  var keyName = $"{property.Name}-{propValue}";
                                                  //Check if we did the calcualtions for that value
                                                  if (!calcuations.ContainsKey(keyName))
                                                  {
                                                      //Add the new item
                                                      calcuations.Add(keyName, new Dictionary<string, float>());
                                                  }
                                                  if (!calcuations[keyName].ContainsKey(target))
                                                  {
                                                      //Do the calculations
                                                      calcuations[keyName].Add(target,
                                                          (dbSet.Where(obj =>
                                                          obj.GetType().GetProperty(model.Target).GetValue(obj).ToString() == target &&
                                                         obj.GetType().GetProperty(property.Name).GetValue(obj).ToString() == propValue
                                                        ).Count() + 1) / (float)targetCount[target]);

                                                  }
                                              }
                                          }
                                          //Else we need to find the avg and the STD (standard deviation)
                                          else
                                          {
                                              //Get the number value of hte property
                                              if (float.TryParse(property.GetValue(item).ToString(), out float propValue))
                                              {
                                                  //Get the calcuations for each target
                                                  foreach (var target in targetDistinctValues)
                                                  {
                                                      var keyName = $"{property.Name}-{target}";
                                                      //Check if we did the calcualtions for that value
                                                      if (!calcuations.ContainsKey(keyName))
                                                      {
                                                          Console.WriteLine(keyName);
                                                          //Add the new item
                                                          calcuations.Add(keyName, new Dictionary<string, float>());
                                                      }

                                                      if (!calcuations[keyName].ContainsKey(AVG_KEY) && !calcuations[keyName].ContainsKey(STD_KEY))
                                                      {

                                                          //Get the points where the target is met
                                                          var points = dbSet.Where(obj =>
                                                       obj.GetType().GetProperty(model.Target).GetValue(obj).ToString() == target
                                                      ).Select(obj => obj.GetType().GetProperty(property.Name).GetValue(obj).ToString());
                                                          //The final results that we need
                                                          double mean = 0;
                                                          double std = 0;
                                                          //Loop through the points and get there number value
                                                          foreach (var point in points)
                                                          {
                                                              if (float.TryParse(point, out float pointValue))
                                                              {
                                                                  mean += pointValue;
                                                              }
                                                          }

                                                          //get the avg
                                                          mean /= points.Count();
                                                          //calcualte the std
                                                          foreach (var point in points)
                                                          {
                                                              if (float.TryParse(point, out float pointValue))
                                                              {
                                                                  std += Math.Pow(pointValue - mean, 2);
                                                              }
                                                          }
                                                          std = Math.Sqrt(std / (points.Count() - 1));

                                                          calcuations[keyName].Add(AVG_KEY, (float)mean);
                                                          calcuations[keyName].Add(STD_KEY, (float)std);
                                                      }
                                                  }
                                              }
                                          }
                                      }
                                  }
                              }
                          }
                          catch (System.Exception ex)
                          {
                              LoggingService.LogException(ex);
                              //Clear it right away
                              options.AbsoluteExpiration = DateTimeOffset.UtcNow.AddMilliseconds(1);
                              return null;
                          }
                          //Keep it in cache for one week only
                          options.AbsoluteExpiration = DateTimeOffset.UtcNow.AddDays(7);
                          return calcuations;
                      });

                    if (calcuationsCache == null)
                    {
                        return InternalServerError();
                    }
                    //Now go throught the properties of the testing example
                    var results = new Dictionary<string, double>();
                    foreach (var property in model.TestExample.Properties())
                    {
                        //Check if the property is not form the ignored ones
                        if (!model.IgnoreProperties.Contains(property.Name))
                        {
                            foreach (var target in calcuationsCache[model.Target])
                            {
                                if (!results.ContainsKey(target.Key))
                                {
                                    //Add the starting result value and defualt to the ratio for each target value
                                    results.Add(target.Key, target.Value);
                                }
                                //Check if the value is an number
                                if (float.TryParse(property.Value.ToString(), out float propIntValue))
                                {
                                    if (calcuationsCache.TryGetValue($"{property.Name}-{target.Key}", out Dictionary<string, float> targetResult))
                                    {
                                        results[target.Key] *= CalculateNormalDistribution(targetResult[AVG_KEY], targetResult[STD_KEY], propIntValue);
                                    }
                                }
                                else
                                {
                                    if (calcuationsCache.TryGetValue($"{property.Name}-{property.Value}", out Dictionary<string, float> targetResult))
                                    {
                                        if (targetResult.TryGetValue(target.Key, out float probability))
                                        {
                                            results[target.Key] *= probability;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    var avgResult = results.Values.Sum();
                    //Calcuate the percentage
                    foreach (var key in results.Keys.ToList())
                    {
                        //Get the percentage of the results
                        results[key] /= avgResult;

                    }
                    return Ok(new
                    {
                        results,
                        calcuationsCache
                    });
                }
                catch (Exception ex)
                {
                    LoggingService.LogException(ex);
                    return InternalServerError(ex.GetBaseException().Message);
                }
            }
            else
            {
                return BadRequest("Some data were not provided plase check and try again");
            }
        }

        public async Task<IActionResult> CalculateID3([FromBody] CalculateTargetModel model)
        {
            if (ModelState.IsValid)
            {
                //Check that we got any data
                var id3Tree = await MemoryCache.GetOrCreate(string.Join('-', "ID3", model.DbSet, model.Target, model.IgnoreProperties), async options =>
                {
                    //Get the records from the db
                    var dbSet = await DataSetDatabaseService.GetDbSet(model.DbSet);
                    try
                    {
                        //Keep it in cache for one week only
                        options.AbsoluteExpiration = DateTimeOffset.UtcNow.AddDays(7);
                        return CreateID3Tree(dbSet, new List<string>(), model.IgnoreProperties, dbSet.First().GetType().GetProperty(model.Target));
                    }
                    catch (Exception ex)
                    {
                        LoggingService.LogException(ex);
                        //Clear it right away
                        options.AbsoluteExpiration = DateTimeOffset.UtcNow.AddMilliseconds(1);
                        return null;
                    }
                });
                var currentNode = id3Tree;
                //While we still have nodes
                while (currentNode != null && !currentNode.IsLeaf)
                {
                    //Get the value fo the property
                    var value = model.TestExample.GetValue(currentNode.PropertyInfo.Name);

                    currentNode = currentNode.Next.SingleOrDefault(item => item.CheckCondition(value));
                }

                return Ok(new
                {
                    results = currentNode.Value,
                    id3Tree
                });
            }
            else
            {
                return BadRequest("Some data were not provided plase check and try again");
            }
        }

        /// <summary>
        /// Dose the calculation for the sent alog name as the id paramter
        /// </summary>
        /// <param name="id">The name of the alog to execute</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ADM(string id, [FromBody] HeartDiseasePatientModel model)
        {
            //Check if the data is send correctly
            if (ModelState.IsValid)
            {
                return View(new HomeworkHeartDiseaseViewModel
                {
                    HeartDiseasesRecords = await DataSetDatabaseService.GetHeartDiseaseRecords()
                });
            }
            else
            {
                return BadRequest("Some data were not provided plase check and try again");
            }
        }

        #endregion


        #region Helpers


        /// <summary>
        /// Calcuialtes the normal distribution of a point
        /// </summary>
        /// <param name="avg"></param>
        /// <param name="std"></param>
        /// <param name="value">the value to calculate the distribution for</param>
        /// <returns></returns>
        private double CalculateNormalDistribution(float avg, float std, float value)
        {
            return (1 / (Math.Sqrt(2 * Math.PI) * std) * Math.Exp(-(Math.Pow((value - avg), 2)) / (2 * Math.Pow(std, 2))));
        }

        /// <summary>
        /// Calculates the probiblity of each target
        /// </summary>
        /// <param name="targetArray"></param>
        /// <returns></returns>
        private Dictionary<string, double> CalculateProbiblties(IEnumerable<string> targetArray)
        {
            //The array to store the probiblity of each target 
            var targetProbabilty = new Dictionary<string, double>();
            //Get the distinct values in the target array
            foreach (var item in targetArray.Distinct())
            {
                targetProbabilty.Add(item, (targetArray.Count(t => t == item) / (1.0 * targetArray.Count())));
            }

            return targetProbabilty;
        }

        /// <summary>
        /// Calcuatels the entropy for the targets
        /// </summary>
        /// <param name="attributeTarget"></param>
        /// <returns></returns>
        private double CalcutlateEntropy(IEnumerable<KeyValuePair<string, string>> attributeTarget)
        {
            var targetEntropy = 0.0;
            //Get the probiblites for the target
            var probiblities = CalculateProbiblties(attributeTarget.Select(item => item.Value));
            //Loop at each target
            foreach (var item in probiblities)
            {
                targetEntropy -= (item.Value * Math.Log(item.Value, 2));
            }
            return targetEntropy;
        }

        /// <summary>
        /// Calcuates the gain for an attribute for the targets sent
        /// </summary>
        /// <param name="attributeTarget"></param>
        /// <returns></returns>
        private double CalcuateGain(IEnumerable<KeyValuePair<string, string>> attributeTarget)
        {
            //find the distinct values for the attribute
            var distinctValues = attributeTarget.Select(item => item.Key).Distinct();
            //Get the total entropy for the hole dataset
            var totalEntropy = CalcutlateEntropy(attributeTarget);
            //Start the gain as its
            var gain = totalEntropy;
            //Calculate the entropy (the disorder of values for the target) for each value
            foreach (var item in distinctValues)
            {
                var currentAttribute = attributeTarget.Where(att => att.Key == item);
                gain -= (currentAttribute.Count() / ((1.0) * attributeTarget.Count())) * CalcutlateEntropy(currentAttribute);
            }
            return gain;
        }
        /// <summary>
        /// Finds the best attribute to branch on for a given dataset
        /// </summary>
        /// <param name="dbset"></param>
        /// <param name="roots"></param>
        /// <param name="ignoredProperties"></param>
        /// <returns></returns>
        private PropertyInfo FindBestAttribute(IEnumerable<object> dbset, IEnumerable<string> roots, IEnumerable<string> ignoredProperties, PropertyInfo target, out double mostGainAttributeValue)
        {
            mostGainAttributeValue = int.MinValue;
            PropertyInfo bestProperty = null;

            foreach (var item in dbset.First().GetType().GetProperties())
            {
                //If the property was not already used
                if (!roots.Contains(item.Name) && !ignoredProperties.Contains(item.Name))
                {
                    var gain = CalcuateGain(dbset.Select(att => new KeyValuePair<string, string>(item.GetValue(att).ToString(), target.GetValue(att).ToString())).ToList());

                    //Check if we got a bigger gain
                    if (gain > mostGainAttributeValue)
                    {
                        mostGainAttributeValue = gain;
                        bestProperty = item;
                    }
                }
            }

            return bestProperty;
        }
        private void FindBestSplit(IEnumerable<KeyValuePair<double, string>> attributes)
        {
            var orderedValues = attributes.OrderBy(item => item.Key);
            var x = orderedValues.Select(item => item.Key).Distinct();
            var list = new List<string>();
            foreach (var value in x)
            {
                foreach (var item in attributes.Select(item => item.Value).Distinct())
                {
                    list.Add($"{value} {item} {orderedValues.Where(val => val.Value == item && val.Key >= value).Count()} {orderedValues.Count() - orderedValues.Where(val => val.Value == item && val.Key >= value).Count()}");
                }

            }

        }
        /// <summary>
        /// Creates the the ID3 nodes
        /// </summary>
        /// <param name="dbset"></param>
        /// <param name="roots"></param>
        /// <param name="ignoredProperties"></param>
        /// <param name="pastBest"></param>
        /// <param name="branchReason"></param>
        /// <returns></returns>
        private NodeID3Model CreateID3Tree(IEnumerable<object> dbset, List<string> roots, IEnumerable<string> ignoredProperties, PropertyInfo target, PropertyInfo pastBest = null, string branchReason = "root")
        {
            //In here that means I reached a leaf node
            if ((dbset.First().GetType().GetProperties().Count() == (roots.Count + ignoredProperties.Count())) || dbset.Select(item => target.GetValue(item)).Distinct().Count() == 1)
            {
                return new NodeID3Model()
                {
                    Name = "leaf",
                    IsLeaf = true,
                    CheckCondition = new IsToCondition((object value) => value.ToString() == branchReason),
                    ToCondition = pastBest.GetValue(dbset.First()).ToString(),
                    Value = target.GetValue(dbset.First()).ToString()

                };
            }
            //Find the best property to branch on
            var bestAtt = FindBestAttribute(dbset, roots, ignoredProperties, target, out double gain);
            //Add the best attribute to the root
            roots.Add(bestAtt.Name);

            var branches = new List<object>();
            var node = new NodeID3Model();
            if (bestAtt.PropertyType == typeof(int) || bestAtt.PropertyType == typeof(double))
            {

                var valueGain = new List<KeyValuePair<double, double>>();
                foreach (var item in dbset)
                {
                    var value = double.Parse(bestAtt.GetValue(item).ToString());
                    if (!valueGain.Any(x => x.Key == value))
                    {
                        valueGain.Add(new KeyValuePair<double, double>(value, CalcuateGain(dbset.Select(att => new KeyValuePair<string, string>(double.Parse(bestAtt.GetValue(att).ToString()) >= value ? value.ToString() : "Other", target.GetValue(att).ToString())))));
                    }
                }
                var maxGainNode = valueGain.Max(item => item.Value);
                var branchValue = valueGain.FirstOrDefault(item => item.Value == maxGainNode);

                node = new NodeID3Model()
                {
                    ToCondition = branchReason,
                    CheckCondition  = new IsToCondition((object value) =>
                    value.ToString() == branchReason),
                    Name = bestAtt.Name,
                    BranchGain = branchValue.Value,
                    IsLeaf = false,
                    PropertyInfo = bestAtt
                };
                var xNode = CreateID3Tree(dbset.Where(val => double.Parse(bestAtt.GetValue(val).ToString()) >= branchValue.Key).ToList(), roots, ignoredProperties, target, bestAtt, $">={branchValue.Key.ToString()}");
                xNode.ToCondition = $">={branchValue.Key.ToString()}";
                xNode.CheckCondition = new IsToCondition((object value) =>
                double.Parse(value.ToString()) >= branchValue.Key);
                //Do a greedy look until we reach the leaf
                node.Next.Add(xNode);
                //Do a greedy look until we reach the leaf
                xNode = CreateID3Tree(dbset.Where(val => double.Parse(bestAtt.GetValue(val).ToString()) < branchValue.Key).ToList(), roots, ignoredProperties, target, bestAtt, $"<{branchValue.Key.ToString()}");

                xNode.CheckCondition = new IsToCondition((object value) => 
                double.Parse(value.ToString()) < branchValue.Key);
                xNode.ToCondition = $"<{branchValue.Key.ToString()}";

                node.Next.Add(xNode);

            }
            else
            {
                //Get the branches that the property has
                branches = dbset.Select(att => bestAtt.GetValue(att)).Distinct().ToList();

                //Create the node
                node = new NodeID3Model()
                {
                    ToCondition = branchReason,
                    CheckCondition = new IsToCondition((object value) => 
                    value.ToString() == branchReason),
                    Name = bestAtt.Name,
                    BranchGain = gain,
                    IsLeaf = false,
                    PropertyInfo = bestAtt
                };
                //Loop through the branchs
                foreach (var branch in branches)
                {
                    //Do a greedy look until we reach the leaf
                    node.Next.Add(CreateID3Tree(dbset.Where(val => bestAtt.GetValue(val).ToString() == branch.ToString()).ToList(), roots, ignoredProperties, target, bestAtt, branch.ToString()));
                }
            }

            //If there  is no more branches
            return node;
        }
        #endregion
    }
}