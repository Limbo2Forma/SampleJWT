using SampleJWT.Filters;
using System.Web.Http;

namespace SampleJWT.Controllers {
    [JwtAuthentication]
    public class ValueController : ApiController
    {
        public string Get() {
            return "value";
        }

        public string Get(int id) {
            return id + " 69";
        }
    }
}
