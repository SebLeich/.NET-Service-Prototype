using GenericWebAPI.Helpers;
using GenericWebAPI.Models;
using GenericWebAPI.Models.Request;
using GenericWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GenericWebAPI.Controllers
{
    /// <summary>
    /// the controller provides endpoints for car interaction
    /// </summary>
    [Route("api/car")]
    [ApiController]
    public class CarController : ControllerBase
    {
        /// <summary>
        /// the service provides methods for car interaction
        /// </summary>
        private ICarService _CarService;

        /// <summary>
        /// the constructor creates a new instance of the controller
        /// </summary>
        /// <param name="carService">di injected car service</param>
        public CarController(ICarService carService)
        {
            _CarService = carService;
        }

        /// <summary>
        /// the method returns a set of cars in accordance to the passed pagination page
        /// </summary>
        /// <param name="page">pagination page</param>
        /// <param name="cancellationToken">cancellatiuon token</param>
        /// <returns>set of cars</returns>
        [Authorize(Policy = nameof(PolicyStore.Policies.AdministrativeAccount))]
        [HttpPost("search")]
        public async Task<IActionResult> GetCarsAsync([FromBody] PaginationPage page, CancellationToken cancellationToken)
        {
            return new OkObjectResult(await _CarService.GetCarsAsync(page, cancellationToken));
        }

        /// <summary>
        /// the endpoint enables clients to retrieve cars by their guid
        /// </summary>
        /// <param name="identifier">car's identifier</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>cars by identifier</returns>
        [HttpGet("{identifier}")]
        public async Task<IActionResult> GetCarAsync([FromRoute] Guid identifier, CancellationToken cancellationToken)
        {
            var result = await _CarService.GetCarAsync(identifier, cancellationToken);

            if (!result.Succeeded) return new StatusCodeResult((int)result.Status);

            return new OkObjectResult(result.Content);
        }

        /// <summary>
        /// the endpoint enables clients to create new cars within the database
        /// </summary>
        /// <param name="car">car object</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>created car</returns>
        [HttpPost("")]
        public async Task<IActionResult> CreateCarAsync([FromBody] Car car, CancellationToken cancellationToken)
        {
            var result = await _CarService.CreateCarAsync(car, cancellationToken);

            if (!result.Succeeded) return new StatusCodeResult((int)result.Status);

            return new OkObjectResult(result.Content);
        }

        /// <summary>
        /// the endpoint enables client's to update existing cars
        /// </summary>
        /// <param name="car">car object</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>updated car</returns>
        [HttpPut("")]
        public async Task<IActionResult> UpdateCarAsync([FromBody] Car car, CancellationToken cancellationToken)
        {
            var result = await _CarService.UpdateCarAsync(car, cancellationToken);

            if (!result.Succeeded) return new StatusCodeResult((int)result.Status);

            return new OkObjectResult(result.Content);
        }

        /// <summary>
        /// the endpoint enables clients to delete cars from the database
        /// </summary>
        /// <param name="identifier">cars identifier</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>deleted car</returns>
        [HttpDelete("{identifier}")]
        public async Task<IActionResult> DeleteCarAsync([FromRoute] Guid identifier, CancellationToken cancellationToken)
        {
            var result = await _CarService.DeleteCarAsync(identifier, cancellationToken);

            return new StatusCodeResult((int)result.Status);
        }
    }
}
