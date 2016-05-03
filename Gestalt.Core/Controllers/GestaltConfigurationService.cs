using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestalt.Abstractions;
using Microsoft.AspNet.Mvc;

namespace Gestalt.Controllers
{
    [Route("Gestalt")]
    public class GestaltConfigurationService<T> : Controller where T : IGestaltConfigurationSchema
    {
        private IGestaltConfigurationRepository<T> repo;
        public GestaltConfigurationService(IGestaltConfigurationRepository<T> repo)
        {
            this.repo = repo;
        }
        [Route("{application}/{environment?}/{version?}")]
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get(string application, string environment = "", string version = "")
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
