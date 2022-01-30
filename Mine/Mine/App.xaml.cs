using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mine.Services;
using Mine.Views;

namespace Mine
{
    public partial class App : Application
    {
        /// <summary>
        /// ctor of the app class
        /// </summary>
        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            //registering the database service so we can do a db call
            DependencyService.Register<DatabaseService>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
