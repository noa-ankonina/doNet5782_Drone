using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Parcel
    {
        public int ID { get; set; }
        public CustomerInParcel sender { get; set; }
        public CustomerInParcel target { get; set; }
        public WeightCategorie weight { get; set; }
        public Priorities priority { get; set; }
        public DateTime? requested { get; set; }//יצירת החבילה
        public DroneInParcel drone { get; set; }
        public DateTime? scheduled { get; set; }//שיוך
        public DateTime? pickedUp { get; set; }//איסוף
        public DateTime? delivered { get; set; }//אספקה
        public bool confirmationRecipt { get; set; }//confirmation on recipt of shipment

        public override string ToString()
        {
            String result = "";
            result += $"ID is: {ID}, Sender {sender}, " +
                      $"drone is: {drone}, target {target}, " +
                      $"weight is: {weight}, priority is: {priority}, " +
                      $"requested date is: {requested}, schedueld date ID is: {scheduled}," +
                      $" picke up date  is: {pickedUp}, delivered date is: {delivered}\n";
            return result;
        }
    }
}
