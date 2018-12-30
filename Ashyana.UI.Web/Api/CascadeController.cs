using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ashyana.UI.Web.Models;
using Ashyana.UI.Web.Common;

namespace Ashyana.UI.Web.Api
{
    public class CascadeController : ApiController
    {
        CascadeList objBinder = new CascadeList();


        [HttpGet]
        public List<Role> GetRole()
        {
            List<Role> roleDetail = new List<Role>();
            try
            {
                roleDetail = objBinder.BindRole();
            }
            catch (ApplicationException ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = ex.Message });
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadGateway, ReasonPhrase = ex.Message });
            }
            return roleDetail;
        }



        [HttpGet]
        public List<Country> GetCountryList()
        {
            List<Country> countryDetail = new List<Country>();
            try
            {
                countryDetail = objBinder.BindCountry();
            }
            catch (ApplicationException ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = ex.Message });
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadGateway, ReasonPhrase = ex.Message });
            }

            return countryDetail;
        }

        [HttpGet]

        public List<State> GetState(int CountryId)
        {

            List<State> stateDetail = new List<State>();
            try
            {
                stateDetail = objBinder.BindState(CountryId);
            }
            catch (ApplicationException ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = ex.Message });
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadGateway, ReasonPhrase = ex.Message });
            }

            return stateDetail;
        }

        [HttpGet]
        // [Route("CityDetails")]
        public List<City> GetCity(int stateId)
        {
            List<City> cityDetail = new List<City>();
            try
            {
                cityDetail = objBinder.BindCity(stateId);
            }
            catch (ApplicationException ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = ex.Message });
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadGateway, ReasonPhrase = ex.Message });
            }

            return cityDetail;
        }



        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}