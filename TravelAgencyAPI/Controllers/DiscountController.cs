using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAgencyAPI.Controllers
{
    public class DiscountController
    {
        /// <summary>
        /// An example http request
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpGet("action")] // Please set the http request type, and http request name.
        public IActionResult Action(LoginInfo userInfo)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                // SQL Queries here

                // If you want to send an error to the frontend,
                // you can set response.HasError = true
                // set a ErrorMessage and just return Ok(response)

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
