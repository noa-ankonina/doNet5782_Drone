using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
    {
        public class DroneToList
        {
            public int ID { get; set; }
            public int droneModel { get; set; }
            public WeightCategorie weight { get; set; }
            public double battery { get; set; }
            public DroneStatus status { get; set; }
            public Location currentLocation { get; set; }
            public int parcelNumber { get; set; }
            public override string ToString()
            {
                String result = "";
                result += $"ID is: {ID}, drone model is: {droneModel}, weight categories is: {weight}, " +
                    $" drone status is: {status}, {battery}% on battery, current location is:{currentLocation}, " +
                    $"number of parcel in transfort: {parcelNumber} \n";
                return result;

            }
        }
    }
