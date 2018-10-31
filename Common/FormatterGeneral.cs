﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common
{
    public class FormatterGeneral : FormatterBase
    {
        private string _message = string.Empty;

        public FormatterGeneral(string message)
        {
            _message = message;
        }

        public override string Message
        {
            get
            {
                return _message;
            }
        }
    }
}