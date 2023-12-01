using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication_Assignment_SkillsLab2023.Controllers
{
    public class TrialController : ApiController
    {
        // GET: api/Trial
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Trial/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Trial
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Trial/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Trial/5
        public void Delete(int id)
        {
        }
    }
}
