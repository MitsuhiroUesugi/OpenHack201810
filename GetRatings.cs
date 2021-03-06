using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;

namespace Company.Function
{
    public static class GetRatings
    {
        [FunctionName("GetRatings")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "GetRatings/{userId}")] HttpRequest req,
            [CosmosDB(
                databaseName: "db1023",
                collectionName: "Ratings",
                SqlQuery="select * from c where c.userId={userId}", 
                ConnectionStringSetting = "Cosmos_Db_connect")
                ]
                   IEnumerable<UserRating> userRatings,
            ILogger log)
        {
            if (userRatings.Count() == 0){
                return new BadRequestObjectResult("Please pass a valid userId.");
            }
            return (ActionResult)new OkObjectResult(userRatings);
        }
    }
}