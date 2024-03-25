using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;
using System.Linq;

namespace Dal
{
    sealed public class DalXml : IDal
    {
        //static string dir = @"..\..\..\..\xmlData\";

        string customerPath = @"CustomerXml.xml";//element
        string stationPath = @"StationsXml.xml";  //serializer
        string parcelPath = @"ParcelXml.xml";//serializer
        string dronePath = @"DroneXml.xml";//Serializer
        string droneChargePath = @"DroneCharge.xml";//serializer

       

        #region singelton

        
        static readonly IDal instance = new DalXml();
        public static IDal Instance { get => instance; }


        private DalXml()
        {
            // Initialize();
            //  Initialize();
            List<DroneCharge> droneCharge = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneChargePath);
            foreach (var item in droneCharge)
            {
                UpdatePluseChargeSlots(item.stationeld);
            }
            droneCharge.Clear();
            XMLTools.SaveListToXMLSerializer(droneCharge, droneChargePath);


        }


      private void UpdatePluseChargeSlots(int id)
        {
            List<Station> list = new List<Station>();
            list = XMLTools.LoadListFromXMLSerializer<DO.Station>(stationPath);
            if (!list.Exists(x => x.ID == id))
                throw new DO.generalException("ERROR! the value not found");
            var p = list.Find(x => x.ID == id);
            list.Remove(p);
            p.chargeSlots++;
            list.Add(p);
            XMLTools.SaveListToXMLSerializer(list, stationPath);
        }
        public void Initialize()
        {
            Random r = new Random();
            List<DO.Drone> droneList = Dal.XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronePath);
            //loop for updete 5 drone
            for (int i = 0; i < 5; i++)
            {
                Drone temp = new Drone();
                temp.ID = r.Next(11111, 99999);
                temp.model = r.Next(1111, 9999);
                var x = (DO.WeightCategories)(r.Next(0, 3));
                temp.weight = x;


                droneList.Add(temp);

            }
            Dal.XMLTools.SaveListToXMLSerializer<DO.Drone>(droneList, dronePath);
            List<Station> list = new List<Station>();
            list = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            //loop for 2 station
            for (int i = 0; i < 2; i++)
            {
                Station temp = new Station();
                temp.ID = r.Next(1111, 9999);
                temp.longitude = r.Next(30, 34) + r.NextDouble();
                temp.lattitude = r.Next(34, 37) + r.NextDouble();
                temp.chargeSlots = r.Next(6, 100);
                Names namesTmp = (DO.Names)(i + 10);//for a diffrent name
                temp.name = namesTmp.ToString();
                list.Add(temp);
            }

            XMLTools.SaveListToXMLSerializer(list, stationPath);
            List<DO.Customer> cusList = Dal.XMLTools.LoadListFromXMLSerializer<DO.Customer>(customerPath);
            //lop for 10 customer
            for (int i = 0; i < 10; i++)
            {
                Customer temp = new Customer();
                Names names = (DO.Names)(i);//for a diffrent name
                temp.name = names.ToString();
                temp.longitude = r.Next(30, 34);
                temp.lattitude = r.Next(34, 37);
                temp.active = true;
                temp.ID = r.Next(212365428, 328987502);
                temp.phone = "0" + r.Next(521121316, 549993899);
                cusList.Add(temp);
            }
            XMLTools.SaveListToXMLSerializer(cusList, customerPath);
            XElement dalConfig = XElement.Load(@"xml\dal-config.xml");
            int id = Convert.ToInt32(dalConfig.Element("parcelId").Value);
            dalConfig.Element("parcelId").Value = (id + 10).ToString();
            dalConfig.Save(@"xml\dal-config.xml");
            List<Parcel> parLst = new List<Parcel>();
            parLst = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);

            //loop for 10 parcels
            for (int i = 0; i < 10; i++)
            {
                int month = r.Next(1, 12);
                int day = r.Next(1, 26);
                //DateTime? start = new DateTime(2021, 1, 1);
                //int range = (DateTime.Today - start).Days;
                Parcel temp = new Parcel();
                temp.ID = id++;
                temp.senderID = cusList[(i + 2) % 9].ID;
                temp.targetId = cusList[i].ID;
                temp.requested = new DateTime(2021, month, day);
                temp.droneID = 0;
                temp.scheduled = new DateTime(2021, month, day + 1);
                temp.pickedUp = new DateTime(2021, month, day + 2);
                temp.delivered = new DateTime(2021, month, day + 3);
                //temp.requested = start.AddDays(r.Next(range));
                //temp.droneID = 0;
                //temp.scheduled = temp.requested.AddHours(r.Next(1, 8));
                //temp.pickedUp = temp.scheduled.AddMinutes(r.Next(20, 180));
                //temp.delivered = temp.pickedUp.AddMinutes(r.Next(20, 90));
                temp.weight = (DO.WeightCategories)(r.Next() % 3);
                temp.priority = (DO.Priorities)(r.Next() % 3);
                parLst.Add(temp);

            }
            XMLTools.SaveListToXMLSerializer(parLst, parcelPath);
        }

        //DalXml()
        //{
        //    if (!Directory.Exists(dir))
        //        Directory.CreateDirectory(dir);

        //    if (!File.Exists(dir + customerPath))
        //        Directory.CreateDirectory(dir + customerPath);


        //    if (!File.Exists(dir + dronePath))
        //        Directory.CreateDirectory(dir + dronePath);

        //    if (!File.Exists(dir + droneChargePath))
        //        Directory.CreateDirectory(dir + droneChargePath);

        //    if (!File.Exists(dir + parcelPath))
        //        Directory.CreateDirectory(dir + parcelPath);

        //    if (!File.Exists(dir + stationPath))
        //        Directory.CreateDirectory(dir + stationPath);
        //}

        #endregion


        public double[] chargeCapacity()
        {
            XElement dalConfig = XElement.Load(@"xml\dal-config.xml");
            double cl = Convert.ToDouble(dalConfig.Element("chargeClear").Value);
            double li= Convert.ToDouble(dalConfig.Element("chargeLightWeight").Value);
            double me = Convert.ToDouble(dalConfig.Element("chargeMediumWeight").Value);
            double ha = Convert.ToDouble(dalConfig.Element("chargeHavyWeight").Value);
            double ra= Convert.ToDouble(dalConfig.Element("chargineRate").Value);
            double[] arr = new double[] { cl,li,me,ha,ra };
            return arr;
        }
        #region ----------------------------------------------customer---------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addCustomer(DO.Customer temp)
        {
            
            
                XElement customerRoot = XMLTools.LoadListFromXMLElement(customerPath);
                var customerElement = (from p in customerRoot.Elements()
                                       where Convert.ToInt32(p.Element("ID").Value) == temp.ID
                                       select p).FirstOrDefault();
                if (customerElement != null)
                    throw new IdExistsException("The customer already exist in the system");

                XElement theNewOne = new XElement("Customer",
                                          new XElement("ID", temp.ID),
                                          new XElement("name", temp.name),
                                          new XElement("phone", temp.phone),
                                          new XElement("longitude", temp.longitude),
                                          new XElement("lattitude", temp.lattitude),
                                          new XElement("active", temp.active));


                customerRoot.Add(theNewOne);
                XMLTools.SaveListToXMLElement(customerRoot, customerPath);
            
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.Customer findCustomer(int id)
        {
            XElement customerRoot = XMLTools.LoadListFromXMLElement( customerPath);
            Customer c = new Customer();
            foreach (var item in customerRoot.Elements())
            {
                if (Convert.ToInt32(item.Element("ID").Value) == id)
                {
                    c.active = Convert.ToBoolean(item.Element("active").Value);
                    c.ID = Convert.ToInt32(item.Element("ID").Value);
                    c.lattitude = Convert.ToDouble(item.Element("lattitude").Value);
                    c.longitude = Convert.ToDouble(item.Element("longitude").Value);
                    c.name = item.Element("name").Value;
                    c.phone = item.Element("phone").Value;
                    return c;
                }
            }
            throw new DO.IdUnExistsException("ERROR! the customer doesn't found ");
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Customer> getAllCustomers()
        {
            XElement customerRoot = XMLTools.LoadListFromXMLElement(customerPath);
            return from p in customerRoot.Elements()
                   where Convert.ToBoolean( p.Element("active").Value)==true
                   select new Customer()
                   {
                       ID = Convert.ToInt32(p.Element("ID").Value),
                       name = p.Element("name").Value,
                       active = Convert.ToBoolean(p.Element("active").Value),
                       lattitude = Convert.ToDouble(p.Element("lattitude").Value),
                       longitude = Convert.ToDouble(p.Element("longitude").Value),
                       phone = p.Element("phone").Value,
                   };
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deleteSCustomer(int id)
        {
            try
            {
                XElement customerRoot = XMLTools.LoadListFromXMLElement(customerPath);
                XElement customerElement = (from p in customerRoot.Elements()
                                            where Convert.ToInt32(p.Element("ID").Value) == id
                                            select p).FirstOrDefault();
                customerElement.Element("active").Value = false.ToString();


                XMLTools.SaveListToXMLElement(customerRoot,  customerPath);
                return;
            }
            catch (Exception e)
            {
                throw new DO.generalException(e.Message, e);
            }

            throw new DO.IdUnExistsException("ERROR! the customer doesn't found");
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateCustomer(int id, DO.Customer c)
        {
            try
            {
                XElement customerRoot = XMLTools.LoadListFromXMLElement( customerPath);
                XElement customerElement = (from p in customerRoot.Elements()
                                            where Convert.ToInt32(p.Element("ID").Value) == id
                                            select p).FirstOrDefault();
                customerElement.Element("ID").Value = c.ID.ToString();
                customerElement.Element("name").Value = c.name;
                customerElement.Element("active").Value = c.active.ToString();
                customerElement.Element("lattitude").Value = c.lattitude.ToString();
                customerElement.Element("longitude").Value = c.longitude.ToString();
                customerElement.Element("phone").Value = c.phone.ToString();

                XMLTools.SaveListToXMLElement(customerRoot, customerPath);
                return;

            }
            catch (Exception e)
            {
                throw new DO.IdUnExistsException(e.Message, e);
            }
        }



        #endregion

        #region ----------------------------------stattion------------------------------------

        // DalName = dalConfig.Element("packnum").Value;
        //    dalConfig.Element("packnum").Value = 8887;
        //    dalConfig.Save(path);
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Station> getAllStations()
        {
            List<DO.Station> list = new List<DO.Station>();
            list = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            return from Station in list
                   select Station;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.Station findStation(int id)
        {
            List<DO.Station> list = new List<DO.Station>();
            list = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            return (from station in list
                    where station.ID == id
                    select station).FirstOrDefault();
           
                   
            //foreach (DO.Station item in list)
            //{
            //    if (item.ID == id)
            //        return item;
            //}
            throw new DO.IdUnExistsException("ERROR! the station doesn't exist");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Station> getStationsWithChargeSlots()
        {
            List<DO.Station> list = new List<DO.Station>();
            list = XMLTools.LoadListFromXMLSerializer<Station>( stationPath);

            var s = from Station in list
                    where Station.chargeSlots > 0
                    select Station;
            return s;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addStations(DO.Station temp)
        {
            bool flug = true;
            List<Station> list = new List<Station>();
            list = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            foreach (Station item in list)
            {
                if (item.ID == temp.ID) //return true if the field is the same
                    flug = false;
            }

            if (flug)
                list.Add(temp);
            else
                throw new DO.IdExistsException("ERROR! the ID already exist");

            XMLTools.SaveListToXMLSerializer(list, stationPath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deleteStation(int id)
        {

            List<Station> list = new List<Station>();
            list = XMLTools.LoadListFromXMLSerializer<Station>( stationPath);
            foreach (DO.Station item in list)
            {
                if (item.ID == id)
                {
                    list.Remove(item);
                    XMLTools.SaveListToXMLSerializer(list, stationPath);
                    return;
                }
            }
            throw new DO.IdUnExistsException("ERROR! the station doesn't found");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateStation(int id, DO.Station st)
        {
            List<Station> list = new List<Station>();
            list = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            foreach (var item in list)
            {
                if (item.ID == id)
                {
                    list.Remove(item);
                    list.Add(st);
                    XMLTools.SaveListToXMLSerializer(list, stationPath);
                    return;
                }
            }

            throw new DO.IdUnExistsException("ERROR! the station doesn't found");
        }
        #endregion

        #region-------------------------------------parcel-----------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetPartParcel()
        {
            List<Parcel> list = new List<Parcel>();
            list = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            return from Parcel in list
                   select Parcel;
        }

        public void confirm(int id)
        {
            List<Parcel> list = new List<Parcel>();
            list = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            var pr = list.Find(x => x.ID == id);
            list.Remove(pr);
            pr.confirm = true;
            list.Add(pr);
            XMLTools.SaveListToXMLSerializer(list, parcelPath);
        }

        public int parcels()
        {
            List<Parcel> list = new List<Parcel>();
            list = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            var x= from Parcel in list
                   where Parcel.scheduled == null
                   select Parcel;
            return x.Count();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int addParcel(DO.Parcel temp)
        {
            XElement dalConfig = XElement.Load(@"xml\dal-config.xml");
            int id= Convert.ToInt32( dalConfig.Element("parcelId").Value);
                dalConfig.Element("parcelId").Value = (id+1).ToString();
                dalConfig.Save(@"xml\dal-config.xml");
            List<Parcel> list = new List<Parcel>();
            list = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            temp.ID = id;
            list.Add(temp);
            XMLTools.SaveListToXMLSerializer(list, parcelPath);
            //temp.ID = DAL.DataSource.Config.runnerID;
            //DAL.DataSource.ParcelList.Add(temp);
            //DAL.DataSource.Config.runnerID++;
            return id;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ParcelDrone(int parcelID, int droneID)
        {
            List<Parcel> list = new List<Parcel>();
            list = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            foreach (var item in list)
            {
                if (item.ID == parcelID)
                {
                    DO.Parcel p = item;
                    list.Remove(item);
                    p.droneID = droneID;
                    p.scheduled = DateTime.Now;
                    list.Add(p);
                    XMLTools.SaveListToXMLSerializer(list, parcelPath);
                    return;
                }
            }
            throw new DO.generalException("ERROR! the value not found");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ParcelPickedUp(int parcelID, DateTime day)
        {
            List<Parcel> list = new List<Parcel>();
            list = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);

            foreach (var item in list)

            {
                if (item.ID == parcelID)
                {
                    DO.Parcel p = item;
                    p.pickedUp = day;
                    list.Remove(item);
                    list.Add(p);
                    XMLTools.SaveListToXMLSerializer(list,parcelPath);
                    return;
                }
            }
            throw new DO.generalException("ERROR! the value not found");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ParcelReceived(int parcelID, DateTime day)
        {
            List<Parcel> list = new List<Parcel>();
            list = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            if(!list.Exists(x=>x.ID==parcelID))
                throw new DO.generalException("ERROR! the value not found");
            var p = list.Find(x => x.ID == parcelID);
            list.Remove(p);
            p.delivered = day;
            list.Add(p);
            
            XMLTools.SaveListToXMLSerializer(list, parcelPath);
            //foreach (var item in list)
            //{
            //    if (item.ID == parcelID)
            //    {
            //        DO.Parcel p = DAL.DataSource.ParcelList[i];
            //        p.delivered = day;
            //        DAL.DataSource.ParcelList[i] = p;
            //        return;
            //    }
            //}
           
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.Parcel findParcel(int id)
        {
            List<Parcel> list = new List<Parcel>();
            list = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);

            foreach (DO.Parcel item in list)
            {
                if (item.ID == id)
                    return item;
            }
            throw new DO.IdUnExistsException("ERROR! the parcel doesn't found");
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Parcel> getAllParcels()
        {
            List<Parcel> list = new List<Parcel>();
            list = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            return from Parcel in list
                   select Parcel;
        }

        /// <summary>
        /// print all parcels that have no yet drone
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Parcel> getParcelsWithoutDrone()
        {
            List<DO.Parcel> list = new List<DO.Parcel>();
            List<DO.Parcel> temp = new List<DO.Parcel>();
            temp = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            foreach (DO.Parcel item in list)
            {
                if (item.droneID == 0 && item.delivered == null)
                    temp.Add(item);
            }
            return temp;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deleteSParcel(int id)
        {
            List<DO.Parcel> list = new List<DO.Parcel>();
            list = XMLTools.LoadListFromXMLSerializer<Parcel>( parcelPath);
            foreach (DO.Parcel item in list)
            {
                if (item.ID == id)
                {
                    list.Remove(item);
                    XMLTools.SaveListToXMLSerializer(list, parcelPath);
                    return;
                }
            }
            throw new DO.IdUnExistsException("ERROR! the parcel doesn't found");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateParcel(int id, DO.Parcel p)
        {
            List<DO.Parcel> list = new List<DO.Parcel>();
            list = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            foreach (var item in list)
            {

                if (item.ID == id)
                {
                    list.Remove(item);
                    list.Add(p);
                    XMLTools.SaveListToXMLSerializer(list, parcelPath);
                    return;
                }
            }
            throw new DO.IdUnExistsException("ERROR! the parcel doesn't found");
        }
        #endregion

        #region-------------------------------drone----------------------------------

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Drone> GetPartOfDrone(Func<DO.Drone, bool> droneCondition = null)
        {
            List<DO.Drone> droneList = Dal.XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronePath);
            var list = from Drone in droneList
                       select Drone;
            if (droneCondition == null)
                return list;
            return list.Where(droneCondition);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addDrone(DO.Drone temp)
        {
            List<DO.Drone> droneList = Dal.XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronePath);
            if (droneList.Exists(x => x.ID == temp.ID))
                throw new DO.IdExistsException();
            droneList.Add(temp);
            Dal.XMLTools.SaveListToXMLSerializer<DO.Drone>(droneList, dronePath);

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.DroneCharge SendToCharge(int DroneID, int StationID)
        {
            List<DO.Drone> droneList = Dal.XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronePath);
            if (!droneList.Exists(x => x.ID == DroneID))
                throw new DO.IdUnExistsException();

            DO.DroneCharge d = new DO.DroneCharge();
            d.droneID = DroneID;
            d.stationeld = StationID;
            d.enterToCharge = DateTime.Now;
            List<DO.Station> stationList = Dal.XMLTools.LoadListFromXMLSerializer<DO.Station>(stationPath);
            DO.Station s = stationList.Find(x => x.ID == StationID);
            stationList.Remove(s);
            s.chargeSlots--;
            stationList.Add(s);
            List<DO.DroneCharge> dr = Dal.XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>( droneChargePath);
            dr.Add(d);
            Dal.XMLTools.SaveListToXMLSerializer<DO.Station>(stationList,  stationPath);
            Dal.XMLTools.SaveListToXMLSerializer<DO.DroneCharge>(dr, droneChargePath);
            return d;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void BatteryCharged(DO.DroneCharge dc)
        {
            List<DO.DroneCharge> dr = Dal.XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>( droneChargePath);
            if (!dr.Exists(x => x.droneID == dc.droneID))
                throw new DO.generalException("ERROR! value not found");
            dr.Remove(dc);
            List<DO.Station> stationList = Dal.XMLTools.LoadListFromXMLSerializer<DO.Station>( stationPath);
            DO.Station s = stationList.Find(x => x.ID == dc.stationeld);
            stationList.Remove(s);
            s.chargeSlots++;
            stationList.Add(s);
            Dal.XMLTools.SaveListToXMLSerializer<DO.Station>(stationList,  stationPath);
            Dal.XMLTools.SaveListToXMLSerializer<DO.DroneCharge>(dr, droneChargePath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.Drone findDrone(int id)
        {
            List<DO.Drone> droneList = Dal.XMLTools.LoadListFromXMLSerializer<DO.Drone>( dronePath);
            if (!droneList.Exists(x => x.ID == id))
                throw new DO.IdUnExistsException("ERROR! the drone doesn't exist");
            return droneList.Find(x => x.ID == id);

        }

        /// <summary>
        /// return a list of all drones
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Drone> getAllDrones()
        {
            List<DO.Drone> droneList = Dal.XMLTools.LoadListFromXMLSerializer<DO.Drone>( dronePath);
            var lst = from Drone in droneList
                      select Drone;
            return lst;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deleteDrone(int id)
        {
            List<DO.Drone> droneList = Dal.XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronePath);
            if (!droneList.Exists(x => x.ID == id))
                throw new DO.IdUnExistsException("ERROR! the drone doesn't exist");
            var d = droneList.Find(x => x.ID == id);
            droneList.Remove(d);
            Dal.XMLTools.SaveListToXMLSerializer<DO.Drone>(droneList, dronePath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateDrone(int id, int mod)
        {
            List<DO.Drone> droneList = Dal.XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronePath);
            if (!droneList.Exists(x => x.ID == id))
                throw new DO.IdUnExistsException("ERROR! the drone doesn't exist");
            var d = droneList.Find(x => x.ID == id);
            droneList.Remove(d);
            d.model = mod;
            droneList.Add(d);
            Dal.XMLTools.SaveListToXMLSerializer<DO.Drone>(droneList,  dronePath);

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.DroneCharge> findDroneCharge(int id)
        {
            List<DO.DroneCharge> dr = Dal.XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(droneChargePath);

            //find all the station with empty charge slots
            var lst = from item in dr
                      where item.stationeld == id
                      select item;
            foreach (var item2 in lst)
                lst.ToList().Add(item2);
            return lst;

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.DroneCharge findStationOfDroneCharge(int id)
        {
            List<DO.DroneCharge> dr = Dal.XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(droneChargePath);

            DO.DroneCharge temp = new DO.DroneCharge();
            foreach (DO.DroneCharge item in dr)
                if (item.droneID == id)
                    temp = item;

            return temp;
        }
        #endregion



    }
}



