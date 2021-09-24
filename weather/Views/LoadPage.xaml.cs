using System;
using System.Collections.Generic;
using weather.ViewModels;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace weather.Views
{
    public partial class LoadPage : Popup
    {
        public LoadPage()
        {
            InitializeComponent();
        }


        public void DimissLoad()
        {
            Dismiss(null);
        }
    }
}
