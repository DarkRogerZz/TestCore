using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore
{
    /// <summary>
    /// 
    /// </summary>
    public class DemoOptions
    {
        public const string MyConfig = "MyConfig";
        public string Name { get; set; }

        public string Tag { get; set; }

        [Range(0,100, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Max { get; set; }
    }
}
