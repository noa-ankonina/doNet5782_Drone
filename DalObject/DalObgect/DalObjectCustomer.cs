using System;
using DalApi;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Dal
{
    sealed partial class DalObject
    {

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addCustomer(DO.Customer temp)
        {
            bool flag = true;
            foreach (DO.Customer item in DAL.DataSource.CustomersList)
            {

                if (item.ID == temp.ID) //return true if the field is the same
                    flag = false;
            }
            if (flag == true)
            {

                DAL.DataSource.CustomersList.Add(temp);
            }
            else
            {
                DO.Customer c = DAL.DataSource.CustomersList.Find(item => item.ID == temp.ID);
                if (c.active == false)
                {
                    DAL.DataSource.CustomersList.Remove(c);
                    c.active = true;
                    DAL.DataSource.CustomersList.Add(c);
                }
                else
                    throw new DO.IdUnExistsException("ERROR! the customer is already exist");



                //throw new IdExistsException();
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.Customer findCustomer(int id)
        {
            foreach (DO.Customer item in DAL.DataSource.CustomersList)
            {
                if (item.ID == id && item.active == true)
                    return item;
            }
            throw new DO.IdUnExistsException("ERROR! the customer doesn't found or not active");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Customer> getAllCustomers()
        {
            List<DO.Customer> lst = new List<DO.Customer>();
            foreach (DO.Customer item in DAL.DataSource.CustomersList)
            {
                if (item.active == true)
                    lst.Add(item);
            }
            return lst;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deleteSCustomer(int id)
        {
            try
            {
                DO.Customer c = findCustomer(id);
                DAL.DataSource.CustomersList.Remove(c);
                c.active = false;
                DAL.DataSource.CustomersList.Add(c);
                return;

            }
            catch (Exception e)
            {
                throw new DO.generalException(e.Message, e);
            }
            //foreach (Customer item in DataSource.CustomersList)
            //{
            //    if (item.ID == id)
            //    {
            //        DataSource.CustomersList.Remove(item);
            //        return;
            //    }
            //}
            throw new DO.IdUnExistsException("ERROR! the customer doesn't found");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateCustomer(int id, DO.Customer c)
        {
            try
            {
                DO.Customer tmp = findCustomer(id);
                DAL.DataSource.CustomersList.Remove(tmp);

                tmp = c;
                DAL.DataSource.CustomersList.Add(tmp);

            }
            catch (Exception e)
            {
                throw new DO.IdUnExistsException(e.Message, e);
            }
        }
    }
}
