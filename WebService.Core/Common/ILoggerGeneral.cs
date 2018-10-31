﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Core.Common
{
    public interface ILoggerGeneral
    {
        void Write(FormatterBase formatter, string logFileName);
    }
}
