using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BL;


namespace BL
{
   internal partial class BL 
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addCustomer(Customer customerBL)
        {
            try
            {
                if (customerBL.location.longitude < 29.3 || customerBL.location.longitude > 33.5)
                    throw new BLgeneralException("Error! the longitude is incorrect, (longitude 29.3 - 33.5, lattitude 33.7 - 36.3 ");
                if (customerBL.location.latitude < 33.7 || customerBL.location.latitude > 36.3)
                    throw new BLgeneralException("Error! the latitude is incorrect, (longitude 29.3 - 33.5, lattitude 33.7 - 36.3 ");
                if (customerBL.ID < 100000000 || customerBL.ID > 999999999)
                    throw new BLgeneralException("ERROR! the id must be with 9 digits");
                if (customerBL.phone.Length < 9 || customerBL.phone.Length > 10)
                    throw new BLgeneralException("ERROR! the phone must be with 9 or 10 digits");

                DO.Customer temp = new DO.Customer();
                temp.ID = customerBL.ID;
                temp.name = customerBL.name;
                temp.phone = customerBL.phone;
                temp.lattitude = customerBL.location.latitude;
                temp.longitude = customerBL.location.longitude;
                temp.active = true;
                dl.addCustomer(temp);
            }
            catch(Exception e)
            {
                throw new BLgeneralException(e.Message, e);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateCustomer(int id, string name, string phoneNum)
        {
            if (phoneNum != "" && (phoneNum.Length < 9 || phoneNum.Length > 10))
                throw new BLgeneralException("ERROR! the phone number must be with 9 or ten digits");
            
            try
            {
                lock (dl)
                {
                    DO.Customer temp = new DO.Customer();
                    temp = dl.findCustomer(id);

                    //if the user want to change some detail....
                    if (name != "")
                        temp.name = name;
                    if (phoneNum != "")
                        temp.phone = phoneNum;

                    dl.updateCustomer(id, temp);
                }
            }
            catch (Exception e)
            {
                throw new BLgeneralException(e.Message,e);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer findCustomer(int id)
        {
            try
            {
                lock (dl)
                {
                    Customer cusBL = new Customer();
                    DO.Customer cusDal = dl.findCustomer(id);
                    List<ParcelAtCustomer> p1 = new List<ParcelAtCustomer>();
                    List<ParcelAtCustomer> p2 = new List<ParcelAtCustomer>();

                    cusBL.ID = cusDal.ID;
                    cusBL.active = cusDal.active;
                    cusBL.location = new Location();
                    cusBL.location.latitude =Math.Round( cusDal.lattitude,2);
                    cusBL.location.longitude = Math.Round( cusDal.longitude,2);
                    cusBL.phone = cusDal.phone;
                    cusBL.name = cusDal.name;
                    cusBL.fromCustomer = new List<ParcelAtCustomer>();
                    cusBL.toCustomer = new List<ParcelAtCustomer>();
                    IEnumerable<DO.Parcel> lstP = dl.getAllParcels();
                    foreach (var item in lstP)
                    {
                        //Finds all the packages the customer receives
                        if (item.targetId == cusBL.ID)
                        {
                            ParcelAtCustomer tmp = new ParcelAtCustomer();
                            tmp.ID = item.ID;
                            tmp.status = getParcelStatus(item);
                            tmp.priority = GetParcelPriorities(item.priority);
                            tmp.senderOrTarget = new CustomerInParcel();
                            tmp.senderOrTarget.ID = item.senderID;
                            tmp.senderOrTarget.customerName = dl.findCustomer(item.senderID).name;
                            tmp.confirmation = item.confirm;
                            p1.Add(tmp);
                        }
                        //Finds all the packages the customer sends
                        if (item.senderID == cusBL.ID)
                        {
                            ParcelAtCustomer tmp = new ParcelAtCustomer();
                            tmp.ID = item.ID;
                            tmp.status = getParcelStatus(item);
                            tmp.senderOrTarget = new CustomerInParcel();
                            tmp.senderOrTarget.ID = item.targetId;
                            tmp.senderOrTarget.customerName = dl.findCustomer(item.targetId).name;
                            tmp.priority = GetParcelPriorities(item.priority);

                            p2.Add(tmp);
                        }

                    }
                    cusBL.fromCustomer = p2;
                    cusBL.toCustomer = p1;
                    return cusBL;
                }
            }
            catch (Exception e)
            {
                throw new BLgeneralException(e.Message, e);
            }
        }

        /// <summary>
        /// Convert from the Dal type to the Bl type priority
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>

        [MethodImpl(MethodImplOptions.Synchronized)]
        private Priorities GetParcelPriorities(DO.Priorities p)
        {

            if (p == DO.Priorities.emergency)
                return Priorities.Emergency;
            if (p == DO.Priorities.fast)
                return Priorities.Fast;
            if (p == DO.Priorities.normal)
                return Priorities.Normal;

            //This line is only to preserve the legality of the kimpol
            return Priorities.Normal;
        }

        /// <summary>
        /// Convert from the Dal type to the Bl type status
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private ParcelStatus getParcelStatus(DO.Parcel p)
        {
            if (p.scheduled == null && p.requested != null)
                return ParcelStatus.Created;
            if (p.pickedUp == null && p.scheduled != null)
                return ParcelStatus.Match;
            if (p.delivered == null && p.pickedUp != null)
                return ParcelStatus.PickedUp;
            return ParcelStatus.Delivred;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deleteCustomer(int id)
        {
            dl.deleteSCustomer(id);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerToList> viewListCustomer()
        {
            lock (dl)
            {
                //bring al the data from dal
                IEnumerable<DO.Customer> lst = new List<DO.Customer>();
                lst = dl.getAllCustomers();

                List<CustomerToList> listBL = new List<CustomerToList>();
                foreach (var item in lst)
                {
                    CustomerToList c = new CustomerToList();
                    c.active = item.active;
                    c.ID = item.ID;
                    c.name = item.name;
                    c.phone = item.phone;

                    //
                    var p = dl.getAllParcels();
                    int counterDelivered = 0;
                    int counterDontDelivered = 0;
                    int counterGot = 0;
                    int counterOnWay = 0;
                    foreach (var parcel in p)
                    {
                        //count the parcel he got
                        if (parcel.senderID == item.ID)
                            counterGot++;
                        //if the parcel arrived
                        if (parcel.senderID == item.ID && parcel.delivered != null)
                            counterDelivered++;
                        //Package not yet associated
                        if (parcel.senderID == item.ID && parcel.scheduled == null)
                            counterDontDelivered++;
                        // A package was assigned to the glider but has not yet arrived
                        if (parcel.senderID == item.ID && parcel.scheduled != null && parcel.delivered == null)
                            counterOnWay++;
                    }
                    c.gotParcels = counterGot;
                    c.onTheWayParcels = counterOnWay;
                    c.sendAndDeliveredParcels = counterDelivered;
                    c.sendAndNotDeliveredParcels = counterDontDelivered;
                    listBL.Add(c);
                }

                return listBL;
            }
        }
    }
}
