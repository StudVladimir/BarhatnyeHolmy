using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BarhatnyeHolmy
{
    /// <summary>
    /// Логика взаимодействия для BarhatnyeHolmyPage1.xaml
    /// </summary>
    public partial class BarhatnyeHolmyPage1 : Page
    {
        public BarhatnyeHolmyPage1()
        {
            InitializeComponent();
            DGridClients.ItemsSource = BarhatnyeHolmyEntities.GetConext().Clients.ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Clients));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var clientsForRemoving = DGridClients.SelectedItems.Cast<Clients>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {clientsForRemoving.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    BarhatnyeHolmyEntities.GetConext().Clients.RemoveRange(clientsForRemoving);
                    BarhatnyeHolmyEntities.GetConext().SaveChanges();
                    MessageBox.Show("Данные сохранены");
                    DGridClients.ItemsSource = BarhatnyeHolmyEntities.GetConext().Clients.ToList();
                }
                catch(Exception exeption) 
                {
                    MessageBox.Show(exeption.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility==Visibility.Visible)
            {
                BarhatnyeHolmyEntities.GetConext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridClients.ItemsSource = BarhatnyeHolmyEntities.GetConext().Clients.ToList();
            }
        }
    }
}
