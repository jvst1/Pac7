using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherPrediction.Application.ApplicationServices;
using WeatherPrediction.Domain.Messaging.Api.Weather.Response;

namespace WeatherPrediction.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherApplicationService _weatherApplicationService;
        public WeatherController(WeatherApplicationService weatherApplicationService)
        {
            _weatherApplicationService = weatherApplicationService;
        }


        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<WeatherResponse>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<WeatherResponse>> GetAll()
        {
            var response = _weatherApplicationService.GetAll();
            if (response == default || !response.Any())
                return NoContent();

            return Ok(response);
        }
    }
}
