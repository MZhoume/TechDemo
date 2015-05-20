﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechDemo.Interface.Server
{
    public interface IServiceFactory
    {
        IDataService CreateService(params object[] objs);
    }
}
