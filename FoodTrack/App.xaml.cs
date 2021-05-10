﻿using FoodTrack.Context.UnitOfWork;
using FoodTrack.Models;
using FoodTrack.Views;
using FoodTrack.Views.Windows;
using FoodTrack.XMLSerializer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FoodTrack
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //SplashScreen splash = new SplashScreen("../Resources/foodTrackSplash.png");
            //splash.Show(autoClose: true, topMost: true);
            //splash.Close(TimeSpan.FromSeconds(4));
            User deserializedeUser = XmlSerializeWrapper.Deserialize("../lastUser.xml");
            using (UnitOfWork unit = new UnitOfWork())
            {
                IEnumerable<User> resultUserFound = unit.UserRepository.Get(x => x.UserLogin == deserializedeUser.UserLogin);

                if (resultUserFound.Count() != 0)
                {
                    if (resultUserFound.First<User>().UserPassword.SequenceEqual<byte>(deserializedeUser.UserPassword))
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        return;
                    }
                }
                    LogInWindow logInWindow = new LogInWindow();
                    logInWindow.Show();
            }
           
        }
    }
}
