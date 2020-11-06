using System;
using System.Runtime.CompilerServices;

namespace AMW.Data.Attributes
{
    public class SqlParamAttribute : Attribute
    {
        #region Properties
        public string Name { get; set; }
        #endregion

        #region Constructer
        public SqlParamAttribute([CallerMemberName]string name = null)
        {
            Name = name;
        }
        #endregion

    }
}
