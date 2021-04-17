using DemoProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DemoProject.Controller
{
    public class BaseApiController : ApiController
    {
        /// <summary>Creates response.</summary>
        /// <param name="responseInfo">BaseRes</param>
        /// <returns>IActionResult</returns>
        [NonAction]
        public IHttpActionResult CreateResponse<T>(T responseInfo) where T : BaseRes
        {
            if (responseInfo != null)
            {
                HttpRequest request = HttpContext.Current.Request;

                if (request.HttpMethod == HttpMethod.Delete.Method)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else
                {
                    return Ok(responseInfo);
                }

            }           
            else
            {
                return BadRequest();
            }
        }
    }
}
