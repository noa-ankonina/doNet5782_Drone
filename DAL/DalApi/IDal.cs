using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi
{
    
    public interface IDal
    {
        /// <summary>
        /// reurn array of five values ​​in the following order: free, light, medium, heavy and load rate
        /// </summary>
        /// <returns></returns>
        public double[] chargeCapacity();

        public int parcels();

        #region-------------------- station----------------------
        /// <summary>
        /// Gets a station and adds it to the list DataSource
        /// </summary>
        /// <param name="temp"></param>
        public void addStations(Station temp);

        /// <summary>
        /// Receives an ID  and returns the station with the same ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DO.Station findStation(int id);

        /// <summary>
        /// retuen  IEnumerable<Station>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.Station> getAllStations();

        /// <summary>
        /// return IEnumerable<Station> of station with empty charge slots
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.Station> getStationsWithChargeSlots();

        /// <summary>
        /// delet station from the list at dataSource
        /// </summary>
        /// <param name="id"></param>
        public void deleteStation(int id);

        /// <summary>
        /// update the details of a station
        /// </summary>
        /// <param name="id"></param>
        /// <param name="st"></param>
        public void updateStation(int id, DO.Station st);


        #endregion

        #region --------------------drone--------------------------
        /// <summary>
        ///מקבלת פרדיקט ומחזירה את כל האיברים העונים לפרדיקט
        /// </summary>
        /// <param name="StationCondition"></param>
        /// <returns></returns>
        public IEnumerable<DO.Drone> GetPartOfDrone(Func<DO.Drone, bool> droneCondition = null);
        /// <summary>
        /// Gets a drone and adds it to the list DataSource
        /// </summary>
        /// <param name="temp"></param>
        public void addDrone(Drone temp);

        /// <summary>
        /// send the drone to charge in a station
        /// </summary>
        /// <param name="DroneID"></param>
        /// <param name="StationID"></param>
        /// <returns></returns>
        public DO.DroneCharge SendToCharge(int DroneID, int StationID);

        /// <summary>
        /// release the drone from the charge slote
        /// </summary>
        /// <param name="dc"></param>
        public void BatteryCharged(DO.DroneCharge dc);


        /// <summary>
        /// Receives an ID  and returns the drone with the same ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DO.Drone findDrone(int id);


        /// <summary>
        /// update the details of a drone
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mod"></param>
        public void updateDrone(int id, int mod);


        /// <summary>
        /// retuen IEnumerable<Drone>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.Drone> getAllDrones();

        /// <summary>
        /// delet drone from the list at dataSource
        /// </summary>
        /// <param name="id"></param>
        public void deleteDrone(int id);

        /// <summary>
        /// return the station that the drone charge in
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DO.DroneCharge findStationOfDroneCharge(int id);
        /// <summary>
        /// get a id of atation and return all the drone that charge in this station
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<DO.DroneCharge> findDroneCharge(int id);

        #endregion

        #region ----------------------customer-------------------------
        /// <summary>
        /// Get a Customer-type variable and adds it to the database
        /// </summary>
        /// <param name="temp"></param>
        public void addCustomer(Customer temp);

        /// <summary>
        /// Receives an ID  and returns the customer with the same ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DO.Customer findCustomer(int id);

        /// <summary>
        /// return list of the customer from type IEnumerable<Customer>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.Customer> getAllCustomers();


        /// <summary>
        /// update the details of the customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="c"></param>
        public void updateCustomer(int id, DO.Customer c);

        /// <summary>
        /// delet customer from the list at dataSource
        /// </summary>
        /// <param name="id"></param>
        public void deleteSCustomer(int id);

        #endregion

        #region ----------------------parcel---------------------
        public void confirm(int id);

        /// <summary>
        ///מקבלת פרדיקט ומחזירה את כל האיברים העונים לפרדיקט
        /// </summary>
        /// <param name="StationCondition"></param>
        /// <returns></returns>
        public IEnumerable<Parcel> GetPartParcel();

        /// <summary>
        /// Gets a parcel and adds it to the list at DataSource
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public int addParcel(Parcel temp);

        /// <summary>
        /// update the DroneId to the parcel
        /// </summary>
        /// <param name="parcelID"></param>
        /// <param name="droneID"></param>
        public void ParcelDrone(int parcelID, int droneID);

        /// <summary>
        /// update the date that the parcel picked up
        /// </summary>
        /// <param name="parcelID"></param>
        /// <param name="day"></param>
        public void ParcelPickedUp(int parcelID, DateTime day);

        /// <summary>
        /// update the date that the parcel delivered
        /// </summary>
        /// <param name="parcelID"></param>
        /// <param name="day"></param>
        public void ParcelReceived(int parcelID, DateTime day);

        /// <summary>
        /// Receives an ID  and returns the parcel with the same ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Parcel findParcel(int id);

        /// <summary>
        /// return IEnumerable<Parcel>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Parcel> getAllParcels();

        /// <summary>
        /// return IEnumerable<Parcel> which have not yet been assigned to any drone
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Parcel> getParcelsWithoutDrone();

        /// <summary>
        /// delet parcel from the list at dataSource
        /// </summary>
        /// <param name="id"></param>
        public void deleteSParcel(int id);

        /// <summary>
        /// update the details of the parcel
        /// </summary>
        /// <param name="id"></param>
        /// <param name="p"></param>
        public void updateParcel(int id, Parcel p);

        #endregion
        
    }
}
