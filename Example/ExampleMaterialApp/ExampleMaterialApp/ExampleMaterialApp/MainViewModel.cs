using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExampleMaterialApp
{
    public class MainViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<Fruit> PickerData { get; set; }
        public Fruit PickerSelectedItem { get; set; }
        public ICommand PickerSelectedIndexChangedCmd { get; }

        public MainViewModel()
        {
            PickerData = new ObservableCollection<Fruit>
            {
                new Fruit { Id = 1, Name ="Apple" },
                new Fruit { Id = 2, Name = "Banana" },
                new Fruit { Id = 3, Name = "Orange" }
            };

            PickerSelectedItem = PickerData[PickerData.Count - 1];

            PickerSelectedIndexChangedCmd = new Command<Fruit>((selectedFruit) => Debug.WriteLine($"PickerSelectedIndexChangedCmd => {selectedFruit}"));
        }

        // Fody will take care of that
        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class Fruit
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}
