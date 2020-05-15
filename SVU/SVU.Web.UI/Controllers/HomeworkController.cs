﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SVU.Database.IService;
using SVU.Logging.IServices;
using SVU.Web.UI.Controllers.Base;
using SVU.Web.UI.Models.Homework;
using SVU.Web.UI.Static;
using SVU.Web.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ILoggingService LoggingService { get; private set; }
        public IMemoryCache MemoryCache { get; private set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public HomeworkController(IDataSetDatabaseService dataSetDatabaseService, ILoggingService loggingService, IMemoryCache memoryCache)
        {
            DataSetDatabaseService = dataSetDatabaseService;
            LoggingService = loggingService;
            MemoryCache = memoryCache;
        }
        #endregion

        #region GET Requests

        /// <summary>
        /// The adm course home work for classification using ID3 and bayes
        /// </summary>
        /// <param name="id">The id (name) of the homework</param>
        /// <returns></returns>
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

        #endregion

        #region POST Requests
        /// <summary>
        /// Dose the calculation for the sent alog name as the id paramter
        /// </summary>
        /// <param name="id">The name of the alog to execute</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CalculateBayes([FromBody] CalculateBayesModel model)
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
                    var calcuationsCache = await MemoryCache.GetOrCreate(string.Join('-', "Bayes", model.Target, model.IgnoreProperties), async options =>
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
                                                     Console.WriteLine(keyName);
                                                     //Add the new item
                                                     calcuations.Add(keyName, new Dictionary<string, float>());
                                                 }
                                                 //Check if we did the calcuations on that target
                                                 if (!calcuations[keyName].ContainsKey(target))
                                                 {
                                                     //Do the calculations
                                                     calcuations[keyName].Add(target,
                                                            (dbSet.Where(obj =>
                                                            obj.GetType().GetProperty(model.Target).GetValue(obj).ToString() == target &&
                                                           obj.GetType().GetProperty(property.Name).GetValue(obj).ToString() == propValue
                                                          ).Count() + 0) / (float)targetCount[target]);
                                                 }
                                             }
                                         }
                                         //Else we need to find the avg and the STD (standard deviation)
                                         else
                                         {
                                             throw new NotImplementedException();
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
                    var results = new Dictionary<string, float>();
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

    }
}
