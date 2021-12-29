using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;
using System.Linq;
using TravelAgencyAPI.Utils;
using TravelAgencyDTO;
using TravelAgencyEntity;

namespace TravelAgencyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TokenCheck]
    public class BookingController : ControllerBase
    {
        private readonly TravelAgencyContext dbContext;
        public BookingController()
        {
            if (dbContext == null) dbContext = new TravelAgencyContext();
        }

        /// <summary>
        /// List all tours and apply filters. It's asumed that DateTime input comes as validated
        /// </summary>
        /// <param name="tourFilter"></param>
        /// <returns>
        /// http://localhost:5000/api/booking/tours
        /// </returns>
        [HttpPost("tours")]
        public IActionResult Tours(TourFilter tourFilter)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                bool hasDate = tourFilter.dateRangeEnd != null && tourFilter.dateRangeStart != null;
                bool hasCity = tourFilter.city != null && tourFilter.city != "";
                bool hasPrice = tourFilter.priceRangeEnd > 0
                    || tourFilter.priceRangeStart > 0;
                string dateQ = (hasDate) ? ("tour_start_date >= \'" + tourFilter.dateRangeStart.ToString("s").Substring(0, 10) + "\' AND tour_end_date <= \'" 
                    + tourFilter.dateRangeEnd.ToString("s").Substring(0, 10) ) + "\'" : " 1=1 ";
                string cityQ = (hasCity) ? (" AND city LIKE \'%" + tourFilter.city + "%\'") : " AND 1=1 ";
                string priceQ = " AND 1=1 ";
                if (tourFilter.priceRangeStart > 0 && tourFilter.priceRangeEnd == 0) {
                    priceQ = " AND price >= " + tourFilter.priceRangeStart;
                }
                else if (tourFilter.priceRangeEnd > 0 && tourFilter.priceRangeStart == 0) {
                    priceQ = " AND price <= " + tourFilter.priceRangeEnd;
                }
                else if (tourFilter.priceRangeEnd == 0 && tourFilter.priceRangeStart == 0) {
                    priceQ = " AND 1=1";
                }
                else if (tourFilter.priceRangeStart <= tourFilter.priceRangeEnd)
                {
                    priceQ = " AND price >= " + tourFilter.priceRangeStart + " AND price <= " + tourFilter.priceRangeEnd;
                }
                string finalQuery = "WITH results" +
                    "(tour_id, city, tour_name, tour_start_date, tour_description, price, discount_id, tour_end_date, discount_start_date) " +
                    "AS (SELECT * " +
                    "FROM Tour " +
                    "WHERE " +
                    dateQ +
                    cityQ +
                    priceQ +
                    ") " +
                    " SELECT tour_id, tour_name, city, tour_start_date, tour_end_date, tour_description, price, percents " +
                    " FROM results LEFT JOIN Discount ON results.discount_id = Discount.discount_id; ";
                Func<DbDataReader, TourDTO> map = x => new TourDTO
                {
                    tourId = (int)x[0],
                    tourName = (string)x[1],
                    city = (string)x[2],
                    tourStartDate = (DateTime)x[3],
                    tourEndDate = (DateTime)x[4],
                    tourDescription = (string)x[5],
                    price = (decimal)x[6],
                    discountPercents = (x[7] != DBNull.Value)?((int)x[7]):0 // if percents is null, then the discount applied is zero percent
                };
                //var tours = dbContext.Tours.FromSqlRaw(finalQuery).ToList();
                Console.Write(finalQuery);
                var toursDTO = Helper.RawSqlQuery<TourDTO>(finalQuery, map);
                // Get the discount of each tour
                // Send an HTTP response as data, if necessary
                response.Data = toursDTO;
            }
            catch (Exception ex)
            {
                // Catch SQL Exceptions, and send them to frontend
                response.HasError = true;
                response.ErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    response.ErrorMessage += ": " + ex.InnerException.Message;
                }
            }
            // Return the HTTP response
            return Ok(response);
        }

        /// <summary>
        /// Display hotels
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpGet("hotels")] 
        public IActionResult Hotels()
        {
            ResponseModel response = new ResponseModel();
            try
            {
                // SQL Queries here

                // If you want to send an error to the frontend,
                // you can set response.HasError = true
                // set a ErrorMessage and just return Ok(response)

                var hotels = dbContext.Hotels.FromSqlRaw("SELECT * FROM Hotel").ToList();

                // Send an HTTP response as data, if necessary
                response.Data = hotels;
            }
            catch (Exception ex)
            {
                // Catch SQL Exceptions, and send them to frontend
                response.HasError = true;
                response.ErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    response.ErrorMessage += ": " + ex.InnerException.Message;
                }
            }
            // Return the HTTP response
            return Ok(response);
        }
    }
}
