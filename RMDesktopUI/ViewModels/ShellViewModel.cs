using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using RMDesktopUI.EventsModel;
using RMDesktopUI.Library;
using RMDesktopUI.Library.Api;

namespace RMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private LoginViewModel _loginVM;
        private IEventAggregator _events;
        private SaleViewModel _salesVM;
        private SimpleContainer _container;
        private ILoggInUserModel _user;
        private IAPIHelper _apiHelper;

        public ShellViewModel(LoginViewModel loginVM, IEventAggregator events, SaleViewModel salesVM, SimpleContainer container, ILoggInUserModel user, IAPIHelper apiHelper)
        {
            _events = events;
            _salesVM = salesVM;
            _loginVM = loginVM;
            _container = container;
            _user = user;
            _apiHelper = apiHelper;

            _events.Subscribe(this);

            ActivateItem(IoC.Get<LoginViewModel>());
        }
        public bool IsLoggIn
        {
            get
            {
                bool output = false;

                if (string.IsNullOrWhiteSpace(_user.Token)== false)
                {
                    output = true;
                }
                return output;
            }


        }
        public void ExitApp()
        {
            TryClose();
        }

        public void UserManagment()
        {
            ActivateItem(IoC.Get<UserDisplayViewModel>());
        }

        public void LogOut()
        {
            _user.ResetUserModel();
            _apiHelper.LogOffUser();
            ActivateItem(IoC.Get<LoginViewModel>());
            NotifyOfPropertyChange(() => IsLoggIn);
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVM);
            NotifyOfPropertyChange(() => IsLoggIn);
            
        }


    }
}
