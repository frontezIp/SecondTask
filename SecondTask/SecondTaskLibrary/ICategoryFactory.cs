using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondTaskLibrary
{
    interface ICategoryFactory
    {
        /// <summary>
        /// Creates category according to given type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        CategoryOfSupply Create(string type);
    }
}
