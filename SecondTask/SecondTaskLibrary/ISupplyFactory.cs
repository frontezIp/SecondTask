using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondTaskLibrary
{
    public interface ISupplyFactory
    {
        /// <summary>
        /// Creates supply according to given type and specifications
        /// </summary>
        /// <param name="type"></param>
        /// <param name="categories"></param>
        /// <param name="weight"></param>
        /// <param name="count"></param>
        /// <param name="tempretureMin"></param>
        /// <param name="tempretureMax"></param>
        /// <returns></returns>
        Supply create(string type,List<CategoryOfSupply> categories,double weight, int count, int tempretureMin, int tempretureMax);
    }
}
