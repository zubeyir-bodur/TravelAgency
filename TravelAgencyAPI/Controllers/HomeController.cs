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
    /// Example controller template
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [TokenCheck]
    public class HomeController : ControllerBase
    {
        
        private readonly TravelAgencyContext dbContext;
        public HomeController()
        {
            if (dbContext == null) dbContext = new TravelAgencyContext();
        }

        /// <summary>
        /// An example http request
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login(UserInfo userInfo)
        {
            // Create response model
            var response = new ResponseModel();
            try
            {
                // Process incoming data, or return requested data
                var username = userInfo.username;
                var pass = userInfo.password;

                // 1. Check if the credentials are correct
                var user = dbContext.Users.AsNoTracking()
                    .FirstOrDefault(u => u.Username == userInfo.username && u.Pass == userInfo.password);
                bool exists = user != null;
                Console.WriteLine(exists);
                // 1.1 If not, send a error response
                if (!exists)
                {
                    Console.WriteLine("nope");
                    response.HasError = true;
                    response.ErrorMessage = "Please check your credentials";
                }
                // 1.2 If yes, send all the user info except their password
                else
                {
                    // Don't send password to frontend
                    Console.WriteLine("yep");
                    response.Data = new
                    {
                        uId= user.UId,
                        firstName = user.FirstName,
                        lastName = user.LastName,
                        age = user.Age, // need to store birth date instead.
                        email = user.Email,
                        phoneNumber = user.PhoneNumber,
                        username = user.Username
                    };
                }
            }
            catch (Exception ex)
            {
                // If an error occurs, return an error to the response model
                response.HasError = true;
                response.ErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    response.ErrorMessage += ": " + ex.InnerException.Message;
                }
            }
            // Return the response model -send the HTTP response
            return Ok(response);
        }
    }
}
