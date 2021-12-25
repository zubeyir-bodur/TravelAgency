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
                    // Find out the type of the user
                    bool isCustomer = false;
                    bool isGuide = false;
                    bool isAgent = false;
                    string type = "";
                    isCustomer = dbContext.Customers.AsNoTracking().FirstOrDefault(c => c.UId == user.UId) != null;
                    isGuide = dbContext.Guides.AsNoTracking().FirstOrDefault(g => g.UId == user.UId) != null;
                    isAgent = dbContext.Agents.AsNoTracking().FirstOrDefault(a => a.UId == user.UId) != null;
                    if (isCustomer)
                    {
                        type = "customer";
                    }
                    else if (isGuide)
                    {
                        type = "guide";
                    }
                    else if (isAgent)
                    {
                        type = "agent";
                    }
                    else
                    {
                        response.HasError = true;
                        response.ErrorMessage = "This user is neither a customer, nor an agent, nor a guide.";
                        return Ok(response);
                    }

                    // Don't send password to frontend
                    Console.WriteLine("yep");
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
                                cAddress = dbContext.Customers.AsNoTracking().FirstOrDefault(c => c.UId == user.UId).CAddress,
                                wallet = dbContext.Customers.AsNoTracking().FirstOrDefault(c => c.UId == user.UId).Wallet,
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
                                salary = dbContext.Employees.AsNoTracking().FirstOrDefault(e => e.UId == user.UId).Salary,
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
                dbContext.Add<User>(user).State = EntityState.Added;
                dbContext.SaveChanges();

                // Get the UId assigned by the framework
                int UId = dbContext.Users.FirstOrDefault(u => u.Username == user.Username).UId;
                switch (userInfo.type)
                {
                    case "customer":
                        var customer = new Customer {
                            UId = UId
                        };
                        dbContext.Add<Customer>(customer).State = EntityState.Added;
                        dbContext.SaveChanges();
                        break;
                    case "agent":
                        var agent = new Agent
                        {
                            UId = UId
                        };
                        var employeeA = new Employee
                        {
                            UId = UId
                        };
                        dbContext.Add<Employee>(employeeA).State = EntityState.Added;
                        dbContext.SaveChanges();
                        dbContext.Add<Agent>(agent).State = EntityState.Added;
                        dbContext.SaveChanges();
                        break;
                    case "guide":
                        var guide = new Guide
                        {
                            UId = UId
                        };
                        var employeeG = new Employee
                        {
                            UId = UId
                        };
                        dbContext.Add<Employee>(employeeG).State = EntityState.Added;
                        dbContext.SaveChanges();
                        dbContext.Add<Guide>(guide).State = EntityState.Added;
                        dbContext.SaveChanges();
                        break;
                    default:
                        break;
                }
                response.Data = new
                {
                    uId = UId,
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

