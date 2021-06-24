using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TestCore.Options
{
    public class CurrencyDecimalFormatOptions
    {
        public int Digits { get; set; }
        public string Symbol { get; set; }

        //public CurrencyDecimalFormatOptions(IConfiguration config)
        //{
        //    Digits = int.Parse(config["Digits"]);
        //    Symbol = config["Symbol"];
        //}
    }
}
