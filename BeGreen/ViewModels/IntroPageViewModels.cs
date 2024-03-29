﻿using System;
using System.Threading;
using System.Threading.Tasks;
using BeGreen.Models.Settings;
using BeGreen.Utilities;
using BeGreen.Views;
using Xamarin.Forms;

namespace BeGreen.ViewModels
{
    public class IntroPageViewModels : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }
        public ImageSource imgLogo { get; set; }
        public AsyncCommand CommandInitialize { get; internal set; }

        public IntroPageViewModels()
        {
            imgLogo = ImageSource.FromResource("BeGreen.Images.ic_launcher_foreground_begreen.png");
            goToTutorial();

            CommandInitialize = new AsyncCommand(InitializeAsync, CanExecuteSubmit);

        }

        private async Task InitializeAsync() {
            try
            {
                IsBusy = true;

                SettingsData data = await App.oServiceManager.getSettings();
                await App.DataBase.SaveSettings(data.data);

                IsBusy = false;

            }
            finally
            {
                IsBusy = false;
            }

        }

        private bool CanExecuteSubmit()
        {
            return !IsBusy;
        }

        public async void goToTutorial() {
            await Task.Delay(5000);
            Application.Current.MainPage = new NavigationPage(new TutorialPage());
        }

    }
}
