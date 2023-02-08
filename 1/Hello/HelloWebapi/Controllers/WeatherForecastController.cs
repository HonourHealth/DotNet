using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace HelloWebapi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Id = index,
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        private ActionResult<List<WeatherForecast>> GetList()//
        {   var rng = new Random();
            List<WeatherForecast> list = new();
            list.Add(new WeatherForecast { Id = 1, Date = DateTime.Now.AddDays(1), TemperatureC = rng.Next(-20, 55), Summary = Summaries[rng.Next(Summaries.Length)]});
            list.Add(new WeatherForecast { Id = 2, Date = DateTime.Now.AddDays(2), TemperatureC = rng.Next(-20, 55), Summary = Summaries[rng.Next(Summaries.Length)]});
            list.Add(new WeatherForecast { Id = 3, Date = DateTime.Now.AddDays(3), TemperatureC = rng.Next(-20, 55), Summary = Summaries[rng.Next(Summaries.Length)] });
            list.Add(new WeatherForecast { Id = 4, Date = DateTime.Now.AddDays(4), TemperatureC = rng.Next(-20, 55), Summary = Summaries[rng.Next(Summaries.Length)] });
            list.Add(new WeatherForecast { Id = 5, Date = DateTime.Now.AddDays(5), TemperatureC = rng.Next(-20, 55), Summary = Summaries[rng.Next(Summaries.Length)] });
            return new ActionResult<List<WeatherForecast>>(list);
        }


        [HttpGet("{id}")] //api/WeatherForecast/3
        public ActionResult<WeatherForecast> GetById(int id)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Id = id,
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray()[0];
        }
        
        [HttpPost]
        public ActionResult<List<WeatherForecast>> Post([FromBody] WeatherForecast weather)
        {
            List<WeatherForecast> list = GetList().Value;
            list.Add(weather);
            return new ActionResult<List<WeatherForecast>>(list);
        }

        /*
        [HttpPut("{id}")]
        public ActionResult<List<WeatherForecast>> Put([FromRoute] int Id, [FromBody] WeatherForecast request)
        {
            var list = GetList().Value;
            WeatherForecast weather = list.Where(x => x.Id == Id).FirstOrDefault();
            list.Remove(weather);

            request.Id = Id;
            list.Add(request);
            return new ActionResult<List<WeatherForecast>>(list);
        }
        */
        
        [HttpDelete("{id}")]
        public ActionResult<List<WeatherForecast>> Delete([FromRoute] int id)
        {
            var list = GetList().Value;
            WeatherForecast weather = list.Where(x => x.Id == id).FirstOrDefault();
            list.Remove(weather);
            return new ActionResult<List<WeatherForecast>>(list);
        }
        
        /*
        // POST api/<WeatherForecast>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<WeatherForecast>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WeatherForecast>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPatch]
        public void Patch([FromBody] string value)
        {
            
        }
        */
        /*
        [HttpGet]
        //api/WeatherForecast?id=3
        public IEnumerable<WeatherForecast> GetById([FromQuery] string id)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        */
    }
}
