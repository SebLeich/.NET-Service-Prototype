using GenericWebAPI.Helpers;
using GenericWebAPI.Models;
using GenericWebAPI.Models.Request;
using GenericWebAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GenericWebAPI.Services
{
    /// <summary>
    /// the interface provides a generic template of a car service
    /// </summary>
    public interface ICarService
    {
        /// <summary>
        /// the method returns all cars in accordance to the passed pagination page
        /// </summary>
        /// <param name="paginationPage">pagination page</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>set of cars</returns>
        public Task<ICollection<Car>> GetCarsAsync(PaginationPage paginationPage, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// the method returns a car by identifier
        /// </summary>
        /// <param name="identifier">car's identifier</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>car</returns>
        public Task<ResponseWrapper<Car>> GetCarAsync(Guid identifier, CancellationToken cancellationToken = default(CancellationToken));


        /// <summary>
        /// the method creates a new car
        /// </summary>
        /// <param name="car">car object</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>created car</returns>
        public Task<ResponseWrapper<Car>> CreateCarAsync(Car car, CancellationToken cancellationToken = default(CancellationToken));

        /// the method updates an existing car
        /// </summary>
        /// <param name="car">car object</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>updated car</returns>
        public Task<ResponseWrapper<Car>> UpdateCarAsync(Car car, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// the method deletes a car with the passed identifier
        /// </summary>
        /// <param name="guid">car's identifier</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>http status</returns>
        public Task<ResponseWrapper> DeleteCarAsync(Guid guid, CancellationToken cancellationToken = default(CancellationToken));
    }

    /// <summary>
    /// the service provides methods for car manipulation
    /// </summary>
    public class CarService : ICarService
    {
        /// <summary>
        /// ef core database context
        /// </summary>
        private DatabaseContext _DatabaseContext;

        /// <summary>
        /// the constructor creates a new instance of the car service
        /// </summary>
        /// <param name="databaseContext">ef core database context</param>
        public CarService(DatabaseContext databaseContext)
        {
            _DatabaseContext = databaseContext;
        }

        /// <summary>
        /// the method returns all cars in accordance to the passed pagination page
        /// </summary>
        /// <param name="paginationPage">pagination page</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>set of cars</returns>
        public async Task<ICollection<Car>> GetCarsAsync(PaginationPage paginationPage, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await new CarRepository().GetCarsAsync(_DatabaseContext, paginationPage, cancellationToken);
        }

        /// <summary>
        /// the method returns a car by identifier
        /// </summary>
        /// <param name="identifier">car's identifier</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>car</returns>
        public async Task<ResponseWrapper<Car>> GetCarAsync(Guid identifier, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await new CarRepository().GetCarAsync(_DatabaseContext, identifier, cancellationToken);
        }

        /// <summary>
        /// the method creates a new car
        /// </summary>
        /// <param name="car">car object</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>created car</returns>
        public async Task<ResponseWrapper<Car>> CreateCarAsync(Car car, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await new CarRepository().CreateCarAsync(_DatabaseContext, car, cancellationToken);
        }

        /// <summary>
        /// the method updates an existing car
        /// </summary>
        /// <param name="car">car object</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>updated car</returns>
        public async Task<ResponseWrapper<Car>> UpdateCarAsync(Car car, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await new CarRepository().UpdateCarAsync(_DatabaseContext, car, cancellationToken);
        }

        /// <summary>
        /// the method deletes a car with the passed identifier
        /// </summary>
        /// <param name="guid">car's identifier</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>http status</returns>
        public async Task<ResponseWrapper> DeleteCarAsync(Guid guid, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await new CarRepository().DeleteCarAsync(_DatabaseContext, guid, cancellationToken);
        }
    }
}
