using Microsoft.AspNetCore.Mvc;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System.Net;
using User.Gestion.Service.Services;
using System.Text.Json;

namespace Gestion_User.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {


        //private readonly Dynamics365Service _dynamics365Service;

        //public WeatherForecastController(Dynamics365Service dynamics365Service)
        //{
        //    _dynamics365Service = dynamics365Service;
        //}

        //public IActionResult GetData()
        //{
        //    var serviceClient = _dynamics365Service.GetServiceClient();
        //    // Utilisez serviceClient pour interagir avec Dynamics 365
        //    return Ok();
        //}


        //[Route("api/accounts")]
        //public async Task<HttpResponseMessage> GetAccount()
        //{
        //    CrmService service = new CrmService();

        //    // Query Expression for retrieving all accounts
        //    QueryExpression query = new QueryExpression("account");
        //    query.ColumnSet = new ColumnSet("accountid", "name");

        //    EntityCollection result = await Task.Run(() => service._service.RetrieveMultiple(query));

        //    if (result.Entities.Count > 0)
        //    {
        //        var serializer = new JavaScriptSerializer();
        //        // Serialize the entities to JSON array
        //        string jsonResult = serializer.Serialize(result.Entities);

        //        // Create HttpResponseMessage with JSON content

        //        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
        //        response.Content = new StringContent(jsonResult, System.Text.Encoding.UTF8, "application/json");

        //        return response;
        //    }
        //    else
        //    {
        //        return Request.CreateResponse(HttpStatusCode.NoContent);
        //    }
        //}

        //[Route("api/accounts")]

        //public async Task<IActionResult> GetAccount()
        //{
        //    CrmService service = new CrmService();

        //    // Query Expression for retrieving all accounts
        //    QueryExpression query = new QueryExpression("account");
        //    query.ColumnSet = new ColumnSet("accountid", "name");

        //    EntityCollection result = await Task.Run(() => service._service.RetrieveMultiple(query));

        //    if (result.Entities.Count > 0)
        //    {
        //        // Serialize the entities to JSON array
        //        string jsonResult = JsonSerializer.Serialize(result.Entities);

        //        // Return the JSON result with OK status
        //        return Ok(jsonResult);
        //    }
        //    else
        //    {
        //        // Return No Content status
        //        return NoContent();
        //    }
        //}
    

    private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
