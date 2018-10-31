using Api.Core.Data;
using Api.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Core.Controllers
{
    /// <summary>
    /// Agent Controller
    /// </summary>
    public class AgentController : ApiController
    {
        AgentRepository _agentRepository = new AgentRepository();  

        /// <summary>
        /// Lấy danh sách Đại lý
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Agent> GetAllAgents()
        {
            return _agentRepository.GetAllAgents();
        }

        /// <summary>
        /// Lấy thông tin Đại lý
        /// </summary>
        /// <param name="id">Id đại lý</param>
        /// <returns></returns>
        public Agent GetAgentById(int id)
        {
            return _agentRepository.GetAgentById(id);
        }

        /// <summary>
        /// Tạo mới Đại lý
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        [HttpPost]
        public ReponseEntity CreateAgent(Agent agent)
        {
            return _agentRepository.CreateAgent(agent);
        }
    }
}
