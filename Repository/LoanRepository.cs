using LendingTest.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LendingTest.Repository
{
    public class LoanRepository
    {
        /// <summary>
        /// This method is to request for a loan and process the loan request
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="amount"></param>
        /// <param name="loanProduct"></param>
        /// <param name="intrest"></param>
        /// <param name="tenure"></param>
        /// <returns></returns>
        public string RequestLoan(string phoneNumber, string amount, string loanProduct, double intrest, string tenure)
        {
            if (amount == "5000")
            {
                SendNotification(amount, phoneNumber, loanProduct, intrest.ToString(), tenure, 2);
                return "Failed!";
            }
            else
            {
                SendNotification(amount, phoneNumber, loanProduct, intrest.ToString(), tenure, 1);
                return "Success";
            }
        }

        /// <summary>
        /// This method lists all the loan products
        /// </summary>
        /// <returns></returns>
        public List<LoanProduct> GetLoanProducts()
        {
            List<LoanProduct> loanProducts = new List<LoanProduct>();

            loanProducts.Add(new LoanProduct { Interest = 0.1, Limit = 10000, LoanProductId = 1, LoanProductName = "Product A", Tenure = "15 Days" });
            loanProducts.Add(new LoanProduct { Interest = 0.125, Limit = 25000, LoanProductId = 2, LoanProductName = "Product B", Tenure = "30 Days" });

            return loanProducts;
        }

        /// <summary>
        /// This method sends notifications on the Log file of all the activities taking place from the customer
        /// </summary>
        /// <param name="loanAmount"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="loanProduct"></param>
        /// <param name="intrest"></param>
        /// <param name="tenure"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool SendNotification(string loanAmount, string phoneNumber, string loanProduct, string intrest, string tenure, int status)
        {
            var path = HttpContext.Current.Server.MapPath("/LogFiles/LogFile.txt"); 

            string text = "";

            if (status == 1)
            {
                text = "Dear Customer, Your loan (" + loanProduct + ") of Kes. " + loanAmount + " has been disbursed to your wallet account " + phoneNumber + ". You will pay and Interest of " + intrest + " over a period of " + tenure + "\n ";
            }
            else
            {
                text = "Dear Customer, Your loan (" + loanProduct + ") of Kes. " + loanAmount + " has been Declined! \n";
            }

         //   File.WriteAllText(path, text);
            File.AppendAllText(path, text);

            return true;
        }
    }
}