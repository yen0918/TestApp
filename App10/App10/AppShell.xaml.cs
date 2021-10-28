﻿using App10.ViewModels;
using App10.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace App10
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("AboutPage/Scaning", typeof(Scaning));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
