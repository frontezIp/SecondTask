using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondTaskLibrary
{
    public interface ISemitrailerFactory
    {
        /// <summary>
        /// Creates semitrailer according to given type and specifications
        /// </summary>
        /// <param name="type"></param>
        /// <param name="categories"></param>
        /// <param name="liftingCacacity"></param>
        /// <param name="volumeOfSemitrailer"></param>
        /// <param name="tempMin"></param>
        /// <param name="tempMax"></param>
        /// <returns></returns>
        AutoStation.AutoPark.Semitrailer Create(string type,List<CategoryOfSupply> categories,double liftingCacacity,int volumeOfSemitrailer,int tempMin = 0 , int tempMax =0);
    }
}
