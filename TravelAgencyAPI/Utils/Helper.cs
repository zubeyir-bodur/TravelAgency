using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using TravelAgencyEntity;

namespace TravelAgencyAPI.Utils
{
    public class Helper
    {
        /// <summary>
        /// We will use this helper function whenever we need to execute a complex query,
        /// since .NET Core does not support executing any select query with a custom format
        /// To use it, input the query string, and mup function
        /// However, two things needs to be done:
        /// 1- The output of the query should be a defined class, that is T.
        /// 2- Second input parameter should be defined to "map" the DBDataReader values
        ///     example usage of mapping is in BookingController
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static List<T> RawSqlQuery<T>(string query, Func<DbDataReader, T> map)
        {
            using (var context = new TravelAgencyContext())
            {
                using (var command = context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    context.Database.OpenConnection();

                    using (var result = command.ExecuteReader())
                    {
                        var entities = new List<T>();

                        while (result.Read())
                        {
                            entities.Add(map(result));
                        }

                        return entities;
                    }
                }
            }
        }
    }
}
