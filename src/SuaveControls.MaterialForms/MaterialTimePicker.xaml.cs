using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SuaveControls.MaterialForms
{
    public partial class MaterialTimePicker : ContentView
    {


        public static BindableProperty TimeProperty = BindableProperty.Create(nameof(Time), typeof(TimeSpan?), typeof(MaterialTimePicker), defaultBindingMode: BindingMode.TwoWay);
        public static BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialTimePicker), defaultBindingMode: BindingMode.TwoWay);
        public static BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(MaterialTimePicker), defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldVal, newval) =>
        {
            var matEntry = (MaterialTimePicker)bindable;
            matEntry.EntryField.Placeholder = (string)newval;
            matEntry.HiddenLabel.Text = (string)newval;
        });

        public static BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(MaterialTimePicker), defaultValue: false, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialTimePicker)bindable;
            matEntry.EntryField.IsPassword = (bool)newVal;
        });
        public static BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(MaterialTimePicker), defaultValue: Keyboard.Default, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialTimePicker)bindable;
            matEntry.EntryField.Keyboard = (Keyboard)newVal;
        });
        public static BindableProperty AccentColorProperty = BindableProperty.Create(nameof(AccentColor), typeof(Color), typeof(MaterialTimePicker), defaultValue: Color.Accent);
        public static BindableProperty InvalidColorProperty = BindableProperty.Create(nameof(InvalidColor), typeof(Color), typeof(MaterialEntry), Color.Red, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialTimePicker)bindable;
            matEntry.UpdateValidation();
        });
        public static BindableProperty DefaultColorProperty = BindableProperty.Create(nameof(DefaultColor), typeof(Color), typeof(MaterialEntry), Color.Gray, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialTimePicker)bindable;
            matEntry.UpdateValidation();
        });
        public static BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(MaterialEntry), true, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialTimePicker)bindable;
            matEntry.UpdateValidation();
        });

        public bool IsValid
        {
            get
            {
                return (bool)GetValue(IsValidProperty);
            }
            set
            {
                SetValue(IsValidProperty, value);
            }
        }
        public Color DefaultColor
        {
            get
            {
                return (Color)GetValue(DefaultColorProperty);
            }
            set
            {
                SetValue(DefaultColorProperty, value);
            }
        }
        public Color InvalidColor
        {
            get
            {
                return (Color)GetValue(InvalidColorProperty);
            }
            set
            {
                SetValue(InvalidColorProperty, value);
            }
        }

        public TimeSpan? Time
        {
            get
            {
                return (TimeSpan?)GetValue(TimeProperty);
            }
            set
            {
                SetValue(TimeProperty, value);
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
        public MaterialTimePicker()
        {
            InitializeComponent();
            EntryField.BindingContext = this;
            BottomBorder.BackgroundColor = DefaultColor;
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
                HiddenLabel.TextColor = DefaultColor;
                if (Time == null)
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

            Picker.PropertyChanged += Picker_PropertyChanged; ;
        }

        private void Picker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Picker.Time))
            {
                EntryField.Text = DateTime.Today.Add(Picker.Time).ToString("hh:mm tt");
                Time = Picker.Time;
            }
        }

        public TimePicker GetUnderlyingPicker()
        {
            return Picker;
        }



        /// <summary>
        /// Updates view based on validation state
        /// </summary>
        private void UpdateValidation()
        {
            if (IsValid)
            {

                BottomBorder.BackgroundColor = DefaultColor;
                HiddenBottomBorder.BackgroundColor = AccentColor;
                if (IsFocused)
                {
                    HiddenLabel.TextColor = AccentColor;
                }
                else
                {
                    HiddenLabel.TextColor = DefaultColor;
                }
            }
            else
            {
                BottomBorder.BackgroundColor = InvalidColor;
                HiddenBottomBorder.BackgroundColor = InvalidColor;
                HiddenLabel.TextColor = InvalidColor;
            }
        }

    }
}