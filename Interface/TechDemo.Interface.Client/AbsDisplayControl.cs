using System.Windows.Controls;

namespace TechDemo.Interface.Client
{
    public abstract class AbsDisplayControl : ContentControl
    {
        public abstract void PopulateData(AbsDataModel data);
    }
}
