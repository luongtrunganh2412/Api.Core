using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Core.Models
{
    public class ReturnShipment
    {
        public string Code { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public int Total { get; set; }

        public CShipment Shipment { get; set; }

        public List<CShipment> ListShipment;
    }

    public class ReturnShipment_v2
    {
        public string Code { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public int Total { get; set; }

        public CShipment_v2 Shipment { get; set; }

        //public List<CShipment_v2> ListShipment;
    }
}