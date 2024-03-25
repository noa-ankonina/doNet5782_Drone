using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
    {
       public class Station
        {
            public int ID { get; set; }
            public string name { get; set; }
            public Location location { get; set; }
            public int chargeSlots { get; set; }
            public List<DroneInCharge> dronesInChargeList { get; set; }
            public override string ToString()
            {
                String result = "";
                result += $"ID is: {ID}, Name is: {name}, location is: {location}, charge slolts is: {chargeSlots}," +
                          $"drone in charge:";
                foreach (var item in dronesInChargeList)
                {
                    result += item+"\n";
                }
                         
                return result;
            }
        }
    }
