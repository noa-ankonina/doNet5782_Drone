using System;
using DO;
using System.Collections.Generic;


    namespace DAL
    { 
        
       
            public class DataSource
            {
                internal static List<Drone> DronesList = new List<Drone>();
                internal static List< Station> StationsList = new List<Station>();
                internal static List< Customer> CustomersList = new List<Customer>();
                internal static List <Parcel> ParcelList = new List<Parcel>();
                internal static List<DroneCharge> DroneChargeList = new List<DroneCharge>();
                internal class Config
                {
                    internal static double chargeClear=0.001;
                   internal static double chargeLightWeight=0.005;
                   internal static double chargeHavyWeight=0.05;
                   internal static double chargeMediumWeight=0.01;
                   internal static double chargineRate=0.50;
                    internal static int runnerID=1000;

                }

                public static void Initialize()
                {
                    Random r = new Random();

                    //loop for updete 5 drone
                    for (int i = 0; i < 5; i++)
                    {
                       Drone temp= new Drone();
                        temp.ID= r.Next(11111, 99999);
                        temp.model = r.Next(1111, 9999);
                        var x = (DO.WeightCategories)(r.Next(0, 3));
                        temp.weight = x;
                        DronesList.Add(temp);
                    }

                    //loop for 2 station
                    for (int i =0; i < 2; i++)
                    {
                        Station temp = new Station();
                        temp.ID = r.Next(1111, 9999);
                        temp.longitude = r.Next(30,34 )+r.NextDouble();
                        temp.lattitude = r.Next(34, 37)+r.NextDouble();
                        temp.chargeSlots = r.Next(6, 100);
                        Names namesTmp = (DO.Names)(i + 9);//for a diffrent name
                        temp.name = namesTmp.ToString();
                        StationsList.Add(temp);
                    }

                    //lop for 10 customer
                    for (int i = 0; i < 10; i++)
                    {
                        Customer temp = new Customer();
                        Names names= (DO.Names)(i);//for a diffrent name
                        temp.name = names.ToString();
                        temp.longitude = r.Next(30, 34);
                        temp.lattitude = r.Next(34, 37);
                        temp.active = true;
                        temp.ID = r.Next(212365428, 328987502);
                        temp.phone = "0"+ r.Next(521121316, 549993899);
                        CustomersList.Add(temp);
                    }

                    //loop for 10 parcels
                    for (int i =0; i < 10; i++)
                    {
                        int month = r.Next(1, 12);
                        int day = r.Next(1, 26);
                        //DateTime? start = new DateTime(2021, 1, 1);
                        //int range = (DateTime.Today - start).Days;
                        Parcel temp = new Parcel();
                        temp.ID = Config.runnerID;
                        temp.senderID = CustomersList[(i + 2) % 9].ID;
                        temp.targetId = CustomersList[i].ID;
                        temp.requested = new DateTime(2021, month, day);
                        temp.droneID = 0;
                        temp.scheduled = new DateTime(2021, month, day + 1);
                        temp.pickedUp = new DateTime(2021, month, day + 2);
                        temp.delivered = new DateTime(2021, month, day+3);
                        //temp.requested = start.AddDays(r.Next(range));
                        //temp.droneID = 0;
                        //temp.scheduled = temp.requested.AddHours(r.Next(1, 8));
                        //temp.pickedUp = temp.scheduled.AddMinutes(r.Next(20, 180));
                        //temp.delivered = temp.pickedUp.AddMinutes(r.Next(20, 90));
                        temp.weight = (DO.WeightCategories)(r.Next() % 3);
                        temp.priority = (DO.Priorities)(r.Next() % 3);
                        ParcelList.Add(temp);
                        Config.runnerID++;
                    }
                }
            }
        
    }


