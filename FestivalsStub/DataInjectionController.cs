using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace FestivalsStub
{
    public class DataInjectionController : ApiController
    {
        /// <summary>
        /// Consume PUT call and store MusicFestival data in Test Data
        /// </summary>
        /// <remarks>
        /// This will only accept a well-formed JSON value.  If tests need to be able to use badly-formed json data then we would hand-roll the Web HTTP controller to allow
        /// badly formed data to be accepted and stored and then returned when call to Festivals GET made.  Quite simple but time here is of importance so not done.
        /// </remarks>
        /// <param name="value">Well-formed MusicFestival test data</param>
        public HttpResponseMessage Put([FromBody] MusicFestival[] value)
        {
            Console.WriteLine("PUT Called");
            try
            {
                TestData.Festivals = null;
                TestData.Festivals = value;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            if (TestData.Festivals==null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Error loading JSON");
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
