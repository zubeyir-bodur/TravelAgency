using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
        public IActionResult Action(TourFilter tourFilter)
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
                string finalQuery = "SELECT * " +
                    "FROM Tour " +
                    "WHERE " +
                    dateQ +
                    cityQ +
                    priceQ +
                    ";";
                // SQL Queries here
                var tours = dbContext.Tours.FromSqlRaw(finalQuery).ToList();
                // Send an HTTP response as data, if necessary
                response.Data = tours;
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
