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
        /// Review tour
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        [HttpGet("tourReview")] // Please set the http request type, and http request name.
        public IActionResult TourReview(int tourId)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                // SQL Queries here
                int maxId;
                if (dbContext.TourReviews.Count() > 0)
                {
                    maxId = dbContext.TourReviews.Max(table => table.ReviewId);
                }
                else
                {
                    maxId = 0;
                }
                var newId = maxId + 1;

                var tour = dbContext.Tours.FromSqlRaw("SELECT * FROM Tour WHERE Tour_Id = " + tourId + ";").ToList().FirstOrDefault();
                string finalQuery = "SELECT * FROM Tour WHERE tour_id = " + tourId + ";";
                Func<DbDataReader, TourRating> map = x => new TourRating
                {
                    tourId = (int)x[0],
                    tourName = (string)x[1],
                    avgStars = (int)x[2],
                };
                var tourRatings = Helper.RawSqlQuery<TourRating>(finalQuery, map);

                dbContext.Database.ExecuteSqlInterpolated($"INSERT INTO TourReviews VALUES({newId}, {tourId});");

                // Send an HTTP response as data, if necessary
                response.Data = tourRatings;

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
        [HttpGet("guideReview")] // Please set the http request type, and http request name.
        public IActionResult GuideReview(int uId)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                // SQL Queries here
                int maxId;
                if (dbContext.GuideReviews.Count() > 0)
                {
                    maxId = dbContext.GuideReviews.Max(table => table.ReviewId);
                }
                else
                {
                    maxId = 0;
                }
                var newId = maxId + 1;

                var guide = dbContext.Guides.FromSqlRaw("SELECT * FROM Guide WHERE UId = " + uId + ";").ToList().FirstOrDefault();
                string finalQuery = "SELECT * FROM Guide WHERE UId = " + uId + ";";
                Func<DbDataReader, GuideRating> map = x => new GuideRating
                {
                    uId = (int)x[0],
                    firstName = (string)x[1],
                    lastName = (string)x[2],
                    avgStars = (int)x[3],
                };
                var guideRatings = Helper.RawSqlQuery<GuideRating>(finalQuery, map);

                dbContext.Database.ExecuteSqlInterpolated($"INSERT INTO GuideReviews VALUES({newId}, {uId});");

                // Send an HTTP response as data, if necessary
                response.Data = guideRatings;
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
