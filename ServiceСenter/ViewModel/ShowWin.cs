using ServiceСenter.Commands;
using ServiceСenter.Model;
using ServiceСenter.View;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ServiceСenter.ViewModel
{
    public static class ShowWin
    {
        public static MainWindow mainWindow;

        #region Окна
        public static void ShowOrders()
        {
            mainWindow.ShowUserControl(new OrdersView() { DataContext = new OrdersViewModel() });
        }
        public static void ShowNewOrder(Orders order)
        {
            mainWindow.ShowUserControl(new NewOrderView() { DataContext = new NewOrderViewModel(order) });
        }
        public static void ShowWork(Orders orders)
        {
            mainWindow.ShowUserControl(new WorkView(orders));
        }
        public static void ShowDictionaries()
        {
            mainWindow.ShowUserControl(new DictionariesView() { DataContext = new DictionariesViewModel() });
        }

        
        #endregion

        #region Донолнительные Окна
        public static void ShowInfo()
        {
            mainWindow.ShowAddedWindow(new InfoView());
        }
        public static AddedView AddedTypeDevice(DicTypesDevices type)
        {
            var tbType = new TextBox() {Margin = new Thickness(10, 0, 10, 10)};
            var lblTypeE = new Label() {Content = "Необходимо заполнить Тип устройства",Foreground = Brushes.Red,Visibility = Visibility.Collapsed};
            var a = new AddedView
            { Controls = new List<Control>
            {
                new Label() {Content = "Тип устройства", Margin = new Thickness(10, 10, 10, 0)}, 
                tbType,
                lblTypeE
            } };
            if (type == null)
            {
                a.Submit = new SimpleCommand(() =>
                {
                    if (string.IsNullOrEmpty(tbType.Text.Trim()))
                    {
                        lblTypeE.Visibility = Visibility.Visible;
                        return;
                    }
                    SQL.Add(new DicTypesDevices() {Type = tbType.Text});
                    a.Close();
                });
            }
            else
            {
                tbType.Text = type.Type;
                a.Submit = new SimpleCommand(() =>
                {
                    if (string.IsNullOrEmpty(tbType.Text.Trim()))
                    {
                        lblTypeE.Visibility = Visibility.Visible;
                        return;
                    }
                    SQL.Update(new DicTypesDevices() { IdType = type.IdType, Type = tbType.Text });
                    a.Close();
                });
            }

            a.Init();
            mainWindow.ShowAddedWindow(a);
            return a;
        }
        public static AddedView AddedFabricator(DicFabricator fabricator)
        {
            var tbFabricator = new TextBox() { Margin = new Thickness(10, 0, 10, 10) };
            var lblFE = new Label() { Content = "Необходимо заполнить Производитель", Foreground = Brushes.Red, Visibility = Visibility.Collapsed };
            var a = new AddedView
            {
                Controls = new List<Control> { new Label() { Content = "Производитель", Margin = new Thickness(10, 10, 10, 0) }, 
                tbFabricator,
                lblFE}
            };
            if (fabricator == null)
            {
                a.Submit = new SimpleCommand(() =>
                {
                    if (string.IsNullOrEmpty(tbFabricator.Text.Trim()))
                    {
                        lblFE.Visibility = Visibility.Visible;
                        return;
                    }
                    SQL.Add(new DicFabricator() { Fabricator = tbFabricator.Text });
                    a.Close();
                });
            }
            else
            {
                tbFabricator.Text = fabricator.Fabricator;
                a.Submit = new SimpleCommand(() =>
                {
                    if (string.IsNullOrEmpty(tbFabricator.Text.Trim()))
                    {
                        lblFE.Visibility = Visibility.Visible;
                        return;
                    }
                    SQL.Update(new DicFabricator() { IdFabricator = fabricator.IdFabricator, Fabricator = tbFabricator.Text });
                    a.Close();
                });
            }

            a.Init();
            mainWindow.ShowAddedWindow(a);
            return a;
        }
        public static AddedView AddedEquipment(DicEquipment equipment)
        {
            var cbType = new ComboBox() {Margin = new Thickness(10, 0, 10, 10), ItemsSource = SQL.GetTypesDevices().Select(s=>s.Type)};
            var lblT = new Label() { Content = "Необходимо заполнить Тип устройства", Foreground = Brushes.Red, Visibility = Visibility.Collapsed };
            var tbEquip = new TextBox() {Margin = new Thickness(10, 0, 10, 10)};
            var lblE = new Label() { Content = "Необходимо заполнить Комплектацию", Foreground = Brushes.Red, Visibility = Visibility.Collapsed };
            var a = new AddedView
            {
                Controls = new List<Control>
                {
                    new Label() {Content = "Тип устройства", Margin = new Thickness(10, 0, 10, 0)},
                    cbType, 
                    lblT,
                    new Label() {Content = "Комплектация", Margin = new Thickness(10, 0, 10, 0)},
                    tbEquip,
                    lblE
                }
            };

            if (equipment == null)
            {
                a.Submit = new SimpleCommand(() =>
                {
                    if (string.IsNullOrEmpty(cbType.Text.Trim()) || (string.IsNullOrEmpty(tbEquip.Text.Trim())))
                    {
                        lblT.Visibility = (string.IsNullOrEmpty(cbType.Text.Trim())) ? Visibility.Visible : Visibility.Collapsed;
                        lblE.Visibility = (string.IsNullOrEmpty(tbEquip.Text.Trim())) ? Visibility.Visible : Visibility.Collapsed;
                        return;
                    }
                    SQL.Add(new DicEquipment()
                    {
                        IdTypeDevices = SQL.GetIdTypeDevice(cbType.SelectedItem.ToString()),
                        Equipment = tbEquip.Text
                    });
                    a.Close();
                });
            }
            else
            {
                tbEquip.Text = equipment.Equipment;
                cbType.SelectedValue = SQL.GetTypesDevices(equipment.IdTypeDevices);

                a.Submit = new SimpleCommand(() =>
                {
                    if (string.IsNullOrEmpty(cbType.Text.Trim()) || (string.IsNullOrEmpty(tbEquip.Text.Trim())))
                    {
                        lblT.Visibility = (string.IsNullOrEmpty(cbType.Text.Trim())) ? Visibility.Visible : Visibility.Collapsed;
                        lblE.Visibility = (string.IsNullOrEmpty(tbEquip.Text.Trim())) ? Visibility.Visible : Visibility.Collapsed;
                        return;
                    }
                    SQL.Update(new DicEquipment()
                    {
                        IdEquipment = equipment.IdEquipment,
                        IdTypeDevices = SQL.GetIdTypeDevice(cbType.SelectedItem.ToString()),
                        Equipment = tbEquip.Text
                    });
                    a.Close();
                });
            }
            a.Init();
            mainWindow.ShowAddedWindow(a);
            return a;
        }

        public static AddedView AddedModel(DicModel model)
        {
            var cbFabricator = new ComboBox() { Margin = new Thickness(10, 0, 10, 10), ItemsSource = SQL.GetFabricator().Select(s=>s.Fabricator) };
            var lblF = new Label() { Content = "Необходимо заполнить Производителя", Foreground = Brushes.Red, Visibility = Visibility.Collapsed };
            var tbModel = new TextBox() { Margin = new Thickness(10, 0, 10, 10) };
            var lblM = new Label() { Content = "Необходимо заполнить Модель", Foreground = Brushes.Red, Visibility = Visibility.Collapsed };
            var a = new AddedView
            {
                Controls = new List<Control>
                {
                    new Label() {Content = "Производитель", Margin = new Thickness(10, 0, 10, 0)},
                    cbFabricator,
                    lblF,
                    new Label() {Content = "Модель", Margin = new Thickness(10, 0, 10, 0)},
                    tbModel,
                    lblM
                }
            };

            if (model == null)
            {
                a.Submit = new SimpleCommand(() =>
                {
                    if (string.IsNullOrEmpty(cbFabricator.Text.Trim()) || (string.IsNullOrEmpty(tbModel.Text.Trim())))
                    {
                        lblF.Visibility = (string.IsNullOrEmpty(cbFabricator.Text.Trim())) ? Visibility.Visible : Visibility.Collapsed;
                        lblM.Visibility = (string.IsNullOrEmpty(tbModel.Text.Trim())) ? Visibility.Visible : Visibility.Collapsed;
                        return;
                    }
                    SQL.Add(new DicModel()
                    {
                        IdFabricator = SQL.GetIdFabricator(cbFabricator.SelectedItem.ToString()),
                        Model = tbModel.Text
                    });
                    a.Close();
                });
            }
            else
            {
                tbModel.Text = model.Model;
                cbFabricator.SelectedValue = SQL.GetFabricator(model.IdFabricator);

                a.Submit = new SimpleCommand(() =>
                {
                    if (string.IsNullOrEmpty(cbFabricator.Text.Trim()) || (string.IsNullOrEmpty(tbModel.Text.Trim())))
                    {
                        lblF.Visibility = (string.IsNullOrEmpty(cbFabricator.Text.Trim())) ? Visibility.Visible : Visibility.Collapsed;
                        lblM.Visibility = (string.IsNullOrEmpty(tbModel.Text.Trim())) ? Visibility.Visible : Visibility.Collapsed;
                        return;
                    }
                    SQL.Update(new DicModel()
                    {
                        IdModel = model.IdModel,
                        IdFabricator = SQL.GetIdFabricator(cbFabricator.SelectedItem.ToString()),
                        Model = tbModel.Text
                    });
                    a.Close();
                });
            }
            
            a.Init();
            mainWindow.ShowAddedWindow(a);
            return a;
        }

        public static AddedView AddedAppearance(DicAppearance appearance)
        {
            var tbAppearance = new TextBox() { Margin = new Thickness(10, 0, 10, 10) };
            var lblA = new Label() { Content = "Необходимо заполнить Внешний вид", Foreground = Brushes.Red, Visibility = Visibility.Collapsed };
            var a = new AddedView
            {
                Controls = new List<Control>
                {
                    new Label() {Content = "Внешний вид", Margin = new Thickness(10, 0, 10, 0)},
                    tbAppearance,
                    lblA
                }
            };

            if (appearance == null)
            {
                a.Submit = new SimpleCommand(() =>
                {
                    if (string.IsNullOrEmpty(tbAppearance.Text.Trim()))
                    {
                        lblA.Visibility = Visibility.Visible;
                        return;
                    }
                    SQL.Add(new DicAppearance()
                    {
                        Appearance = tbAppearance.Text
                    });
                    a.Close();
                });
            }
            else
            {
                tbAppearance.Text = appearance.Appearance;
                
                a.Submit = new SimpleCommand(() =>
                {
                    if (string.IsNullOrEmpty(tbAppearance.Text.Trim()))
                    {
                        lblA.Visibility = Visibility.Visible;
                        return;
                    }
                    SQL.Update(new DicAppearance()
                    {
                        IdAppearance = appearance.IdAppearance,
                        Appearance = tbAppearance.Text
                    });
                    a.Close();
                });
            }

            a.Init();
            mainWindow.ShowAddedWindow(a);
            return a;
        }

        public static AddedView AddedMalfunction(DicMalfunction malfunction)
        {
            var tbMalfunction = new TextBox() { Margin = new Thickness(10, 0, 10, 10) };
            var lblM = new Label() { Content = "Необходимо заполнить Неисправность", Foreground = Brushes.Red, Visibility = Visibility.Collapsed };
            var a = new AddedView
            {
                Controls = new List<Control>
                {
                    new Label() {Content = "Неисправность", Margin = new Thickness(10, 0, 10, 0)},
                    tbMalfunction
                }
            };

            if (malfunction == null)
            {
                a.Submit = new SimpleCommand(() =>
                {
                    if (string.IsNullOrEmpty(tbMalfunction.Text.Trim()))
                    {
                        lblM.Visibility = Visibility.Visible;
                        return;
                    }
                    SQL.Add(new DicMalfunction()
                    {
                        Malfunction = tbMalfunction.Text
                    });
                    a.Close();
                });
            }
            else
            {
                tbMalfunction.Text = malfunction.Malfunction;

                a.Submit = new SimpleCommand(() =>
                {
                    if (string.IsNullOrEmpty(tbMalfunction.Text.Trim()))
                    {
                        lblM.Visibility = Visibility.Visible;
                        return;
                    }
                    SQL.Update(new DicMalfunction()
                    {
                        IdMalfunction = malfunction.IdMalfunction,
                        Malfunction = tbMalfunction.Text
                    });
                    a.Close();
                });
            }

            a.Init();
            mainWindow.ShowAddedWindow(a);
            return a;
        }
        public static AddedView AddedWork(Works works)
        {
            var cbWork = new ComboBox()
            {
                Text = SQL.GetWork(works.IdWork),
                Margin = new Thickness(10, 0, 10, 10),
                ItemsSource = SQL.GetDicWork(),
                IsEditable = true
            };
            var tbPrice = new TextBox()
            {
                Text = works.Price.ToString(CultureInfo.InvariantCulture),
                Margin = new Thickness(10, 0, 10, 10)
            };
            var tbCount = new TextBox() {Text = works.Count.ToString(), Margin = new Thickness(10, 0, 10, 10)};

            var lblW = new Label() { Content = "Необходимо заполнить Работу", Foreground = Brushes.Red, Visibility = Visibility.Collapsed };
            var lblP = new Label() { Content = "Необходимо заполнить Цену", Foreground = Brushes.Red, Visibility = Visibility.Collapsed };
            var lblPInt = new Label() { Content = "Цена должна быть числом", Foreground = Brushes.Red, Visibility = Visibility.Collapsed };
            var lblC = new Label() { Content = "Необходимо заполнить Количество", Foreground = Brushes.Red, Visibility = Visibility.Collapsed };
            var lblCInt = new Label() { Content = "Количество должно быть числом", Foreground = Brushes.Red, Visibility = Visibility.Collapsed };
            var a = new AddedView
            {
                Controls = new List<Control>
                {
                    new Label() {Content = "Работа", Margin = new Thickness(10, 0, 10, 0)},
                    cbWork, lblW,
                    new Label() {Content = "Цена", Margin = new Thickness(10, 0, 10, 0)},
                    tbPrice, lblP, lblPInt,
                    new Label() {Content = "Количество", Margin = new Thickness(10, 0, 10, 0)},
                    tbCount, lblC,lblCInt
                }
            };

            a.Submit = new SimpleCommand(() =>
            {
                int res;
                lblW.Visibility = (string.IsNullOrEmpty(cbWork.Text.Trim())) ? Visibility.Visible : Visibility.Collapsed;
                lblP.Visibility = (string.IsNullOrEmpty(tbPrice.Text.Trim())) ? Visibility.Visible : Visibility.Collapsed;
                lblC.Visibility = (string.IsNullOrEmpty(tbCount.Text.Trim())) ? Visibility.Visible : Visibility.Collapsed;
                lblPInt.Visibility = (!Int32.TryParse(tbPrice.Text.Trim(), out res)) ? Visibility.Visible : Visibility.Collapsed;
                lblCInt.Visibility = (!Int32.TryParse(tbCount.Text.Trim(), out res)) ? Visibility.Visible : Visibility.Collapsed;
                if (lblW.Visibility == Visibility.Visible || lblP.Visibility == Visibility.Visible 
                    || lblC.Visibility == Visibility.Visible || lblPInt.Visibility == Visibility.Visible || lblCInt.Visibility == Visibility.Visible)
                    return;
                
                if (!SQL.GetDicWork().Contains(cbWork.Text))
                    SQL.Add(new DicWork() {Work = cbWork.Text, Price = Int32.Parse(tbPrice.Text)});
                
                var newW = new Works()
                {
                    Id = works.Id,
                    IdOrder = works.IdOrder,
                    IdWork = SQL.GetIdWork(cbWork.Text),
                    Count = Int32.Parse(tbCount.Text) == 0 ? 1 : Int32.Parse(tbCount.Text),
                    Price = Int32.Parse(tbPrice.Text)
                };
                string message = "";
                if (works.IdWork != newW.IdWork)
                    message += String.Format("Работа с {0} на {1}; ", SQL.GetWork(works.IdWork), cbWork.Text);
                if (works.Price != newW.Price)
                    message += String.Format("Цена с {0} на {1};", works.Price, newW.Price);
                if (works.Count != newW.Count)
                    message += String.Format("Количество с {0} на {1}; ", works.Count, newW.Count);

                SQL.AddHistory(works.IdOrder, String.Format("Работа {1} изменена: {0}", message, SQL.GetWork(works.IdWork)));
                SQL.Update(newW);
                a.Close();
            });

            a.Init();
            mainWindow.ShowAddedWindow(a);
            return a;
        }

        public static AddedView AddedDicWork(DicWork work)
        {
            var tbWork = new TextBox() { Margin = new Thickness(10, 0, 10, 10) };
            var tbPrice = new TextBox() { Margin = new Thickness(10, 0, 10, 10) };
            var tbDescription = new TextBox() { Margin = new Thickness(10, 0, 10, 10) };
            var a = new AddedView
            {
                Controls = new List<Control>
                {
                    new Label() { Content = "Работа", Margin = new Thickness(10, 10, 10, 0) }, 
                    tbWork,
                    new Label() { Content = "Цена", Margin = new Thickness(10, 10, 10, 0) }, 
                    tbPrice,
                    new Label() { Content = "Описание", Margin = new Thickness(10, 10, 10, 0) }, 
                    tbDescription
                }
            };
            if (work == null)
            {
                a.Submit = new SimpleCommand(() =>
                {
                    SQL.Add(new DicWork() { Work = tbWork.Text, Price = Int32.Parse(tbPrice.Text), Description = tbDescription.Text});
                    a.Close();
                });
            }
            else
            {
                tbWork.Text = work.Work;
                tbPrice.Text = work.Price.ToString();
                tbDescription.Text = work.Description;
                a.Submit = new SimpleCommand(() =>
                {
                    SQL.Update(new DicWork() { Work = tbWork.Text, Price = Int32.Parse(tbPrice.Text), Description = tbDescription.Text });
                    a.Close();
                });
            }

            a.Init();
            mainWindow.ShowAddedWindow(a);
            return a;
        }

        public static AddedView AddedExecutors(Executors executors)
        {
            var tbExecutors = new TextBox() { Margin = new Thickness(10, 0, 10, 10) };
            var a = new AddedView { Controls = new List<Control> { new Label() { Content = "Исполнитель", Margin = new Thickness(10, 10, 10, 0) }, tbExecutors } };
            if (executors == null)
            {
                a.Submit = new SimpleCommand(() =>
                {
                    SQL.Add(new Executors() { Name = tbExecutors.Text });
                    a.Close();
                });
            }
            else
            {
                tbExecutors.Text = executors.Name;
                a.Submit = new SimpleCommand(() =>
                {
                    SQL.Update(new Executors() { IdExecutor = executors.IdExecutor, Name = tbExecutors.Text });
                    a.Close();
                });
            }

            a.Init();
            mainWindow.ShowAddedWindow(a);
            return a;
        }

        #endregion
    }
}
