using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ParcelAtCustomer
    {
        public int ID { get; set; }
        public WeightCategorie weight { get; set; }
        public Priorities priority { get; set; }
        public ParcelStatus status { get; set; }
        public CustomerInParcel senderOrTarget { get; set; }//if the parcel need to be sent its the terget and if not its the sender
        public bool confirmation { get; set; }//confirmation on recipt of shipment orconfirmation of parcel collection

        public override string ToString()
        {
            String result = "";
            result += $"ID is: {ID}, weight is: {weight}, priority is: {priority}, " +
                      $"status is: {status}, " +
                      $"customer is: {senderOrTarget}, \n";
            return result;
        }
    }
}
