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
    public class ReservationController : ControllerBase
    {
        private readonly TravelAgencyContext dbContext;
        public ReservationController()
        {
            if (dbContext == null) dbContext = new TravelAgencyContext();
        }

        /// <summary>
        /// An example http request
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpGet("tourReservations")]
        public IActionResult TourReservations(int uId)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                // SQL Queries here
                string tReservationQ = "SELECT Reservation.reserve_id, reserve_start_date, reserve_end_date, num_reserving, tour_name, Tour.tour_id, 	" +
                                            "CASE WHEN percents IS NOT NULL THEN price*(100.0-percents)/100 " +
                                            "WHEN percents IS NULL THEN price END AS price, is_booked " +
                                      "FROM Reservation JOIN TourReservation ON Reservation.reserve_id = TourReservation.reserve_id " +
                                                                "JOIN Tour ON Tour.tour_id = TourReservation.tour_id " +
                                                                "LEFT JOIN Discount ON Tour.discount_id=Discount.discount_id  WHERE u_id="+ uId +";";
                
                Func<DbDataReader, TourReservationDTO> map = x => new TourReservationDTO
                {
                    reserveId = (int)x[0],
                    reserveStartDate = (DateTime)x[1],
                    reserveEndDate = (DateTime)x[2],
                    numReserving = (int)x[3],
                    tourName = (string)x[4],
                    tourId = (int)x[5],
                    price = (decimal)x[6],
                    isBooked = (bool)x[7],
                };
                var output = Helper.RawSqlQuery<TourReservationDTO>(tReservationQ, map).ToList();
                // Send an HTTP response as data, if necessary
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
        /// An example http request
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpGet("hotelReservations")] // Please set the http request type, and http request name.
        public IActionResult HotelReservations(int uId)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                // SQL Queries here
                string hReservationQ = "SELECT Reservation.reserve_id, reserve_start_date, reserve_end_date, num_reserving, hotel_name, number,	CASE " +
                                                        "WHEN percents IS NOT NULL THEN price*(100.0-percents)/100		" +
                                                        "WHEN percents IS NULL THEN price END AS price_per_day, is_booked " +
                                        "FROM Reservation JOIN HotelReservation ON Reservation.reserve_id = HotelReservation.reserve_id " +
                                            "JOIN Room ON Room.room_id = HotelReservation.room_id AND Room.hotel_id = HotelReservation.hotel_id	" +
                                            "JOIN Hotel ON Hotel.hotel_id=Room.hotel_id	" +
                                            "LEFT JOIN Discount ON Hotel.discount_id=Discount.discount_id WHERE u_id=" + uId +  ";";

                Func<DbDataReader, HotelReservationDTO> map = x => new HotelReservationDTO
                {
                    reserveId = (int)x[0],
                    reserveStartDate = (DateTime)x[1],
                    reserveEndDate = (DateTime)x[2],
                    numReserving = (int)x[3],
                    hotelName = (string)x[4],
                    roomNumber = (int)x[5],
                    price = (decimal)x[6],
                    isBooked = (bool)x[7]
                };
                var output = Helper.RawSqlQuery<HotelReservationDTO>(hReservationQ, map).ToList();
                // Send an HTTP response as data, if necessary
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
        /// An example http request
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpGet("markedActivities")] // Please set the http request type, and http request name.
        public IActionResult MarkedActivities(int reserveId, int uId)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                // SQL Queries here
                string hReservationQ = " WITH tourReservations(reserve_id, reserve_start_date, reserve_end_date, num_reserving, tour_name, tour_id, price, is_booked) AS ( " +
                                            " SELECT Reservation.reserve_id, reserve_start_date, reserve_end_date, num_reserving, tour_name, Tour.tour_id, " +
                                                " CASE WHEN percents IS NOT NULL THEN price*(100.0 - percents) / 100 " +
                                                " WHEN percents IS NULL THEN price END AS price, is_booked " +
                                            " FROM Reservation JOIN TourReservation ON Reservation.reserve_id = TourReservation.reserve_id " +
                                            " JOIN Tour ON Tour.tour_id = TourReservation.tour_id " +
                                            " LEFT JOIN Discount ON Tour.discount_id = Discount.discount_id  WHERE u_id = " + uId + " ) " +
                                        " SELECT tourReservations.tour_id, activity_name, activity_start_time, activity_end_time,  " + 
                                        " num_reserving, tour_name,CASE WHEN percents IS NOT NULL THEN ticket_price*(100.0 - percents) / 100 " + 
                                                                                    " WHEN percents IS NULL THEN ticket_price END AS ticket_price, is_booked " +
                                        " FROM tourReservations JOIN marked_activity ON tourReservations.reserve_id = marked_activity.reserve_id " +
                                                              " JOIN Activity ON marked_activity.activity_id = Activity.activity_id " +
                                                              " LEFT JOIN Discount ON Activity.discount_id = Discount.discount_id " +
                                        " WHERE tourReservations.reserve_id = " + reserveId + " ; ";

                Func<DbDataReader, MarkedActivityDTO> map = x => new MarkedActivityDTO
                {
                    tourId = (int)x[0],
                    activityName = (string)x[1],
                    activityStartTime = (DateTime)x[2],
                    activityEndTime = (DateTime)x[3],
                    numReserving = (int)x[4],
                    tourName = (string)x[5],
                    ticketPrice = (decimal)x[6],
                    isBooked = (bool)x[7]
                };
                var output = Helper.RawSqlQuery<MarkedActivityDTO>(hReservationQ, map).ToList();
                // Send an HTTP response as data, if necessary
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
        /// An example http request
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpGet("customer")] // Please set the http request type, and http request name.
        public IActionResult Action()
        {
            ResponseModel response = new ResponseModel();
            try
            {
                // SQL Queries here

                // If you want to send an error to the frontend,
                // you can set response.HasError = true
                // set a ErrorMessage and just return Ok(response)
                string query = "select Users.u_id, first_name, last_name from Users join Customer " +
                    " ON Customer.u_id=Users.u_id where Users.u_id in (select  u_id from Customer); ";
                Func<DbDataReader, CustomerDTO> map = x => new CustomerDTO
                {
                    u_id = (int)x[0],
                    first_name = (string)x[1],
                    last_name = (string)x[2],
                };
                var tours = Helper.RawSqlQuery<CustomerDTO>(query, map).ToList();

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
