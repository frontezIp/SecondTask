using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondTaskLibrary
{
    class TractorFactory: ITractorFactory
    {
        public AutoStation.AutoPark.Tractor create(string type,ModelOfTractor model)
        {
            switch (type)
            {
                case "TractorOne":
                    return new TractorOne(model);
                    break;

                default:
                    throw new Exception("No such types of tractors");
            }
        }
    }
}
