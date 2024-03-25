using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using BO;
using System.Threading;
using static BL.BL;


namespace BL
{
    class Simolatur
    {
        const int delay = 1000;
        const double speed = 100;

        /// <summary>
        /// constructor
        /// </summary>
        public Simolatur(BlApi.IBL bl ,int id, Action myDelegate, Func<bool> flug)
        {
            try
            {
                Drone drone = new Drone();
                Parcel parcel = new Parcel();
                lock (bl) { drone = bl.findDrone(id); }

                while (!flug())
                {
                    lock (bl) { drone = bl.findDrone(id); }

                    switch (drone.status)
                    {
                        case DroneStatus.Available:
                            try
                            {
                                //send the drone to delivery
                                bl.parcelToDrone(id);
                                myDelegate();
                                Thread.Sleep(delay);

                            }
                            catch (Exception)
                            {
                                if (drone.battery != 100)
                                {
                                    try
                                    {
                                        bl.sendToCharge(id);
                                        myDelegate();
                                        Thread.Sleep(delay);
                                    }
                                    catch (Exception)
                                    {
                                        Thread.Sleep(delay);
                                    }
                                }
                            }
                            break;

                        case DroneStatus.Maintenace:
                            bl.releaseFromCharge(id, true);
                            myDelegate();
                            Thread.Sleep(delay);
                            break;

                        case DroneStatus.Delivery:
                            if (drone.parcel.status == true)
                                bl.packageDelivery(id, true);
                            else
                                bl.packageCollection(id, true);
                            myDelegate();
                            Thread.Sleep(delay);
                            break;
                    }
                }
            }
            catch(Exception ex)
            {
                throw new BLgeneralException(ex.Message);
            }

        }
    }
}
