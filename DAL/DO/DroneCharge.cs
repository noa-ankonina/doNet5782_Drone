using System;
using DO;

namespace DO
{

    public struct DroneCharge
    {
        public int droneID { get; set; }
        public int stationeld { get; set; }
        public DateTime enterToCharge;
        public override string ToString()
        {
            String result = "";
            result += $"drone ID is: {droneID}, \n";
            result += $"stationeld is: {stationeld}, \n";
            return result;
        }
    }
}
