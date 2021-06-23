using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

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
        public void Put([FromBody] MusicFestival[] value)
        {
            TestData.Festivals = value;
        }
    }
}
