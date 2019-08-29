using System;
using System.Collections.Generic;

namespace PaymentAPI
{
    public class CustomersMethods
    {
        public CustomersMethods()
        {
           
        }

        /// <summary>
        /// a method that returns a list containg all the delinquents from our stripe customers. 
        /// in stripe a delinquent is a customer whos most recent charge has failed (could be due to no funds, cancled cards, many other reasons, but bottom line is they missed a payment)
        /// </summary>
        /// <returns></returns>
        public List<CustomersDatum> getDelinquents(Customers customers)
        {



            //this method is used for testing.it would turn every 5th customer into a delinquent
            //int count = 0;
            //foreach (CustomersDatum customer in customers.Data)
            //{
            //    if (count % 5 == 0)
            //    {
            //        System.Diagnostics.Debug.WriteLine(customer.Id);
            //        System.Diagnostics.Debug.WriteLine(customer.Name);
            //        customer.Delinquent = true;
            //    }
            //    count++;
            //}

            //create delinquent list 
            List<CustomersDatum> delinquents = new List<CustomersDatum>();
            foreach (CustomersDatum customer in customers.Data)
            {
                if (customer.Delinquent == true)
                {
                    delinquents.Add(customer);
                }
            }


            System.Diagnostics.Debug.WriteLine(delinquents.Count);
            return delinquents;

        }
    }
}
