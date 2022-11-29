using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
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
using BarhatnyeHolmy;
using Captcha;


namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для EntryPage.xaml
    /// </summary>
    public partial class EntryPage : Page
    {
        private int LoginCheck = 0;
        private int AccessDenied = 0;
        public EntryPage()
        {
            InitializeComponent();

        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (AccessDenied >= 1)
            {
                Captcha.Captcha a = new Captcha.Captcha();
                a.CreateCaptcha();
                Manager.MainFrame.Navigate(a);
            }
            foreach (Employees employee in BarhatnyeHolmyEntities.GetConext().Employees.ToList())
            {
                if (employee.Login.ToString() == TBLogin.Text.ToString())
                {
                    LoginCheck++;
                    if (employee.Password.ToString() == TBPassword.Text.ToString())
                    {
                        Manager.AuthEmployee = employee;
                        Manager.MainFrame.Navigate(new Page1());
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль");
                        AccessDenied++;
                        break;
                    }
                }
            }
            if (LoginCheck == 0)
            {
                MessageBox.Show("Неверный логин");
                AccessDenied++;
            }
            
        }
    }
}
