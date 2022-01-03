using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgencyAPI.Utils;
using TravelAgencyDTO;
using TravelAgencyEntity;
using System.Data.Common;

namespace TravelAgencyAPI.Controllers
{
    /// <summary>
    /// Reviews
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [TokenCheck]
    public class ReviewController : ControllerBase
    {
        private readonly TravelAgencyContext dbContext;
        public ReviewController()
        {
            if (dbContext == null) dbContext = new TravelAgencyContext();
        }

        /// <summary>
        /// Given tour reservation, get the tour
        /// </summary>
        /// <param name="reserveId"></param>
        /// <returns></returns>
        [HttpGet("tourReserveToTour")]
        public IActionResult TourReserveToTour(int reserveId)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                string query = "SELECT Tour.tour_id, city, tour_name, tour_start_date, tour_end_date, tour_description, price, percents " +
                                " FROM Reservation JOIN TourReservation ON TourReservation.reserve_id=Reservation.reserve_id " +
                                " JOIN Tour ON Tour.tour_id = TourReservation.tour_id " +
                                " LEFT JOIN Discount ON Discount.discount_id=Tour.discount_id " +
                                " WHERE Reservation.reserve_id=" + reserveId + ";";

                Func<DbDataReader, TourDTO> map = x => new TourDTO
                {
                    tourId = (int)x[0],
                    tourName = (string)x[2],
                    city = (string)x[1],
                    tourStartDate = (DateTime)x[3],
                    tourEndDate = (DateTime)x[4],
                    tourDescription = (string)x[5],
                    price = (decimal)x[6],
                    discountPercents = (x[7] != DBNull.Value) ? ((int)x[7]) : 0 // if percents is null, then the discount applied is zero percent
                };
                var output = Helper.RawSqlQuery<TourDTO>(query, map).ToList().SingleOrDefault();
                response.Data = output;
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
        /// Given tour reservation, get the guide
        /// </summary>
        /// <param name="reserveId"></param>
        /// <returns></returns>
        [HttpGet("tourReserveToGuide")] // Please set the http request type, and http request name.
        public IActionResult TourReserveToGuide(int reserveId)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                string query = "SELECT Guide.u_id, Users.first_name, Users.last_name " +
                                "FROM Reservation JOIN TourReservation ON TourReservation.reserve_id=Reservation.reserve_id " +
                                                "JOIN Tour ON Tour.tour_id = TourReservation.tour_id " +
                                                "JOIN assign_guide ON Tour.tour_id=assign_guide.tour_id " +
                                                "JOIN Guide ON Guide.u_id = assign_guide.guide_u_id " +
                                                "JOIN Users ON Users.u_id=Guide.u_id WHERE Reservation.reserve_id=" + reserveId + ";";

                Func<DbDataReader, GuideDTO> map = x => new GuideDTO
                {
                    uId = (int)x[0],
                    firstName = (string)x[1],
                    lastName = (string)x[2]
                };
                var output = Helper.RawSqlQuery<GuideDTO>(query, map).ToList().SingleOrDefault();
                response.Data = output;
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
        /// Review tour
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        [HttpPost("tourReview")] // Please set the http request type, and http request name.
        public IActionResult AddTourReview(ReviewInput tourReviewInput)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                // SQL Queries here
                int maxId;
                string countQuery = "SELECT COUNT(*) FROM Review;";
                string maxQuery = "SELECT MAX(review_id) FROM Review;";
                Func<DbDataReader, int> mapInt = x => (int)x[0];
                int count = Helper.RawSqlQuery<int>(countQuery, mapInt).SingleOrDefault();
                if (count > 0)
                {
                    maxId = Helper.RawSqlQuery<int>(maxQuery, mapInt).SingleOrDefault();
                }
                else
                {
                    maxId = 0;
                }
                var newId = maxId + 1;

                dbContext.Database.ExecuteSqlInterpolated($"INSERT INTO Review VALUES({newId}, {tourReviewInput.comment}, \'{tourReviewInput.entryDate}\', {tourReviewInput.stars}, {tourReviewInput.uId});");
                dbContext.Database.ExecuteSqlInterpolated($"INSERT INTO TourReview VALUES({newId}, {tourReviewInput.id});");

                // Send an HTTP response as data, if necessary
                response.Data = null;

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
        /// Review Guide
        /// </summary>
        /// <param name="uId"></param>
        /// <returns></returns>
        [HttpPost("guideReview")] // Please set the http request type, and http request name.
        public IActionResult AddGuideReview(ReviewInput guideReviewInput)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                // SQL Queries here
                int maxId;
                string countQuery = "SELECT COUNT(*) FROM Review;";
                string maxQuery = "SELECT MAX(review_id) FROM Review;";
                Func<DbDataReader, int> mapInt = x => (int)x[0];
                int count = Helper.RawSqlQuery<int>(countQuery, mapInt).SingleOrDefault();
                if (count > 0)
                {
                    maxId = Helper.RawSqlQuery<int>(maxQuery, mapInt).SingleOrDefault();
                }
                else
                {
                    maxId = 0;
                }
                var newId = maxId + 1;

                dbContext.Database.ExecuteSqlInterpolated($"INSERT INTO Review VALUES({newId}, {guideReviewInput.comment}, \'{guideReviewInput.entryDate}\', {guideReviewInput.stars}, {guideReviewInput.uId});");
                dbContext.Database.ExecuteSqlInterpolated($"INSERT INTO GuideReview VALUES({newId}, {guideReviewInput.id});");

                // Send an HTTP response as data, if necessary
                response.Data = null;
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
