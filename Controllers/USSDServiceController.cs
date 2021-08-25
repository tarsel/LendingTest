using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

using LendingTest.Models;
using LendingTest.Repository;

namespace LendingTest.Controllers
{
    [RoutePrefix("application/services")] // This Application will be served as http(s)://<host>:port/application/services/...
    public class USSDServiceController : ApiController
    {
        [Route("ussdservice")]  // http(s)://<host>:port/application/services/ussdservice
        [HttpPost, ActionName("ussdservice")]
        public HttpResponseMessage httpResponseMessage([FromBody] UssdResponse ussdResponse)
        {
            HttpResponseMessage responseMessage;
            string response = null;

            LoanRepository loanRepository = new LoanRepository();

            if (!string.IsNullOrEmpty(ussdResponse.ToString()))
            {

                if (ussdResponse.text == null)
                {
                    ussdResponse.text = "";
                }

                string[] request = ussdResponse.text.Split('*');

                string input = request[0];
                long count = request.LongLength;

                if (ussdResponse.phoneNumber != null)
                {
                    switch (input)
                    {
                        case ""://Landing Page.
                            response = "CON Welcome to DTONE Credit!\n";
                            response += "Select a Service!\n";
                            response += "1. Request Loan\n";
                            response += "2. Repay Loan\n";
                            response += "0. Logout\n";

                            break;
                        case "0"://Logout.
                            response = "END You have logged out!\n";
                            break;
                        case "1"://Loan Request Process    
                            if (count == 1)//Select Loan Product
                            {
                                List<LoanProduct> products = new List<LoanProduct>();

                                products = loanRepository.GetLoanProducts();

                                response = "CON Select Loan Product:\n";

                                foreach (var item in products)
                                {
                                    response += item.LoanProductId + ". " + item.LoanProductName + "\n";
                                }
                            }
                            else if (count == 2)//Enter Amount
                            {
                                response = "CON Enter Amount:\n";

                            }
                            else if (count == 3)//Confirm Application
                            {
                                response = $"CON Confirm Loan Request of Kes. {request[2]}! \n";
                                response += "1. Yes\n";
                                response += "2. No\n";
                            }
                            else if (count == 4)//End
                            {
                                if (request[3].ToString() == "1")
                                {
                                    string result = "";
                                    List<LoanProduct> products = new List<LoanProduct>();

                                    LoanProduct product = loanRepository.GetLoanProducts().Where(w => w.LoanProductId.ToString() == request[1]).FirstOrDefault();

                                    if (long.Parse(request[2]) > product.Limit)
                                    {
                                        result = "END You have Exeeded the Loan Limit!";
                                    }
                                    else
                                    {
                                        result = loanRepository.RequestLoan(ussdResponse.phoneNumber, request[2], product.LoanProductName, product.Interest, product.Tenure);

                                    }

                                    response = $"END " + result + " \n";
                                }
                                else
                                {
                                    response = "END Canceled! \n";
                                }
                            }

                            break;

                        case "2"://Repay Loan
                            if (count == 1)//Key in the Loan Repayment Amount
                            {
                                response = "CON Enter Repayment Amount:\n";
                            }
                            else if (count == 2)//Confirm The Repayment Amount
                            {
                                response = $"CON Confirm Loan Repayment of Kes. {request[1]}! \n";
                                response += "1. Yes\n";
                                response += "2. No\n";
                            }
                            else if (count == 3)//Complete Selection
                            {
                                if (request[2].ToString() == "1")
                                {
                                    response = "END Confirmed! Loan Repaid\n";
                                }
                                else
                                {
                                    response = "END Request Canceled!\n";
                                }
                            }

                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    response = "END Invalid Input!";
                }

                responseMessage = Request.CreateResponse(HttpStatusCode.Created, response);

                responseMessage.Content = new StringContent(response, Encoding.UTF8, "text/plain");

                return responseMessage;

            }
            else
            {
                responseMessage = Request.CreateResponse(HttpStatusCode.Created, response);

                responseMessage.Content = new StringContent(response, Encoding.UTF8, "text/plain");

                return responseMessage;
            }

        }

    }



    public class UssdResponse
    {
        public string text { get; set; }
        public string phoneNumber { get; set; }
        public string sessionId { get; set; }
        public string serviceCode { get; set; }
    }
}
