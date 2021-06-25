using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalsStub
{
    static class TestData
    {
        //
        // Test data is configured so that if NO test data has been loaded we make this visible by returning this in the call.....
        //
        //

        static private MusicFestival[] festivals;
        static public MusicFestival[] Festivals
        {
            set
            {
                festivals = value;
            }
            get
            {
                return festivals;
            }
        }
    }

    //
    // This MusicFestival JSON class would probably be provided by the API dev team - OR obtained from swagger.  As Tester, I would prefer to get from API Project team as that
    // would define how the API schema should be rather than what they have implemented.....
    //
    public class MusicFestival
    {
        public string name { get; set; }
        public Band[] bands { get; set; }
    }

    public class Band
    {
        public string name { get; set; }
        public string recordLabel { get; set; }
    }
}
