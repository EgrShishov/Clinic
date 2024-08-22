using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            AddHttpClients(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
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
