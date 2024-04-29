using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grocery_Store.Areas.Admin.Data
{
    public class OrderSummary
    {
        public DateTime Date { get; set; }
        public int TotalAmount { get; set; }
    }
}