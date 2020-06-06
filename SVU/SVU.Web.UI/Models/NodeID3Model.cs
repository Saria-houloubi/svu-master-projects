using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// The function to check if the condition is meet
/// </summary>
/// <param name="value"></param>
/// <returns></returns>
public delegate bool IsToCondition(object value);

namespace SVU.Web.UI.Models
{
    public class NodeID3Model
    {
        #region Properties
        public bool IsLeaf { get; set; }
        public string Value { get; set; }
        public double BranchGain { get; set; }
        public string Name { get; set; }
        public string ToCondition { get; set; }
        public List<NodeID3Model> Next { get; set; }
        [JsonIgnore]
        public PropertyInfo PropertyInfo { get; set; }
        [JsonIgnore]
        public IsToCondition CheckCondition { get; set; }
        #endregion


        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public NodeID3Model()
        {
            Next = new List<NodeID3Model>();
            CheckCondition = new IsToCondition((object obj) => false);
        }
        #endregion
    }
}
