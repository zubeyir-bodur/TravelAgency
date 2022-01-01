using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using TravelAgencyAPI.Utils;
using TravelAgencyDTO;
using TravelAgencyEntity;

namespace TravelAgencyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TokenCheck]
    public class StatisticsController : ControllerBase
    {
        private readonly TravelAgencyContext dbContext;
        public StatisticsController()
        {
            if (dbContext == null) dbContext = new TravelAgencyContext();
        }
        
        [HttpGet("history")]
        public IActionResult History()
        {
            ResponseModel response = new ResponseModel();
            try
            {
                // SQL Queries here
                string finalQuery = "WITH paymentHistory(reserve_id,	reserve_start_date,	reserve_end_date, " +
                        "u_id, first_name, last_name, tour_id, " +
						"tour_name, price, is_booked, " +
						"num_reserving, " +
						"total_ticket_price_per) AS " +
                        "(SELECT Reservation.reserve_id, reserve_start_date, " +
                                    "reserve_end_date, Users.u_id, " +
                                    "Users.first_name, Users.last_name, " +
                                    "Tour.tour_id, Tour.tour_name, " +
                                    "price, is_booked, num_reserving, " + 
                                    "SUM(ticket_price) as total_ticket_price_per " +
                        "FROM Reservation    JOIN TourReservation ON Reservation.reserve_id = TourReservation.reserve_id " +
                                            "JOIN Tour ON Tour.tour_id = TourReservation.tour_id " +
                                            "JOIN marked_activity ON Reservation.reserve_id = marked_activity.reserve_id " +
                                            "JOIN Activity ON Activity.activity_id = marked_activity.activity_id "+ 
                                            "JOIN Users ON Reservation.u_id = Users.u_id " +
                        "GROUP BY Reservation.reserve_id, reserve_start_date, " +
                                    "reserve_end_date, Users.u_id, " +
                                    "Users.first_name, Users.last_name, " +
                                    "Tour.tour_id, Tour.tour_name, " +
                                    "price, is_booked, " +
                                    "num_reserving) " +
                    "SELECT u_id, " +
                            "first_name, " + 
                            " last_name, " +
                            "reserve_start_date, " +
                            "reserve_end_date, " +
                            "tour_name, " +
                            "price, " +
                            "is_booked, " +
                            "num_reserving, " +
                            "total_ticket_price_per as extra_activities_per, " +
		                    "is_booked* num_reserving*(price + total_ticket_price_per) as total_payment_done " +
                    "FROM paymentHistory " +
                    "ORDER BY last_name ASC";
                Func<DbDataReader, PaymentHistory> map = x => new PaymentHistory
                {
                    uId = (int)x[0],
                    firstName = (string)x[1],
                    lastName = (string)x[2],
                    reserveStartDate = (DateTime)x[3],
                    reserveEndDate = (DateTime)x[4],
                    tourName = (string)x[5],
                    price = (decimal)x[6],
                    isBooked = (bool)x[7],
                    numReserving = (int)x[8],
                    extraActivitiesPer = (decimal)x[9],
                    totalPaymentDone = (decimal)x[10]
                };
                Console.Write(finalQuery);
                var paymentHistories = Helper.RawSqlQuery<PaymentHistory>(finalQuery, map);


                // If you want to send an error to the frontend,
                // you can set response.HasError = true
                // set a ErrorMessage and just return Ok(response)

                // Send an HTTP response as data, if necessary
                response.Data = paymentHistories;
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
        /// An example http request
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpGet("historyOut")] // Please set the http request type, and http request name.
        public IActionResult HistoryOut()
        {
            ResponseModel response = new ResponseModel();
            try
            {
                // SQL Queries here
                string finalQuery = "WITH paymentHistory(reserve_id,	reserve_start_date,	reserve_end_date, " +
                                                        "u_id, first_name, last_name, tour_id, " +
                                                        "price, is_booked, " +
                                                        "num_reserving, total_ticket_price_per) AS " +
                                        "(SELECT Reservation.reserve_id, reserve_start_date, " +
                                                "reserve_end_date, Users.u_id, Users.first_name, " +
                                                "Users.last_name, Tour.tour_id, price, " +
                                                "is_booked, num_reserving, " +
                                                "SUM(ticket_price) as total_ticket_price_per " +
                                        "FROM Reservation    JOIN TourReservation ON Reservation.reserve_id = TourReservation.reserve_id " +
                                                            "JOIN Tour ON Tour.tour_id = TourReservation.tour_id " +
                                                            "JOIN marked_activity ON Reservation.reserve_id = marked_activity.reserve_id " +
                                                            "JOIN Activity ON Activity.activity_id = marked_activity.activity_id " +
                                                            "JOIN Users ON Reservation.u_id = Users.u_id " +
                                        "GROUP BY Reservation.reserve_id, " +
                                                    "reserve_start_date, " +
                                                    "reserve_end_date, " +
                                                    "Users.u_id, " +
                                                    "Users.first_name, " +
                                                    "Users.last_name, " +
                                                    "Tour.tour_id, " +
                                                    "price, " +
                                                    "is_booked, " +
                                                    "num_reserving) " +
                                    "SELECT u_id, first_name, last_name, SUM(total_payment_done) AS total_payment_so_far " +
                                    "FROM( " +
                                        "SELECT  u_id, first_name, last_name, " +
                                                "price, is_booked, num_reserving, total_ticket_price_per, " +
                                                "is_booked * num_reserving * (price + total_ticket_price_per) as total_payment_done " +
                                        "FROM paymentHistory) AS paymentHistoryFinal " +
                                    "GROUP BY u_id, first_name, last_name " +
                                    "ORDER BY total_payment_so_far DESC;";
                Func<DbDataReader, TotalPaymentDTO> map = x => new TotalPaymentDTO
                {
                    uId = (int)x[0],
                    firstName = (string)x[1],
                    lastName = (string)x[2],
                    totalPaymentSoFar = (decimal)x[3],
                };
                Console.Write(finalQuery);
                var totalPayments = Helper.RawSqlQuery<TotalPaymentDTO>(finalQuery, map);

                // Send an HTTP response as data, if necessary
                response.Data = totalPayments;
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
        /// An example http request
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpGet("guidesTop")] // Please set the http request type, and http request name.
        public IActionResult GuidesTop()
        {
            ResponseModel response = new ResponseModel();
            try
            {
                // SQL Queries here
                string finalQuery = "WITH guide_ratio(u_id, avg_stars) AS( " +
                                        "SELECT Guide.u_id, AVG(Review.rating) as avg_stars " +
                                        "FROM Guide  JOIN GuideReview ON Guide.u_id = GuideReview.u_id " +
                                                    "JOIN Review ON Review.review_id = GuideReview.review_id " +
                                        "GROUP BY Guide.u_id) " +
                                "SELECT TOP 10 guide_ratio.u_id, first_name, last_name, avg_stars " +
                                "FROM guide_ratio JOIN Users ON guide_ratio.u_id = Users.u_id " +
                                "ORDER BY avg_stars DESC;";

                Func<DbDataReader, GuideRating> map = x => new GuideRating
                {
                    uId = (int)x[0],
                    firstName = (string)x[1],
                    lastName = (string)x[2],
                    avgStars = (decimal)x[3],
                };
                Console.Write(finalQuery);
                var topGuides = Helper.RawSqlQuery<GuideRating>(finalQuery, map);

                // Send an HTTP response as data, if necessary
                response.Data = topGuides;
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
