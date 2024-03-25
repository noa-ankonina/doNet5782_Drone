using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
    {
       public class StationToList
        {
            public int ID { get; set; }
            public string name { get; set; }
            public int availableChargeSlots { get; set; }
            public int notAvailableChargeSlots { get; set; }
            public override string ToString()
            {
                String result = "";
                result += $"ID is: {ID}, Name is: {name}, avilable charge slots: {availableChargeSlots}, not avilable charge slots: {notAvailableChargeSlots}\n";
                return result;
            }
        }
    }
