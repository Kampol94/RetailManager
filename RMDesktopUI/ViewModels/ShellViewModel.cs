using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using RMDesktopUI.EventsModel;

namespace RMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private LoginViewModel _loginVM;
        private IEventAggregator _events;
        private SaleViewModel _salesVM;
        private SimpleContainer _container;

        public ShellViewModel(LoginViewModel loginVM, IEventAggregator events, SaleViewModel salesVM, SimpleContainer container)
        {
            _events = events;
            _salesVM = salesVM;
            _loginVM = loginVM;
            _container = container;

            _events.Subscribe(this);

            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVM);
            
        }
    }
}
