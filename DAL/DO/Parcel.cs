using System;
using DO;



    namespace DO
    {


        public struct Parcel
        {
            public int ID { get; set; }
            public int senderID { get; set; }
            public int targetId { get; set; }
            public WeightCategories weight { get; set; }
            public Priorities priority { get; set; }
            public DateTime? requested { get; set; }
            public int droneID { get; set; }
            public DateTime? scheduled { get; set; }
            public DateTime? pickedUp { get; set; }
            public DateTime? delivered { get; set; }
        public bool confirm { get; set; }
            public override string ToString()
            {
                String result = "";
                result += $"ID is: {ID}, sender ID is: {senderID}, " +
                          $"drone ID is: {droneID}, target ID is: {targetId}, " +
                          $"weight is: {weight}, priority is: {priority}, " +
                          $"requested date is: {requested}, schedueld date ID is: {scheduled}," +
                          $" picke up date  is: {pickedUp}, delivered date is: {delivered}\n";
                
                return result;
            }

        }
    }

