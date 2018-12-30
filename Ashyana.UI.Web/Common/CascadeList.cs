using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ashyana.UI.Web.Models;
namespace Ashyana.UI.Web.Common
{
    public class CascadeList
    {
        AshyanaDBEntities objEntity = new AshyanaDBEntities();


        public List<Role> BindRole()
        {
            this.objEntity.Configuration.ProxyCreationEnabled = false;
            List<Role> lstRole = new List<Role>();
            try
            {
                lstRole = objEntity.Roles.ToList();
            }
            catch(Exception e)
            {
                e.ToString();
            }
            return lstRole;
        }
        public List<Country> BindCountry()
        {
            this.objEntity.Configuration.ProxyCreationEnabled = false;

            List<Country> lstCountry = new List<Country>();
            try
            {
                lstCountry = objEntity.Countries.ToList();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return lstCountry;
        }

        public List<State> BindState(int countryId)
        {
            List<State> lstState = new List<State>();
            try
            {
                this.objEntity.Configuration.ProxyCreationEnabled = false;

                lstState = objEntity.States.Where(a => a.countryID == countryId).ToList();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return lstState;
        }

        public List<City> BindCity(int stateId)
        {
            List<City> lstCity = new List<City>();
            try
            {
                this.objEntity.Configuration.ProxyCreationEnabled = false;

                lstCity = objEntity.Cities.Where(a => a.StateID == stateId).ToList();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return lstCity;
        }
    }
}
