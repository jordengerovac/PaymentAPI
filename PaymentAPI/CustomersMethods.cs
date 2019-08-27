using System;
using System.Collections.Generic;

namespace PaymentAPI
{
    public class CustomersMethods
    {
        public CustomersMethods()
        {
           
        }


        public List<CustomersDatum> getDelinquents()
        {
            //string endpoint = "https://jsonplaceholder.typicode.com/todos/1";
            string endpoint = "https://api.stripe.com/v1/customers";

            StripeClient stripeClient = new StripeClient();
            stripeClient.endpoint = endpoint;
            string response = stripeClient.makeRequest();
            //string response = "[" + stripeClient.makeRequest() + "]";
            System.Diagnostics.Debug.WriteLine(response);
            System.Diagnostics.Debug.WriteLine("TESTTESTTEST");

            var customers = Customers.FromJson(response);

            System.Diagnostics.Debug.WriteLine(customers.Data.Count);

            //populate fake delinquints
            int count = 0;
            foreach (CustomersDatum customer in customers.Data)
            {
                if (count % 5 == 0)
                {
                    System.Diagnostics.Debug.WriteLine(count);

                    customer.Delinquent = true;
                }
                count++;


            }

            //create delinquent list
            List<CustomersDatum> delinquents = new List<CustomersDatum>();
            foreach (CustomersDatum customer in customers.Data)
            {
                if (customer.Delinquent == true)
                {
                    delinquents.Add(customer);
                    System.Diagnostics.Debug.WriteLine(delinquents.Count);

                }
            }
            System.Diagnostics.Debug.WriteLine(delinquents.Count);

            return delinquents;
        }
    }
}
