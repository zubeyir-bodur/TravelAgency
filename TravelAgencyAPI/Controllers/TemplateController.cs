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

namespace TravelAgencyAPI.Controllers
{
    /// <summary>
    /// No need to change these
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [TokenCheck]
    public class TemplateController : ControllerBase
    {
        private readonly TravelAgencyContext dbContext;
        public TemplateController()
        {
            if (dbContext == null) dbContext = new TravelAgencyContext();
        }

        /// <summary>
        /// An example http request
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpGet("action")] // Please set the http request type, and http request name.
        public IActionResult Action()
        {
            ResponseModel response = new ResponseModel();
            try
            {
                // SQL Queries here

                // If you want to send an error to the frontend,
                // you can set response.HasError = true
                // set a ErrorMessage and just return Ok(response)

                var tours = dbContext.Tours.FromSqlRaw("SELECT * FROM Tours").ToList();

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
