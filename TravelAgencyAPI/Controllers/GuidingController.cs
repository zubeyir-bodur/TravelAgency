﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgencyAPI.Utils;
using TravelAgencyEntity;

namespace TravelAgencyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TokenCheck]
    public class GuidingController : ControllerBase
    {
        private readonly TravelAgencyContext dbContext;
        public GuidingController()
        {
            if (dbContext == null) dbContext = new TravelAgencyContext();
        }

        /// <summary>
        /// List all tours without an assigned guide. (employee)
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost("toursWithoutGuide")]
        public IActionResult ToursWithoutGuide()
        {
            ResponseModel response = new ResponseModel();
            try
            {
                string finalQuery = "SELECT tour_id, tour_name, city, tour_start_date, tour_end_date, tour_description, price, percents FROM Tour WHERE tour_id NOT IN (SELECT tour_id FROM AssignGuide WHERE assign_status != null AND assign_status != "" ); ""
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
        /// Select a tour and assign an available guide. (employee)
        /// </summary>
        /// <param name="tourId"></param>
        /// <param name="guideUId"></param>
        /// <returns></returns>
        [HttpGet("assignGuideForTour")]
        public IActionResult AssignGuideForTour(int tourId, int guideUId)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                // SQL Queries here
             
                var tour = dbContext.Tours.FromSqlRaw("SELECT * FROM Tour WHERE Tour_Id = " + tourId + ";").ToList().FirstOrDefault();
                string finalQuery = "SELECT * FROM Tour WHERE tour_id = " + tourId + ";";
                Func<DbDataReader, AssignGuideDTO> map = x => new AssignGuideDTO
                {
                    tourId = (int)x[0],
                    guideUId = (int)x[1],
                    agentUId = (int)x[2],
                    assignStatus = (string)x[3],
                };
                var assignGuide = Helper.RawSqlQuery<AssignGuideDTO>(finalQuery, map);

                dbContext.Database.ExecuteSqlInterpolated($"INSERT INTO AssignGuides VALUES({tourId}, {guideUId}, {agentUId}, {"assigned"});");

                // Send an HTTP response as data, if necessary
                response.Data = assignGuide;
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
        ///  List all available assigned tours. (guide)
        /// </summary>
        /// <param name="uId"></param>
        /// <returns></returns>
        [HttpPost("assignedTours")]
        public IActionResult AssignedTours(int uId)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                string finalQuery = "SELECT tour_id, tour_name, city, tour_start_date, tour_end_date, tour_description, price, percents FROM Tour WHERE tour_id IN (SELECT tour_id FROM AssignGuide WHERE guide_uid = " + uid + "); ";
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
    }
}
