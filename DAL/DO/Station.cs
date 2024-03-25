using System;
using DO;


    namespace DO
    {


        public struct Station
        {
            public int ID { get; set; }
            public string name { get; set; }
            public double longitude { get; set; }
            public double lattitude { get; set; }
            public int chargeSlots { get; set; }

            public override string ToString()
            {
                String result = "";
                result += $"ID is: {ID}, Name is: {name}, Longitude is: {longitude}, " +
                          $"Latitude is: {lattitude}, charge slolts is: {chargeSlots},\n";
                
                return result;
            }
        }
    }
