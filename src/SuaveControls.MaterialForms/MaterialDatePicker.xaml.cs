using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SuaveControls.MaterialForms
{
    public partial class MaterialDatePicker : ContentView
    {

        public static BindableProperty CustomDateFormatProperty = BindableProperty.Create(nameof(CustomDateFormat), typeof(string), typeof(MaterialDatePicker), defaultBindingMode: BindingMode.TwoWay);
        public string CustomDateFormat
        {
            get
            {
                return (string)GetValue(CustomDateFormatProperty);
            }
            set
            {
                SetValue(CustomDateFormatProperty, value);
            }
        }
        private static string _defaultDateFormat = "dddd, MMMM d, yyyy";
        public static BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateTime?), typeof(MaterialDatePicker), defaultBindingMode: BindingMode.TwoWay);
        public static BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialDatePicker), defaultBindingMode: BindingMode.TwoWay);
        public static BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(MaterialDatePicker), defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldVal, newval) =>
        {
            var matEntry = (MaterialDatePicker)bindable;
            matEntry.EntryField.Placeholder = (string)newval;
            matEntry.HiddenLabel.Text = (string)newval;
        });

        public static BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(MaterialDatePicker), defaultValue: false, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialDatePicker)bindable;
            matEntry.EntryField.IsPassword = (bool)newVal;
        });
        public static BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(MaterialDatePicker), defaultValue: Keyboard.Default, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialDatePicker)bindable;
            matEntry.EntryField.Keyboard = (Keyboard)newVal;
        });
        public static BindableProperty AccentColorProperty = BindableProperty.Create(nameof(AccentColor), typeof(Color), typeof(MaterialDatePicker), defaultValue: Color.Accent);
        public DateTime? Date
        {
            get
            {
                return (DateTime?)GetValue(DateProperty);
            }
            set
            {
                SetValue(DateProperty, value);
            }
        }
        public Color AccentColor
        {
            get
            {
                return (Color)GetValue(AccentColorProperty);
            }
            set
            {
                SetValue(AccentColorProperty, value);
            }
        }
        public Keyboard Keyboard
        {
            get
            {
                return (Keyboard)GetValue(KeyboardProperty);
            }
            set
            {
                SetValue(KeyboardProperty, value);
            }
        }

        public bool IsPassword
        {
            get
            {
                return (bool)GetValue(IsPasswordProperty);
            }
            set
            {
                SetValue(IsPasswordProperty, value);
            }
        }

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }
        public string Placeholder
        {
            get
            {
                return (string)GetValue(PlaceholderProperty);
            }
            set
            {
                SetValue(PlaceholderProperty, value);
            }
        }
        public MaterialDatePicker()
        {
            InitializeComponent();
            EntryField.BindingContext = this;
            EntryField.Focused += (s, a) =>
            {
				Device.BeginInvokeOnMainThread(() => {
					EntryField.Unfocus();
					Picker.Focus();
				});
            };
            Picker.Focused += async (s, a) =>
            {
                HiddenBottomBorder.BackgroundColor = AccentColor;
                HiddenLabel.TextColor = AccentColor;
                HiddenLabel.IsVisible = true;
		if (string.IsNullOrEmpty(CustomDateFormat))
                    CustomDateFormat = _defaultDateFormat;
                EntryField.Text = Picker.Date.ToString(CustomDateFormat, CultureInfo.CurrentCulture);
                if (string.IsNullOrEmpty(EntryField.Text))
                {
                    // animate both at the same time
                    await Task.WhenAll(
                    HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, BottomBorder.Width, BottomBorder.Height), 200),
                    HiddenLabel.FadeTo(1, 60),
                    HiddenLabel.TranslateTo(HiddenLabel.TranslationX, EntryField.Y - EntryField.Height + 4, 200, Easing.BounceIn)
                 );
                    EntryField.Placeholder = null;
                }
                else
                {
                    await HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, BottomBorder.Width, BottomBorder.Height), 200);
                }
            };
            Picker.Unfocused += async (s, a) =>
            {
                HiddenLabel.TextColor = Color.Gray;
                if (string.IsNullOrEmpty(EntryField.Text))
                {
                    // animate both at the same time
                    await Task.WhenAll(
                    HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, 0, BottomBorder.Height), 200),
                    HiddenLabel.FadeTo(0, 180),
                    HiddenLabel.TranslateTo(HiddenLabel.TranslationX, EntryField.Y, 200, Easing.BounceIn)
                 );
                    EntryField.Placeholder = Placeholder;
                }
                else
                {
                    await HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, 0, BottomBorder.Height), 200);
                }
            };

            Picker.DateSelected += Picker_DateSelected;
        }

        private void Picker_DateSelected(object sender, DateChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(CustomDateFormat))
                CustomDateFormat = _defaultDateFormat;
            EntryField.Text = e.NewDate.ToString(CustomDateFormat, CultureInfo.CurrentCulture);
            Date = e.NewDate;
        }
    }
}
