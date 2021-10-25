using GenericWebAPI.Helpers;
using GenericWebAPI.Models;
using GenericWebAPI.Models.Request;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GenericWebAPI.Repositories
{
    public class CarRepository
    {
        /// <summary>
        /// the method returns all cars in accordance to the passed pagination page
        /// </summary>
        /// <param name="paginationPage">pagination page</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>set of cars</returns>
        public async Task<ICollection<Car>> GetCarsAsync(DatabaseContext databaseContext, PaginationPage page = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (page == null) page = new PaginationPage();

            return await databaseContext.Cars
                .OrderBy(x => x.Guid).Skip((page.PageNumber - 1) * page.PageSize)
                .ToArrayAsync(cancellationToken);
        }

        /// <summary>
        /// the method returns a car by identifier
        /// </summary>
        /// <param name="identifier">car's identifier</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>car</returns>
        public async Task<ResponseWrapper<Car>> GetCarAsync(DatabaseContext databaseContext, Guid identifier, CancellationToken cancellationToken = default(CancellationToken))
        {
            var car = await databaseContext.Cars.Where(x => x.Guid == identifier).FirstOrDefaultAsync(cancellationToken);

            return new ResponseWrapper<Car>
            {
                Status = car == null ? System.Net.HttpStatusCode.NotFound : System.Net.HttpStatusCode.OK,
                Content = car
            };
        }

        /// <summary>
        /// the method creates a new car
        /// </summary>
        /// <param name="car">car object</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>created car</returns>
        public async Task<ResponseWrapper<Car>> CreateCarAsync(DatabaseContext databaseContext, Car car, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (!car.Guid.Equals(default(Guid))) return new ResponseWrapper<Car> { Status = System.Net.HttpStatusCode.Conflict };

            car.Guid = Guid.NewGuid();
            car.Created = DateTime.Now;

            databaseContext.Cars.Add(car);
            await databaseContext.SaveChangesAsync(cancellationToken);

            return new ResponseWrapper<Car> { Status = System.Net.HttpStatusCode.OK, Content = car };
        }

        /// <summary>
        /// the method updates an existing car
        /// </summary>
        /// <param name="car">car object</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>updated car</returns>
        public async Task<ResponseWrapper<Car>> UpdateCarAsync(DatabaseContext databaseContext, Car car, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await GetCarAsync(databaseContext, car.Guid, cancellationToken);
            if (!result.Suceeded) return result;

            var existing = result.Content;

            existing.End = car.End;
            existing.Power = car.Power;
            existing.ProducerGuid = car.ProducerGuid;
            existing.Series = car.Series;
            existing.Start = car.Start;
            existing.Type = car.Type;
            
            await databaseContext.SaveChangesAsync(cancellationToken);

            return new ResponseWrapper<Car> { Status = System.Net.HttpStatusCode.OK, Content = existing };
        }

        /// <summary>
        /// the method deletes a car with the passed identifier
        /// </summary>
        /// <param name="guid">car's identifier</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>http status</returns>
        public async Task<ResponseWrapper> DeleteCarAsync(DatabaseContext databaseContext, Guid identifier, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await GetCarAsync(databaseContext, identifier, cancellationToken);
            if (!result.Suceeded) return new ResponseWrapper { Status = result.Status };

            databaseContext.Cars.Remove(result.Content);
            await databaseContext.SaveChangesAsync(cancellationToken);

            return new ResponseWrapper { Status = System.Net.HttpStatusCode.OK };
        }
    }
}
