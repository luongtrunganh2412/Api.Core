using Api.Core.Data;
using Api.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Api.Core.Controllers
{
    public class StoreController : ApiController
    {
        StoreRepository storeRepository = new StoreRepository();
        /// <summary>
        /// GetStore
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="CusId"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        public IEnumerable<Store> GetStore(string Id, string CusId)
        {
            if (Id == null)
            {
                Id = "0";
            }

            if (CusId == null)
            {
                CusId = "0";
            }
            string sMes = string.Empty;
            return storeRepository.GET_STORE(Id, CusId, ref sMes);
        }

        /// <summary>
        /// LockStore
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        public Result LockStore(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                Id = "0";
            }

            return storeRepository.LockStore(Id);
        }
       
       /// <summary>
        /// CreateStore
       /// </summary>
       /// <param name="oStore"></param>
       /// <returns></returns>
        [AcceptVerbs("POST")]
        [Route("Store/CreateStore")]
        public ReponseEntity CreateStore(Store oStore)
        {
            return storeRepository.CreatStore(oStore);
        }
        /// <summary>
        /// Update Store
        /// </summary>
        /// <param name="oStore"></param>
        /// <returns></returns>
         [AcceptVerbs("POST")]
        public ReponseEntity UpdateStore(Store oStore)
        {
            return storeRepository.UpdateStore(oStore);
        }
    }
}