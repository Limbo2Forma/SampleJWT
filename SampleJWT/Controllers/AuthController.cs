using SampleJWT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SampleJWT.Controllers {
    public class AuthController : ApiController {
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Get([FromBody] User user) {
            if (CheckUser(user.name, user.password)) {
                return Ok<string>(JwtManager.GenerateToken(user.name));
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        public bool CheckUser(string username, string password) {
            // should check in the database
            return true;
        }
    }
}
