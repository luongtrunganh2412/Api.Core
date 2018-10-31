using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Core.Models
{
    /// <summary>
    /// Đại lý
    /// </summary>
    public class Agent
    {
        public int AmndUser { get; set; }
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int AgentLevel { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int WardId { get; set; }
        public int HamletId { get; set; }
        public string Street { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string IsLock { get; set; }
    }
}