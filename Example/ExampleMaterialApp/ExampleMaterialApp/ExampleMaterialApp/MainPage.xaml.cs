using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExampleMaterialApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            PickerDemo.Items = new List<string>
            {
                "Option 1",
                "Option 2",
                "Option 3",
                "Option 4"
            };
        }
    }
}
