using AutoMapper;
using Caliburn.Micro;
using Microsoft.Extensions.Configuration;
using RMDesktopUI.Helper;
using RMDesktopUI.Library;
using RMDesktopUI.Library.Api;
using RMDesktopUI.Library.Models;
using RMDesktopUI.Models;
using RMDesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RMDesktopUI
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();

            ConventionManager.AddElementConvention<PasswordBox>(
            PasswordBoxHelper.BoundPasswordProperty,
            "Password",
            "PasswordChanged");
        }

        private IMapper ConfigureAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductModel, ProductDisplayModel>();
                cfg.CreateMap<CartItemModel, CartItemDisplayModel>();
            });

            var output = config.CreateMapper();

            return output;
        }

        private IConfiguration AddConfiguration()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

#if DEBUG
            configurationBuilder.AddJsonFile("appsetting.Development.json", optional: true, reloadOnChange: true);
#else
            configurationBuilder.AddJsonFile("appsetting.Development.json", optional: true, reloadOnChange: true);
#endif
            return configurationBuilder.Build();
        }

        protected override void Configure()
        {


            _container.Instance(ConfigureAutomapper());

            _container.Instance(_container)
                .PerRequest<IProductEndpoint, ProductEndpoint>()
                .PerRequest<ISaleEndPoint, SaleEndPoint>()
                .PerRequest<IUserEndPoint, UserEndPoint>();


            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<ILoggInUserModel, LoggInUserModel>()
                .Singleton<IAPIHelper, APIHelper>();

            _container.RegisterInstance(typeof(IConfiguration), "IConfiguration", AddConfiguration());

            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));



        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
