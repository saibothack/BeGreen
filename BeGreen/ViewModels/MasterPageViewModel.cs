﻿using System;
using System.Windows.Input;
using BeGreen.Helpers;
using BeGreen.Views;
using Xamarin.Forms;

namespace BeGreen.ViewModels
{
    public class MasterPageViewModel
    {
        public string NombreUsuario { get; set; }
        public bool IsUsuarioVisible { get; set; }
        public ImageSource imageProfiler { get; set; }
        public ImageSource imageBackground { get; set; }
        public Command CommandRegister { get; set; }

        public MasterPageViewModel()
        {
            imageBackground = ImageSource.FromResource("BeGreen.Images.background_menu.png");
            imageProfiler = ImageSource.FromResource("BeGreen.Images.profile.png");
            CommandRegister = new Command(goToLogin);
        }

        public void goToLogin() {
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }

        public ICommand NavigationCommand
        {
            get
            {
                return new Command((value) =>
                {
                    // COMMENT: This is just quick demo code. Please don't put this in a production app.
                    var mdp = (Application.Current.MainPage as MasterDetailPage);
                    var navPage = mdp.Detail as NavigationPage;

                    // Hide the Master page
                    mdp.IsPresented = false;

                    switch (value)
                    {
                        case "1":
                            Application.Current.MainPage = new MasterDetailPage()
                            {
                                Master = new MasterPage() { Title = "Menú" },
                                Detail = new NavigationPage(new HomePage())
                            };
                            break;

                        case "2":
                            //navPage.PushAsync(new QRPage());
                            break;

                        case "3":
                            //navPage.PushAsync(new QRReadPage());
                            break;

                        case "4":
                            Application.Current.MainPage = new NavigationPage(new TutorialPage());
                            break;

                        case "5":
                            //navPage.PushAsync(new TrackingPage());
                            break;

                        case "6":
                            //navPage.PushAsync(new FAQPage());
                            break;

                        case "7":
                            //navPage.PushAsync(new ListRemissionPage());
                            break;

                        case "100":

                            //App.EndSesion();
                            //Application.Current.MainPage = new NavigationPage(new LoginPage());

                            break;
                    }

                });
            }
        }
    }
}
