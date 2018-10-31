using Api.Core.Common;
using Api.Core.Models;
using Api.Core.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace Api.Core.Controllers
{
    public class FeeController : ApiController
    {
        EmsRepository ems_repository = new EmsRepository();
        [AcceptVerbs("GET")]
        public ResponseFee GetFee(int from, int to, int weight, int amount)
        {
            ResponseFee responsefee = new ResponseFee();
            double f = 0; double c = 0;
            Models.Data data = new Models.Data();
            try
            {
                f = ems_repository.Get_Postage(from, to, weight);
                c = ems_repository.Get_Cod_Fee(amount);
                responsefee.Code = "00";
                responsefee.Message = "Success";
                data.CoDFee = c;
                data.TransportFee = f;
                responsefee.data = data;
                responsefee.CalculatedFee = f + c;
                responsefee.WeightDimension = weight;
            }
            catch(Exception ex)
            {
                responsefee.Code = "99";
                responsefee.Message = "Error Processing. " + ex.Message;              
            }         
            return responsefee;
        }
        [AcceptVerbs("GET")]
        public ResponseFee GetFee(int from, int to, int weight)
        {
            ResponseFee responsefee = new ResponseFee();
            double f = 0; Models.Data data = new Models.Data();
            try
            {
                f = ems_repository.Get_Postage(from, to, weight);
                responsefee.Code = "00";
                responsefee.Message = "Success";
                data.CoDFee = 0;
                data.TransportFee = f;
                responsefee.data = data;
                responsefee.CalculatedFee = f;
                responsefee.WeightDimension = weight;
            }
            catch (Exception ex)
            {
                responsefee.Code = "99";
                responsefee.Message = "Error Processing. " + ex.Message;
              
            }
            return responsefee;
        }
        [AcceptVerbs("GET")]
        public ResponseFee GetCodeFee(int amount)
        {
            ResponseFee responsefee = new ResponseFee();
            double c = 0; Models.Data data = new Models.Data();
            try
            {
                c = ems_repository.Get_Cod_Fee(amount);
                responsefee.Code = "00";
                responsefee.Message = "Success";
                data.CoDFee = c;
                data.TransportFee = 0;
                responsefee.data = data;
                responsefee.CalculatedFee = c;
                responsefee.WeightDimension = 0;
            }
            catch (Exception ex)
            {
                responsefee.Code = "99";
                responsefee.Message = "Error Processing. " + ex.Message;
              
            }
            return responsefee;
            
        }
    }
}
