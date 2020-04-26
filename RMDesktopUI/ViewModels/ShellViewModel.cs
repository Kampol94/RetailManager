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
        }

        //public void Handle(LogOnEvent message)
        //{
        //    ActivateItem(_salesVM);
        //    NotifyOfPropertyChange(() => IsLoggIn);
            
        //}

        public async Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync(_salesVM, cancellationToken);
            NotifyOfPropertyChange(() => IsLoggIn);
        }
    }
}
