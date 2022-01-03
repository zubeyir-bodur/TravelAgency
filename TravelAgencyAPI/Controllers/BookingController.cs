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
        /// Display hotels, add some like condition just to be safe
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        [HttpGet("hotels")] 
        public IActionResult Hotels(int tourId)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var tourCity = dbContext.Tours.FromSqlRaw("SELECT * FROM Tour WHERE tour_id = " + tourId + ";").ToList().FirstOrDefault().City;
                string finalQuery = "SELECT * FROM Hotel WHERE city LIKE '%" + tourCity + "%' ";
                Func<DbDataReader, HotelDTO> map = x => new HotelDTO
                {
                    hotelId = (int)x[0],
                    hotelName = (string)x[1],
                    city = (string)x[2],
                    numOfStars = (int)x[3],
                    discountPercents = (x[4] != DBNull.Value) ? ((int)x[4]) : 0 // if percents is null, then the discount applied is zero percent
                };
                var hotels = Helper.RawSqlQuery<HotelDTO>(finalQuery, map);

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


        /// <summary>
        /// Display activities
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        [HttpGet("activities")]
        public IActionResult Activities(int tourId)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                // SQL Queries here

                // If you want to send an error to the frontend,
                // you can set response.HasError = true
                // set a ErrorMessage and just return Ok(response)
                string finalQuery = "SELECT activity_id, activity_name, a_description, activity_start_time, activity_end_time, ticket_price, percents " +
                                    "FROM Activity LEFT JOIN Discount ON Activity.discount_id = Discount.discount_id " +
                                    "WHERE tour_id = " + tourId + ";";
                Func<DbDataReader, ActivityDTO> map = x => new ActivityDTO
                {
                    activityId = (int)x[0],
                    activityName = (string)x[1],
                    aDescription = (string)x[2],
                    activityStartTime = (DateTime)x[3],
                    activityEndTime = (DateTime)x[4],
                    ticketPrice = (decimal)x[5],
                    discountPercents = (x[6] != DBNull.Value) ? ((int)x[6]) : 0 // if percents is null, then the discount applied is zero percent
                };
                var activities = Helper.RawSqlQuery<ActivityDTO>(finalQuery, map).ToList();
                // Send an HTTP response as data, if necessary
                response.Data = activities;
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
        /// Make payment
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        [HttpGet("rooms")]
        public IActionResult Room(int hotelId)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                // Need to join with discount and hotels, to compute discounted price
                string finalQuery = "WITH table_(room_id, number, size, price, percents) AS( " +
                                        "SELECT room_id, number, size, price, percents " +
                                        "FROM Room JOIN Hotel ON Room.hotel_id=Hotel.hotel_id " +
                                                        "LEFT JOIN Discount ON Discount.discount_id=Hotel.discount_id " +
                                        "WHERE Room.hotel_id = " + hotelId + ") " +
                                    "SELECT room_id, number, size, price, price*(100-percents)/100 AS price_discounted " +
                                    "FROM table_;";
                Func<DbDataReader, RoomDTO> map = x => new RoomDTO
                {
                    roomId = (int)x[0],
                    number = (int)x[1],
                    size = (int)x[2],
                    price = (decimal)x[3],
                    priceDiscounted = (x[4] == DBNull.Value)?((decimal)x[3]):((decimal)x[4])
                };
                var output = Helper.RawSqlQuery<RoomDTO>(finalQuery, map).ToList();

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
        /// Make a payment.
        /// No validation needed, frontend already knows if customer have enough balance or not
        /// </summary>
        /// <param name="paymentInfo"></param>
        /// <returns></returns>
        [HttpPut("payment")]
        public IActionResult Payment(PaymentInfo paymentInfo)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                // SQL Queries here 
                dbContext.Database.ExecuteSqlInterpolated($"UPDATE Customer SET wallet = wallet - {paymentInfo.total} WHERE Customer.u_id = {paymentInfo.uId};");

                // Send an HTTP response as data, if necessary
                response.Data = null;
                response.HasError = false;
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
        /// Insert necessary rows for a tour reservation, without activities
        /// No validation needed, frontend already knows if customer have enough balance or not
        /// </summary>
        /// <param name="tourReservationInfo"></param>
        /// <returns></returns>
        [HttpPost("tourReservation")]
        public IActionResult TourReservation(TourReservationInfo tourReservationInfo)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                int maxId;
                string countQuery = "SELECT COUNT(*) FROM Reservation;";
                string maxQuery = "SELECT MAX(reserve_id) FROM Reservation;";
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
                int tourReserveId = newId;
                // todo find available id 
                // SQL Queries here 
                string queryR = "INSERT INTO " +
                                                    " Reservation(reserve_id, reserve_start_date, " +
                                                    " reserve_end_date, num_reserving, is_booked, u_id) " +
                                                    " VALUES(" + tourReserveId + ", \'" +
                                                                tourReservationInfo.tourStartDate.ToString("s").Substring(0, 10) + "\', \'" +
                                                                tourReservationInfo.tourEndDate.ToString("s").Substring(0, 10) + "\', " +
                                                                tourReservationInfo.numPeople + ", " +
                                                                (tourReservationInfo.isBooked ? (1) : (0)) + ", " +
                                                                tourReservationInfo.uId + ");";
                string queryTR = "INSERT INTO " +
                                                    " TourReservation(reserve_id, tour_id) " +
                                                    " VALUES( " + tourReserveId + ", " +
                                                                tourReservationInfo.tourId + ");";
                dbContext.Database.ExecuteSqlRaw(queryR);
                dbContext.Database.ExecuteSqlRaw(queryTR);

                // Send the tourReserveId for marked activities
                response.Data = tourReserveId;
                response.HasError = false;
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
        /// Insert necessary rows for a single hotel reservation, 
        /// This method will be called inside a loop to insert all necessary rows for chosen rooms
        /// </summary>
        /// <param name="hotelReservationInfo"></param>
        /// <returns></returns>
        [HttpPost("hotelReservation")]
        public IActionResult HotelReservation(HotelReservationInfo hotelReservationInfo)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                int maxId;
                string countQuery = "SELECT COUNT(*) FROM Reservation;";
                string maxQuery = "SELECT MAX(reserve_id) FROM Reservation;";
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
                int hotelReservationId = newId;
                // SQL Queries here 
                dbContext.Database.ExecuteSqlRaw("INSERT INTO " +
                                                    " Reservation(reserve_id, reserve_start_date, " +
                                                    " reserve_end_date, num_reserving, is_booked, u_id) " +
                                                    " VALUES(" + hotelReservationId + ", \'" +
                                                                hotelReservationInfo.tourStartDate.ToString("s").Substring(0, 10) + "\', \'" +
                                                                hotelReservationInfo.tourEndDate.ToString("s").Substring(0, 10) + "\', " +
                                                                hotelReservationInfo.numPeople + ", " +
                                                                (hotelReservationInfo.isBooked ? (1) : (0)) + ", " +
                                                                hotelReservationInfo.uId + ");");
                dbContext.Database.ExecuteSqlRaw("INSERT INTO " +
                                                    " HotelReservation(reserve_id, room_id, hotel_id) " +
                                                    " VALUES(" + hotelReservationId + ", " +
                                                                hotelReservationInfo.roomId + ", " +
                                                                hotelReservationInfo.hotelId + ");");

                // Send an HTTP response as data, if necessary
                response.Data = null;
                response.HasError = false;
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
        /// Insert necessary rows to indicate a single activity user chose to participate
        /// </summary>
        /// <param name="markedActivityInfo"></param>
        /// <returns></returns>
        [HttpPost("markedActivity")]
        public IActionResult MarkedActivity(MarkedActivityInfo markedActivityInfo)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                // SQL Queries here 
                dbContext.Database.ExecuteSqlRaw("INSERT INTO marked_activity(reserve_id, activity_id) " +
                                                    " VALUES( " + markedActivityInfo.reserveId + ", " +
                                                                markedActivityInfo.activityId + ");");

                // Send an HTTP response as data, if necessary
                response.Data = null;
                response.HasError = false;
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
