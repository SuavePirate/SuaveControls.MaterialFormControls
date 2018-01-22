using System;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SuaveControls.MaterialForms
{
    public partial class MaterialPicker : ContentView
    {
        public event EventHandler SelectedIndexChanged;


        public Picker GetUnderlyingPicker() => Picker;

        public static BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialPicker), defaultBindingMode: BindingMode.TwoWay);
        public static BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(MaterialPicker), defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldVal, newval) =>
        {
            var matPicker = (MaterialPicker)bindable;
            matPicker.Picker.Title = (string)newval;
            matPicker.HiddenLabel.Text = (string)newval;
        });
        public static BindableProperty ItemsProperty = BindableProperty.Create(nameof(Items), typeof(IList), typeof(MaterialPicker), null);
        public static BindableProperty SelectedIndexProperty = BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(MaterialPicker), 0, BindingMode.TwoWay);
        public static BindableProperty AccentColorProperty = BindableProperty.Create(nameof(AccentColor), typeof(Color), typeof(MaterialPicker), defaultValue: Color.Accent);
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(MaterialPicker), null, BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var matPicker = (MaterialPicker)bindable;
            matPicker.HiddenLabel.IsVisible = !string.IsNullOrEmpty(newValue?.ToString());
        });
        public static BindableProperty SelectedIndexChangedCommandProperty = BindableProperty.Create(nameof(SelectedIndexChangedCommand), typeof(ICommand), typeof(MaterialPicker), null);
        public static BindableProperty InvalidColorProperty = BindableProperty.Create(nameof(InvalidColor), typeof(Color), typeof(MaterialEntry), Color.Red, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialPicker)bindable;
            matEntry.UpdateValidation();
        });
        public static BindableProperty DefaultColorProperty = BindableProperty.Create(nameof(DefaultColor), typeof(Color), typeof(MaterialEntry), Color.Gray, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialPicker)bindable;
            matEntry.UpdateValidation();
        });
        public static BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(MaterialEntry), true, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialPicker)bindable;
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
        public ICommand SelectedIndexChangedCommand
        {
            get { return (ICommand)GetValue(SelectedIndexChangedCommandProperty); }
            set { SetValue(SelectedIndexChangedCommandProperty, value); }
        }

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public int SelectedIndex
        {
            get
            {
                return (int)GetValue(SelectedIndexProperty);
            }
            set
            {
                SetValue(SelectedIndexProperty, value);
            }
        }

        public IList Items
        {
            get
            {
                return (IList)GetValue(ItemsProperty);
            }
            set
            {
                SetValue(ItemsProperty, value);
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

        public MaterialPicker()
        {
            InitializeComponent();
            Picker.BindingContext = this;
            BottomBorder.BackgroundColor = DefaultColor;
            // TODO: Possible memory leak?
            Picker.SelectedIndexChanged += (sender, e) =>
            {
                SelectedIndexChangedCommand?.Execute(Picker.SelectedItem);
                SelectedIndexChanged?.Invoke(sender, e);
            };

            Picker.Focused += async (s, a) =>
            {
                HiddenBottomBorder.BackgroundColor = AccentColor;
                HiddenLabel.TextColor = AccentColor;
                HiddenLabel.IsVisible = true;
                if (Picker.SelectedItem == null)
                {
                    // animate both at the same time
                    await Task.WhenAll(
                    HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, BottomBorder.Width, BottomBorder.Height), 200),
                    HiddenLabel.FadeTo(1, 60),
                    HiddenLabel.TranslateTo(HiddenLabel.TranslationX, Picker.Y - Picker.Height + 4, 200, Easing.BounceIn)
                 );
                    Picker.Title = null;
                }
                else
                {
                    await HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, BottomBorder.Width, BottomBorder.Height), 200);
                }
            };
            Picker.Unfocused += async (s, a) =>
            {
                HiddenLabel.TextColor = DefaultColor;
                if (Picker.SelectedItem == null)
                {
                    // animate both at the same time
                    await Task.WhenAll(
                    HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, 0, BottomBorder.Height), 200),
                    HiddenLabel.FadeTo(0, 180),
                    HiddenLabel.TranslateTo(HiddenLabel.TranslationX, Picker.Y, 200, Easing.BounceIn)
                 );
                    Picker.Title = Placeholder;
                }
                else
                {
                    await HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, 0, BottomBorder.Height), 200);
                }
            };

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