using AdminApp.Common.Abstractions;
using AdminApp.Stores;
using AdminApp.View;
using AdminApp.ViewModel;
using AdminApp.ViewModel.Appointment;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider _serviceProvider { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            NavigationStore navigationStore = new NavigationStore();

            navigationStore.CurrentViewModel = new SignInViewModel(navigationStore);

            MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainViewModel>()
                    .AddSingleton<SignInViewModel>()
                    .AddSingleton<HomeViewModel>();
        }

        private IServiceCollection AddHttpClients(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("offices", client =>
            {
                client.BaseAddress = new Uri(configuration.GetSection("gateway").Value);
            });
            return services;
        }
    }

}
