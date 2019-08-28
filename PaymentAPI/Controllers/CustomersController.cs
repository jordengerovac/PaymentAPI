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

            return new string[] { "value3", "value4" };
        }

        // GET api/customers/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            List<CustomersDatum> delinquents = cService.getDelinquents();
            string isDelinquent = "0";
            //int userID = Convert.ToInt32(id);
            for (int i=0; i<delinquents.Count; i++)
            {
                if(delinquents.ElementAt(i).Id == id)
                {
                    isDelinquent = "1";
                }
            }
            return isDelinquent;
        }


        // POST api/customers
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/customers/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/customers/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
