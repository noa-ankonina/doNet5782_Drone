using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
    {
         public class ParcelToList
        {
            public int ID { get; set; }
            public string senderName { get; set; }
            public string targetName { get; set; }
            public WeightCategorie weight { get; set; }
            public Priorities priority { get; set; }
            public ParcelStatus status { get; set; }
            public override string ToString()
            {
                String result = "";
                result += $"ID is: {ID}, status is: {status},  " +
                          $"weight is: {weight}, priority is: {priority}, " +
                          $"The name of the sender is:{senderName},The name of the target customer is:{targetName}";


                return result;
            }
        }
    }
