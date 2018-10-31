using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Core.Models
{
    /// <summary>
    /// ReponseEntity
    /// </summary>
    public class ReponseEntity
    {
        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }

        public int Id { get; set; }
    }
}