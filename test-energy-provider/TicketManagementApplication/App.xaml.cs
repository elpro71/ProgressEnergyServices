using Microsoft.Practices.ServiceLocation;
using System.Windows;
using TicketManagementApplication.Services;
using Splat;
using System;

namespace TicketManagementApplication
{
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            var locator = Splat.Locator.Current;
            var builder = Splat.Locator.CurrentMutable;
            builder.Register<ITicketService>(() => new TicketAdaptor());
            builder.Register<EmailsViewModel>(() =>
            {
               return new EmailsViewModel(locator.GetService<ITicketService>());
            });
            base.OnStartup(e);
        }
        
    }
}
