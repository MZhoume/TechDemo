using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TechDemo.Interface.Client
{
    public abstract class AbsDisplayControl : ContentControl
    {
        public abstract void PopulateData(AbsDataModel data);
    }
}
