#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTools.Enumerations;

#endregion

namespace CommonTools.Test.TestClasses
{
    public enum SampleEnum
    {
        Unknown,
        [EnumDescription("A")]
        Value1,
        [EnumDescription("B")]
        Value2,
        [EnumDescription("c")]
        Value3
    }
}
