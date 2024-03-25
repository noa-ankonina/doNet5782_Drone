using System;
using DO;

namespace DO
{

    public struct Customer
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public double longitude { get; set; }
        public double lattitude { get; set; }
         public bool active { get; set; }

        public override string ToString()
        {
            String result = "";
            result += $"ID is: {ID}, Name is: {name}, Telephone is: {phone.Substring(0, 3) + '-' + phone.Substring(3)}," +
                $"Lattitude is: {lattitude}, longitude is: {longitude},\n";
            // result += $"Name is: {name}, \n";
            //result += $"Telephone is: {phone.Substring(0, 3) + '-' + phone.Substring(3)}, \n";
            //result += $"Lattitude is: {lattitude}, \n";
            //result += $"longitude is: {longitude}, \n";
            return result;
        }


    }
}

