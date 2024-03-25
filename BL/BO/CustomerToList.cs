using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class CustomerToList
    {
        public bool active { get; set; }

        public int ID { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public int sendAndDeliveredParcels { get; set; }
        public int sendAndNotDeliveredParcels { get; set; }
        public int gotParcels { get; set; }
        public int onTheWayParcels { get; set; }
        public override string ToString()
        {
            String result = "";
            result += $"ID: {ID}, Name: {name}, phone number: {phone.Substring(0, 3) + '-' + phone.Substring(3)}, {sendAndDeliveredParcels} Packages were successfully delivered" +
                $", {sendAndNotDeliveredParcels} Packages sending , {gotParcels} Packages received, {onTheWayParcels} on the way'\n";
            return result;
        }
    }
}

