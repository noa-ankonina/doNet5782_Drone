using System.Runtime.CompilerServices;
using System;
using DalApi;
using System.Collections.Generic;

namespace Dal
{
    sealed partial class DalObject
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addStations(DO.Station temp)
        {
            bool flug = true;
            foreach (DO.Station item in DAL.DataSource.StationsList)
            {

                if (item.ID == temp.ID) //return true if the field is the same
                    flug = false;
            }
            if (flug)
                DAL.DataSource.StationsList.Add(temp);
            else
                throw new DO.IdExistsException("ERROR! the ID already exist");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.Station findStation(int id)
        {
            foreach (DO.Station item in DAL.DataSource.StationsList)
            {
                if (item.ID == id)
                    return item;
            }
            throw new DO.IdUnExistsException("ERROR! the station doesn't exist");
        }

      
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Station> getAllStations()
        {
            List<DO.Station> list = new List<DO.Station>();
            foreach (DO.Station item in DAL.DataSource.StationsList)
            {
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// print all stations with charge slots available
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Station> getStationsWithChargeSlots()
        {
            List<DO.Station> lst = new List<DO.Station>();
            foreach (DO.Station item in DAL.DataSource.StationsList)
            {
                if (item.chargeSlots > 0)
                    lst.Add(item);
            }
            return lst;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deleteStation(int id)
        {
            foreach (DO.Station item in DAL.DataSource.StationsList)
            {
                if (item.ID == id)
                {
                    DAL.DataSource.StationsList.Remove(item);
                    return;
                }
            }
            throw new DO.IdUnExistsException("ERROR! the station doesn't found");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateStation(int id, DO.Station st)
        {
            for (int i = 0; i < DAL.DataSource.StationsList.Count; i++)
            {
                if (DAL.DataSource.StationsList[i].ID == id)
                {
                    DAL.DataSource.StationsList[i] = st;
                    return;
                }
            }
            throw new DO.IdUnExistsException("ERROR! the station doesn't found");
        }
    }
}
