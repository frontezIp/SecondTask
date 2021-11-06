using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondTaskLibrary
{
    interface ITractorFactory
    {
        /// <summary>
        /// Creates tractor according to given type and specifications
        /// </summary>
        /// <param name="type"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        AutoStation.AutoPark.Tractor create(string type, ModelOfTractor model);
    }
}
