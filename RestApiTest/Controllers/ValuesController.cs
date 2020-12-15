using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using RestApiTest.Models;
using RestApiTest.Services;

namespace RestApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMongoDbService _mongoDbService;

        static List<string> Values { get; set; } = new List<string> { "value1", "value2" };
        public ValuesController(IMongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get()
        {
            var coll = _mongoDbService.GetDb().GetCollection<Person>("testData");
            var people = coll.Find(p => true).ToList();
            return people;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Person> Get(string id)
        {
            var coll = _mongoDbService.GetDb().GetCollection<Person>("testData");
            var person = coll.Find(p => p.Id == new MongoDB.Bson.ObjectId(id)).FirstOrDefault();
            return person;
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] Person person)
        {
            var coll = _mongoDbService.GetDb().GetCollection<Person>("testData");
            try
            {
                coll.InsertOne(person);
                return Ok("success");
            }
            catch (Exception ex)
            {
                return new ObjectResult(500) { Value = ex.ToString(), StatusCode = 500 };
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Person person)
        {
            var coll = _mongoDbService.GetDb().GetCollection<Person>("testData");
            try
            {
                person.Id = new MongoDB.Bson.ObjectId(id);
                coll.ReplaceOne(p => p.Id == new MongoDB.Bson.ObjectId(id), person);
                return Ok("success");
            }
            catch (Exception ex)
            {
                return new ObjectResult(500) { Value = ex.ToString(), StatusCode = 500 };
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var coll = _mongoDbService.GetDb().GetCollection<Person>("testData");
            try
            {
                coll.DeleteOne(p => p.Id == new MongoDB.Bson.ObjectId(id));
                return Ok("success");
            }
            catch (Exception ex)
            {
                return new ObjectResult(500) { Value = ex.ToString(), StatusCode = 500 };
            }
        }
    }
}
