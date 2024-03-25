using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
    {
       public class Location
        {
            public double longitude { get; set; }
            public double latitude { get; set; }
            public override string ToString()
            {
                int longSeconds = (int)Math.Round(longitude * 60 * 60);
                double x = (longitude - Math.Truncate(longitude)) * 60;//the decimaly part *60
                double seconds = (float)(x - Math.Truncate(x)) * 60;//the decimaly part of minute *60
                int minutes = ((longSeconds / 60) % 60);
                int degrees = ((longSeconds / 60) / 60);
                //
                if (seconds < 0)
                    seconds = seconds * -1;
                if (minutes < 0)
                    minutes = minutes * -1;
                if (degrees < 0)
                    degrees = degrees * -1;

                int latSeconds = (int)Math.Round(latitude * 60 * 60);
                double xL = (latitude - Math.Truncate(latitude)) * 60;//the decimaly part *60
                double secondsL = (float)(xL - Math.Truncate(xL)) * 60;//the decimaly part of minute *60
                int minutesL = (latSeconds / 60) % 60;
                int degreesL = (latSeconds / 60) / 60;
                //
                if (secondsL < 0)
                    secondsL = secondsL * -1;
                if (minutesL < 0)
                    minutesL = minutesL * -1;
                if (degreesL < 0)
                    degreesL = degreesL * -1;

               string result = $@"{degrees}° {minutes}' {seconds}"" E ,{degreesL}° {minutesL}' {secondsL}"" S";
       
                return result;
            }
        }
    }
