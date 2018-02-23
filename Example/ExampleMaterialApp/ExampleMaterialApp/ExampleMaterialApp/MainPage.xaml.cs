using System;
using Xamarin.Forms;

namespace ExampleMaterialApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void MyButton_Clicked(object sender, EventArgs e)
        {
            MyButton.Elevation++;
        }
    }
}