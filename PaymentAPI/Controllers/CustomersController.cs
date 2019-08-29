using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace PaymentAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        CustomersMethods cService = new CustomersMethods();

        // GET api/customers
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

            return new string[] { "Please include an ID in the url to check for Payment status. " };
        }

        // GET api/customers/$CUSTOMER_ID
        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            string isAuthorized = "1";
            //int userID = Convert.ToInt32(id);



            // this is the end point that will grab the list of customers
            string endpoint = "https://api.stripe.com/v1/customers";

            //initialize the stripe client and give it the endpoint
            StripeClient stripeClient = new StripeClient();
            stripeClient.endpoint = endpoint;

            // use the stripe cleint to request a list of customers from stripe. (makes an http request to stripe api)
            string response = stripeClient.makeRequest();
            System.Diagnostics.Debug.WriteLine(response);
            var customers = Customers.FromJson(response); // turn json into list of customer objects

            //check if ID is valid
            bool IDIsValid = false;
            foreach (CustomersDatum customer in customers.Data)
            {
                if (customer.Id == id) { IDIsValid = true; }
            }
            if (IDIsValid == false) { return "ID is invalid. Check the ID to make sure it is correct and associated with an active account."; }


            // check for delinquent status. if delinquent then change is Authorized status to 0
            List<CustomersDatum> delinquents = cService.getDelinquents(customers);
            for (int i=0; i<delinquents.Count; i++)
            {
                if(delinquents.ElementAt(i).Id == id)
                {
                    isAuthorized = "0";
                }
            }


            return isAuthorized;
        }

    }
}
