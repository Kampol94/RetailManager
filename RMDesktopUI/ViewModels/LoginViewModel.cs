using Caliburn.Micro;
using RMDesktopUI.EventsModel;
using RMDesktopUI.Library.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _userName;
        private string _password;
        private IAPIHelper _apiHelper;
        private IEventAggregator _events;

        public LoginViewModel(IAPIHelper apiHelper, IEventAggregator events)
        {
            _apiHelper = apiHelper;
            _events = events;
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);

            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
               NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        

        public bool IsErrorVisible
        {
            get
            {
                bool output = false;

                if (ErrorMessege?.Length > 0)
                {
                    output = true;
                }
                return output;
            }
            
            
        }

        private string _errorMessege;

        public string ErrorMessege
        {
            get { return _errorMessege; }
            set
            {
                _errorMessege = value;
                NotifyOfPropertyChange(() => IsErrorVisible);
                NotifyOfPropertyChange(() => ErrorMessege);
                
            }
        }



        public bool CanLogIn
        {
            get
            {
                bool output = false;
                if (UserName?.Length > 0 && Password?.Length > 0)
                {
                    output = true;
                }
                return output;
            }
        }

        public async Task LogIn()
        {
            try
            {
                ErrorMessege = "";
                var result = await _apiHelper.Authenticate(UserName, Password);

                await _apiHelper.GetLoggedInUserInfo(result.Access_Token);

                _events.PublishOnUIThread(new LogOnEvent()); 
                
            }
            catch (Exception ex)
            {

                ErrorMessege = ex.Message;
            }
        }
    }
}
