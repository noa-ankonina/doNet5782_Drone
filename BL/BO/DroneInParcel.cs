using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
    {
        public class DroneInParcel
        {
            public int ID { get; set; }
            public double battery { get; set; }
            public Location currentLocation { get; set; }
            public override string ToString()
            {
                String result = "";
                result += $"ID is: {ID}, {battery} on battery, current location: {currentLocation}\n";
                return result;
            }
        }
    }
