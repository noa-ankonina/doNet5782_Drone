using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
    {
         public class Customer
        {
        public bool active { get; set; }

        public int ID { get; set; }
            public string name { get; set; }
            public string phone { get; set; }
            public Location location { get; set; }
            public List<ParcelAtCustomer> fromCustomer { get; set; }
            public List<ParcelAtCustomer> toCustomer { get; set; }
            public override string ToString()
            {
                String result = "";
                result += $"ID is: {ID}, Name is: {name}, Telephone is: {phone.Substring(0, 3) + '-' + phone.Substring(3)}," +
                    $"Lcation is: {location}\n";
                if (fromCustomer != null && fromCustomer.Count > 0)
                {
                    result += "parcel from customer:\n";
                    foreach (var item in fromCustomer)
                    {
                        result += item + "\n";
                    }
                }
                if (toCustomer != null && toCustomer.Count > 0)
                {
                    result += "parcel to customer:\n";
                    foreach (var item in toCustomer)
                    {
                        result += item + "\n";
                    }
                }
                return result;
            }
        }
    }
