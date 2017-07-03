using ScreepsGUI.ClientAPI;
using ScreepsGUI.DTO;
using ScreepsGUI.Tools.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ScreepsGUI.ViewModel
{
    public class MainWindowModel : ViewModelBase
    {
        #region Properties

        public string Token { get; set; }
        public string Login { get; set; }

        public Account AccountInformation { get; private set; }

        private bool IsLoggedIn { get { return !String.IsNullOrEmpty(Context.Token); } }

        #endregion

        #region Commands

        #region Command Authenticate

        private ICommand authenticateCommand;
        public ICommand AuthenticateCommand
        {
            get
            {
                if (authenticateCommand == null)
                {
                    authenticateCommand = new RelayCommand<PasswordBox>(Authenticate, CanAuthenticate);
                }

                return authenticateCommand;
            }
        }

        private void Authenticate(PasswordBox pwdPassword)
        {
            if (String.IsNullOrEmpty(Login) || String.IsNullOrEmpty(pwdPassword.Password))
            {
                // MessageBox.Show("Please, enter login and password before");

                return;
            }

            AuthenticationAnswer authenticationAnswer = AccountControler.Authenticate(Login, pwdPassword.Password);

            pwdPassword.Clear();

            Token = authenticationAnswer.Token;

            if (!String.IsNullOrEmpty(Context.Token))
            {
                AccountInformation = AccountControler.GeAccountInformation();
            }
            else
            {
                // MessageBox.Show("Authentication error : " + authenticationAnswer.ErrorType);
            }
        }

        private bool CanAuthenticate(PasswordBox pwdPassword)
        {
            return !String.IsNullOrEmpty(Login) && !String.IsNullOrEmpty(pwdPassword.Password) && !IsLoggedIn;
        }

        #endregion

        #region Command Disconnect

        private ICommand disconnectCommand;
        public ICommand DisconnectCommand
        {
            get
            {
                if (disconnectCommand == null)
                {
                    disconnectCommand = new RelayCommand(Disconnect, CanDisconnect);
                }

                return disconnectCommand;
            }
        }

        private void Disconnect()
        {
            AccountControler.Disconnect();

            Token = string.Empty;

            AccountInformation = null;

            Login = string.Empty;
        }

        private bool CanDisconnect()
        {
            return IsLoggedIn;
        }

        #endregion

        #endregion
    }
}