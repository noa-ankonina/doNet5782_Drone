using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
    {
        public class DroneInCharge
        {
            public int ID { get; set; }
            public double battery { get; set; }

       // public DateTime enterToCharge;
            public override string ToString()
            {
                String result = "";
                result += $"ID is: {ID}, {battery} on battery\n";
                return result;
            }
        }
    }
