using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    /// <summary>
    /// Example controller template
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [TokenCheck]
    public class AccountController : ControllerBase
    {

        private readonly TravelAgencyContext dbContext;
        public AccountController()
        {
            if (dbContext == null) dbContext = new TravelAgencyContext();
        }

        /// <summary>
        /// An example http request
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login(LoginInfo userInfo)
        {
            // Create response model
            var response = new ResponseModel();
            try
            {
                // Process incoming data, or return requested data
                var username = userInfo.username;
                var pass = userInfo.password;
                // 1. Check if the credentials are correct

                var user = dbContext.Users.FromSqlInterpolated($"SELECT * FROM Users WHERE username = {userInfo.username} AND pass = {userInfo.password};")
                    .ToList().FirstOrDefault();

                bool exists = user != null;
                // 1.1 If not, send a error response
                if (!exists)
                {
                    response.HasError = true;
                    response.ErrorMessage = "Please check your credentials";
                }
                // 1.2 If yes, send all the user info except their password
                else
                {
                    // Find out the type of the user
                    
                    Customer customer;
                    Guide guide;
                    Agent agent;
                    Employee employee;
                    string type = "";
                    string cAddress = "";
                    decimal? wallet = 0;
                    decimal? salary = 0;
                    customer = dbContext.Customers.FromSqlInterpolated($"SELECT * FROM Customer WHERE u_id = {user.UId};")
                        .ToList().FirstOrDefault();
                    guide = dbContext.Guides.FromSqlInterpolated($"SELECT * FROM Guide WHERE u_id = {user.UId};")
                        .ToList().FirstOrDefault();
                    agent = dbContext.Agents.FromSqlInterpolated($"SELECT * FROM Agent WHERE u_id = {user.UId};")
                        .ToList().FirstOrDefault();
                    employee = dbContext.Employees.FromSqlInterpolated($"SELECT * FROM Employee WHERE u_id = {user.UId};")
                        .ToList().FirstOrDefault();


                    bool isCustomer = customer != null;
                    bool isGuide = guide != null;
                    bool isAgent = agent != null;

                    if (isCustomer)
                    {
                        type = "customer";
                        cAddress = customer.CAddress;
                        wallet = customer.Wallet;
                    }
                    else if (isGuide)
                    {
                        type = "guide";
                        salary = employee.Salary;
                    }
                    else if (isAgent)
                    {
                        type = "agent";
                        salary = employee.Salary;
                    }
                    else
                    {
                        response.HasError = true;
                        response.ErrorMessage = "This user is neither a customer, nor an agent, nor a guide.";
                        return Ok(response);
                    }

                    // Don't send password to frontend
                    switch (type)
                    {
                        case "customer":
                            response.Data = new
                            {
                                uId = user.UId,
                                firstName = user.FirstName,
                                lastName = user.LastName,
                                birthDate = user.BirthDate, // need to store birth date instead.
                                email = user.Email,
                                phoneNumber = user.PhoneNumber,
                                username = user.Username,
                                type = type,
                                cAddress = cAddress,
                                wallet = wallet,
                            };
                            break;
                        default:
                            response.Data = new
                            {
                                uId = user.UId,
                                firstName = user.FirstName,
                                lastName = user.LastName,
                                birthDate = user.BirthDate, // need to store birth date instead.
                                email = user.Email,
                                phoneNumber = user.PhoneNumber,
                                username = user.Username,
                                type = type,
                                salary = salary,
                            };
                            break;
                    }
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

        [HttpPost("signup")]
        public IActionResult Signup(RegisterInfo userInfo)
        {
            // Create response model
            var response = new ResponseModel();
            try
            {
                // TODO check if the username is unique

                var user = new User
                {
                    FirstName = userInfo.firstName,
                    LastName = userInfo.lastName,
                    Email = userInfo.email,
                    PhoneNumber = userInfo.phoneNumber,
                    Username = userInfo.username,
                    Pass = userInfo.password,
                    BirthDate = userInfo.birthDate
                };
                switch (userInfo.type) {
                    case "customer":
                        break;
                    case "agent":
                        break;
                    case "guide":
                        break;
                    default:
                        response.HasError = true;
                        response.ErrorMessage = "The user trying to sign up must be either a customer, agent or a guide.";
                        return Ok(response);

                }
                int maxId;
                string countQuery = "SELECT COUNT(*) FROM Users;";
                string maxQuery = "SELECT MAX(u_id) FROM Users;";
                Func<DbDataReader, int> mapInt = x => (int)x[0];
                int count = Helper.RawSqlQuery<int>(countQuery, mapInt).SingleOrDefault();
                if (count > 0)
                {
                    maxId = Helper.RawSqlQuery<int>(maxQuery, mapInt).SingleOrDefault();
                }
                else {
                    maxId = 0;
                }
                var newId = maxId + 1;
                user.UId = newId;

                dbContext.Database.ExecuteSqlInterpolated($"INSERT INTO Users VALUES({user.UId}, {user.FirstName}, {user.LastName}, {user.Email}, {user.PhoneNumber}, {user.Username}, {user.Pass}, {user.BirthDate});");

                switch (userInfo.type)
                {
                    case "customer":
                        dbContext.Database.ExecuteSqlInterpolated($"INSERT INTO Customer VALUES({newId}, '', 0);");
                        break;
                    case "agent":
                        dbContext.Database.ExecuteSqlInterpolated($"INSERT INTO Employee VALUES({newId}, 0);");
                        dbContext.Database.ExecuteSqlInterpolated($"INSERT INTO Agent VALUES({newId});");
                        break;
                    case "guide":
                        dbContext.Database.ExecuteSqlInterpolated($"INSERT INTO Employee VALUES({newId}, 0);");
                        dbContext.Database.ExecuteSqlInterpolated($"INSERT INTO Guide VALUES({newId});");
                        break;
                    default:
                        break;
                }
                response.Data = new
                {
                    uId = newId,
                    firstName = userInfo.firstName,
                    lastName = userInfo.lastName,
                    email = userInfo.email,
                    phoneNumber = userInfo.phoneNumber,
                    username = userInfo.username,
                    password = userInfo.password,
                    birthDate = userInfo.birthDate,
                    type = userInfo.type,
                };
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
            // Return the response model - send the HTTP response
            return Ok(response);
        }



    }
}

