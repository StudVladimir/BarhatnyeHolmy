using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Clients _currentClient = new Clients();
        public AddEditPage(Clients selectedClient)
        {
            InitializeComponent();

            if (selectedClient != null)
                _currentClient = selectedClient;

            DataContext = _currentClient;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            Datepicker1.ToString().Remove(10,8).Replace(".","-");
           
            if (string.IsNullOrWhiteSpace(_currentClient.Surame))
                errors.AppendLine("Введите фамилию клиента");
            if (string.IsNullOrWhiteSpace(_currentClient.Name))
                errors.AppendLine("Введите имя клиента");
            if (string.IsNullOrWhiteSpace(_currentClient.Patronymic))
                errors.AppendLine("Введите отчество клиента");
            if (string.IsNullOrWhiteSpace(_currentClient.Pasport))
                errors.AppendLine("Введите паспорт клиента");
            if (_currentClient.BirthDate == null)
                errors.AppendLine("Введите дату рождения клиента");
            if (string.IsNullOrWhiteSpace(_currentClient.Adress))
                errors.AppendLine("Введите адрес клиента");
            if (string.IsNullOrWhiteSpace(_currentClient.Email))
                errors.AppendLine("Введите Email клиента");
            if (string.IsNullOrWhiteSpace(_currentClient.Password))
                errors.AppendLine("Введите пароль клиента");

            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            
            BarhatnyeHolmyEntities.GetConext().Clients.Add(_currentClient);
            
            try
            {
                BarhatnyeHolmyEntities.GetConext().SaveChanges();
                MessageBox.Show("Инфомация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
