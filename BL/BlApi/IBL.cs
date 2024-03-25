using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi
{
    public interface IBL
    {
        /// <summary>
        /// start the action of the simolator
        /// </summary>
        /// <param name="id"></param>
        /// <param name="myDelegate"></param>
        /// <param name="func"></param>
    public  void playSimolator(int id, Action myDelegate, Func<bool> func);

       // public int parcels();
        
        #region - - - - - - - - - - - - customer- - - - - - - - - - - - - - - 
        /// <summary>
        /// get a customer of BL and add it to the list of DAL
        /// </summary>
        /// <param name="customerBL"></param>
        public void addCustomer(global::BO.Customer customerBL);

        /// <summary>
        /// update the name or the phone number of the customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phoneNum"></param>
        public void updateCustomer(int id, string name, string phoneNum);

        /// <summary>
        /// print all the list of the customer to list
        /// </summary>
        public IEnumerable<global::BO.CustomerToList> viewListCustomer();

        /// <summary>
        /// get a id of customer and return a customer of BL
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public global::BO.Customer findCustomer(int id);

        /// <summary>
        /// change the status(active) of the customer to false
        /// </summary>
        /// <param name="id"></param>
        public void deleteCustomer(int id);
        #endregion

        #region - - - - - - - - - - - - drone - - - - - - - - - - - - - - - -
        /// <summary>
        /// Returns a filtered list by drone status
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        public IEnumerable<global::BO.DroneToList> droneFilterStatus(global::BO.DroneStatus w);


        /// <summary>
        /// Returns a filtered list by weight category
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        public IEnumerable<global::BO.DroneToList> droneFilterWheight(global::BO.WeightCategorie w);

            /// <summary>
            /// adding a drone to droneList
            /// </summary>
            /// <param name="drone"></param>
        public void addDrone(int id, int model, int weight, int stationId);

        /// <summary>
        /// get a id and return the drone(of bl) with this id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public global::BO.Drone findDrone(int id);

        /// <summary>
        /// update the name of the drone
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="model"></param>
        public void updateNameDrone(int ID, int model);

        /// <summary>
        /// release the drone from charge
        /// </summary>
        /// <param name="id"></param>
        /// <param name="time"></param>
        public void releaseFromCharge(int id,bool flag=false);

        /// <summary>
        /// return a IEnumerable<IBL.BO.DroneToList>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<global::BO.DroneToList> getAllDrones();

        /// <summary>
        /// send a drone to the closed station
        /// </summary>
        /// <param name="id"></param>
        public void sendToCharge(int id);

        /// <summary>
        /// get a id of drone and find a  parcel 
        /// </summary>
        /// <param name="id"></param>
        public void parcelToDrone(int id);

        /// <summary>
        /// get an id and remove the drone with this id
        /// </summary>
        /// <param name="id"></param>
        public void deleteDrone(int id);

        #endregion

        #region - - - - - - - - - - - - parcel - - - - - - - - - - - - - - - 
        /// <summary>
        /// add a parcel to the list in the DAL
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="targetId"></param>
        /// <param name="weight"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public int addParcel(int senderId, int targetId, int weight, int priority);

        /// <summary>
        /// change the field of confirn (confirm the parcel delivered)
        /// </summary>
        /// <param name="parcelId"></param>
        public void confirm(int parcelId);
        /// <summary>
        /// return the list of all parcels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ParcelToList> getAllParcels(Func<Parcel, bool> predicate = null);

        // <summary>
        /// return a parcel with all details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public global::BO.Parcel findParcel(int id);

        /// <summary>
        /// return a list of parcels that have not a drone yet
        /// </summary>
        /// <returns></returns>
        public IEnumerable<global::BO.ParcelToList> parcelsWithoutDrone();

        /// <summary>
        /// the function update the parcel to be collected by the drone
        /// </summary>
        /// <param name="droneid"></param>
        public void packageCollection(int droneid,bool fa=false);

        /// <summary>
        /// The function update the parcel to be delivered
        /// </summary>
        /// <param name="droneid"></param>
        public void packageDelivery(int droneid,bool f=false);

        /// <summary>
        /// delete thr parcel
        /// </summary>
        /// <param name="id"></param>
        public void deleteParcel(int id);
        #endregion

        #region       - - - - - - - - - - - - - station - - - - - - - - - - - - - - - 
        /// <summary>
        /// add a station to the list of dal.dataSource
        /// </summary>
        /// <param name="station"></param>
        public void addStation(global::BO.Station station);

        /// <summary>
        /// update some field in the station
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="name"></param>
        /// <param name="emptyChargeSlot"></param>
        public void updateStation(int id, string name, int chargeSlot);

        /// <summary>
        /// the func return all the list of the station
        /// </summary>
        /// <returns></returns>
        public IEnumerable<global::BO.StationToList> veiwListStation();

        /// <summary>
        /// get a id of station and return a station of BL
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public global::BO.Station findStation(int id);

        /// <summary>
        /// return all the ststion with 1 or more avilable charge slots
        /// </summary>
        /// <returns></returns>
        public IEnumerable<global::BO.StationToList> avilableCharginStation();

        /// <summary>
        /// get an id  delete the station
        /// </summary>
        /// <param name="id"></param>
        public void deleteStation(int id);

        #endregion
    }
}
