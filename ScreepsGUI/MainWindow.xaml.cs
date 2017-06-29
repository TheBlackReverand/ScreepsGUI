using ScreepsGUI.ClientAPI;
using ScreepsGUI.DTO;
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

namespace ScreepsGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            if(String.IsNullOrEmpty(txtLogin.Text) || String.IsNullOrEmpty(pwdPassword.Password))
            {
                MessageBox.Show("Please, enter login and password before");

                return;
            }

            AuthenticationAnswer authenticationAnswer = AccountControler.Authenticate(txtLogin.Text, pwdPassword.Password);

            txbToken.Text = authenticationAnswer.Token;

            if (!String.IsNullOrEmpty(Context.Token))
            {
                Account account = AccountControler.GeAccountInformation();

                txbLogin.Text = account.Login;
                txbEmail.Text = account.Email;
                txbUsername.Text = account.Username;
                txbSteamDisplayName.Text = account.SteamDisplayName;
            }
            else
            {
                MessageBox.Show("Authentication error : " + authenticationAnswer.ErrorType);
            }
        }

        private void btnDeconnect_Click(object sender, RoutedEventArgs e)
        {
            AccountControler.Disconnect();

            txbToken.Text = string.Empty;
            txbLogin.Text = string.Empty;
            txbEmail.Text = string.Empty;
            txbUsername.Text = string.Empty;
            txbSteamDisplayName.Text = string.Empty;
        }
    }
}