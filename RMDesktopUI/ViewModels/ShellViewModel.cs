using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private SimpleContainer _container;
        private ILoggInUserModel _user;
        private IAPIHelper _apiHelper;

        public ShellViewModel(LoginViewModel loginVM,
                              IEventAggregator events,
                              SimpleContainer container,
                              ILoggInUserModel user,
                              IAPIHelper apiHelper)
        {
            _events = events;;
            _loginVM = loginVM;
            _container = container;
            _user = user;
            _apiHelper = apiHelper;

            _events.SubscribeOnPublishedThread(this);

            ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());
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

        public bool IsLoggOut
        {
            get
            {
                return !IsLoggIn;
            }


        }

        public void ExitApp()
        {
            TryCloseAsync();
        }

        public async Task UserManagment()
        {
            await ActivateItemAsync(IoC.Get<UserDisplayViewModel>(), new CancellationToken());
        }

        public async Task LogOut()
        {
            _user.ResetUserModel();
            _apiHelper.LogOffUser();
            await ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());
            NotifyOfPropertyChange(() => IsLoggIn);
            NotifyOfPropertyChange(() => IsLoggOut);
        }

        public async Task LogIn()
        {
            await ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());
        }
        public async Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync(IoC.Get<SaleViewModel>(), cancellationToken);
            NotifyOfPropertyChange(() => IsLoggIn);
            NotifyOfPropertyChange(() => IsLoggOut);
        }
    }
}
