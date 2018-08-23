using System;
using System.Timers;
using System.Windows.Controls;
using ServiceСenter.ViewModel;

namespace ServiceСenter.View
{
    public partial class DictionariesView : UserControl
    {
        Timer t = new Timer(1000);
        public DictionariesView()
        {
            InitializeComponent();
            t.Elapsed += t_Elapsed;
            t.Start();
        }

        void t_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                var dc = (this.DataContext as DictionariesViewModel);
                if (dc != null)
                {
                    if (!Equals(DgTypeDevice.ItemsSource, dc.TypesDevicesEntries))
                        DgTypeDevice.ItemsSource = dc.TypesDevicesEntries;
                    if (!Equals(DgAppearance.ItemsSource, dc.AppearanceEntries))
                        DgAppearance.ItemsSource = dc.AppearanceEntries;
                    if (!Equals(DgEquipment.ItemsSource, dc.EquipmentEntries))
                        DgEquipment.ItemsSource = dc.EquipmentEntries;
                    if (!Equals(DgFabricator.ItemsSource, dc.FabricatorEntries))
                        DgFabricator.ItemsSource = dc.FabricatorEntries;
                    if (!Equals(DgMalfunction.ItemsSource, dc.MalfunctionEntries))
                        DgMalfunction.ItemsSource = dc.MalfunctionEntries;
                    if (!Equals(DgModel.ItemsSource, dc.ModelEntries))
                        DgModel.ItemsSource = dc.ModelEntries;
                    if (!Equals(DgWork.ItemsSource, dc.WorkEntries))
                        DgWork.ItemsSource = dc.WorkEntries;
                    
                }
            }));
        }
    }
}
