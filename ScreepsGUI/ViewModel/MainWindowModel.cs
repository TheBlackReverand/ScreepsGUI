using ScreepsGUI.ClientAPI;
using ScreepsGUI.ClientAPI.Controlers;
using ScreepsGUI.ClientAPI.DTO;
using ScreepsGUI.ClientAPI.DTO.Enum;
using ScreepsGUI.Tools.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ScreepsGUI.ViewModel
{
    public class MainWindowModel : ViewModelBase
    {
        public MainWindowModel()
        {
            #region Liste Interval

            Intervals = new ObservableCollection<Interval>((IEnumerable<Interval>)Enum.GetValues(typeof(Interval)));

            IntervalsCollectionView = CollectionViewSource.GetDefaultView(Intervals);

            if (IntervalsCollectionView == null)
                throw new NullReferenceException("'IntervalsCollectionView' is null in Constructor");

            IntervalsCollectionView.CurrentChanged += OnIntervalsCollectionViewCurrentChanged;

            #endregion Liste Interval
        }

        #region Properties

        public string Token { get; set; }
        public string Login { get; set; }

        public MyAccount MyAccount { get; private set; }

        private bool IsLoggedIn { get { return !String.IsNullOrEmpty(Context.Token); } }

        public string FindUserUsername { get; set; }
        public UserAccount FindedUser { get; private set; }

        public string RoomName { get; set; }
        public RoomOverview RoomOverview { get; private set; }

        #endregion

        #region Liste Interval

        public ObservableCollection<Interval> Intervals { get; private set; }

        private readonly ICollectionView IntervalsCollectionView;
        public Interval CurrentInterval
        {
            get
            {
                if (IntervalsCollectionView.CurrentItem == null)
                {
                    return Interval.None;
                }
                else
                {
                    return (Interval)IntervalsCollectionView.CurrentItem;
                }
            }
        }

        private void OnIntervalsCollectionViewCurrentChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("CurrentInterval");
        }

        #endregion Liste Interval

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
                MyAccount = AccountControler.GetMyAccount();
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

            MyAccount = null;

            Login = string.Empty;
        }

        private bool CanDisconnect()
        {
            return IsLoggedIn;
        }

        #endregion

        #region Command FindUser

        private ICommand findUserCommand;
        public ICommand FindUserCommand
        {
            get
            {
                if (findUserCommand == null)
                {
                    findUserCommand = new RelayCommand(FindUser, CanFindUser);
                }

                return findUserCommand;
            }
        }

        private void FindUser()
        {
            SearchUserResult searchUserResult = UserControler.SearchUser(FindUserUsername);

            if (searchUserResult.Success && searchUserResult.UserFound != null)
            {
                FindedUser = searchUserResult.UserFound;
            }
        }

        private bool CanFindUser()
        {
            return !String.IsNullOrEmpty(FindUserUsername);
        }

        #endregion

        #region Command RoomOverview

        private ICommand getRoomOverviewCommand;
        public ICommand GetRoomOverviewCommand
        {
            get
            {
                if (getRoomOverviewCommand == null)
                {
                    getRoomOverviewCommand = new RelayCommand(GetRoomOverview, CanGetRoomOverview);
                }

                return getRoomOverviewCommand;
            }
        }

        private void GetRoomOverview()
        {
            RoomOverview = GameControler.Room.GetRoomOverview(RoomName, CurrentInterval);
        }

        private bool CanGetRoomOverview()
        {
            return IsLoggedIn && !String.IsNullOrEmpty(RoomName);
        }

        #endregion

        #endregion
    }
}