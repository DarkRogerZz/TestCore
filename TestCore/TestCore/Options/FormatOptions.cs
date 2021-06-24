using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TestCore.Options
{
    public class FormatOptions
    {
        public DateTimeFormatOptions DateTime { get; set; }
        public CurrencyDecimalFormatOptions CurrencyDecimal { get; set; }

        //public FormatOptions(IConfiguration config)
        //{
        //    DateTime = new DateTimeFormatOptions(config.GetSection("DateTime"));
        //    CurrencyDecimal = new CurrencyDecimalFormatOptions(config.GetSection("CurrencyDecimal"));
        //}
    }
}
