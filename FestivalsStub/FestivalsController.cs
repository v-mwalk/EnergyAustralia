using System.Collections.Generic;
using System.Web.Http;

namespace FestivalsStub
{
    public class FestivalsController : ApiController
    {
        public MusicFestival[] Get()
        {
            return TestData.Festivals;
        }
    }
}
