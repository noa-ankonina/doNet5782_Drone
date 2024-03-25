using System;
using DalApi;
using System.Runtime.CompilerServices;

namespace Dal
{
    sealed partial class DalObject : DalApi.IDal
    {
        //singelton

        static readonly IDal instance = new DalObject();
        public static IDal Instance { get { return instance; } }

        /// <summary>
        /// constructor
        /// </summary>
        private DalObject() { DAL.DataSource.Initialize(); }

        /// <summary>
        /// reurn array of five values ​​in the following order: free, light, medium, heavy and load rate
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public double[] chargeCapacity()
        {
            double[] arr = new double[] { DAL.DataSource.Config.chargeClear,
                                                  DAL.DataSource.Config.chargeLightWeight,
                                                  DAL.DataSource.Config.chargeMediumWeight,
                                                  DAL.DataSource.Config.chargeHavyWeight,
                                                  DAL.DataSource.Config.chargineRate };
            return arr;
        }



       

    }
}

