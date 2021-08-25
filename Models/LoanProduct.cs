using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LendingTest.Models
{
    /// <summary>
    /// Loan Product
    /// </summary>
    public class LoanProduct
    {
        public long LoanProductId { get; set; }
        public string LoanProductName { get; set; }
        public long Limit { get; set; }
        public double Interest { get; set; }
        public string Tenure { get; set; }
    }
}