using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
namespace PaymentAPI
{
    public class StripeClient
    {

        public enum httpVerb
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public string endpoint { get; set; }
        public httpVerb httpMethod { get; set; }

        public StripeClient()
        {
            endpoint = string.Empty;
            httpMethod = httpVerb.GET;

        }

        public string makeRequest()
        {
            string strResponseValue = string.Empty;

            // build request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint); 
            request.Method = httpMethod.ToString();
            // add an authorization header
            request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"sk_test_GXdRSMJMLiXJIDqc1qnEGWaA:")));


            //make request
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                //check for error codes
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException("error code: " + response.StatusCode.ToString());
                }

                //Process the response stream... (should be JSON)
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            strResponseValue = reader.ReadToEnd();

                        }//end of using StreamReader
                    }
                }//End of using ResponseStream

            }//End of using response

            return strResponseValue;
        }
        //}

    }
}