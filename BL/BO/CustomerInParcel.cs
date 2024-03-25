using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
    {
         public class CustomerInParcel
        {
        public bool active { get; set; }

        public int ID { get; set; }
            public string customerName { get; set; }
            public override string ToString()
            {
                String result = "";
                result += $"ID is: {ID},  customer name is: {customerName}\n";
                return result;
            }
        }
    }
