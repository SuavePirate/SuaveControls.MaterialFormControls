using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SuaveControls.MaterialForms
{
    public partial class MaterialEntry : ContentView
    {
        #region Events
        public event EventHandler<FocusEventArgs> EntryFocused;
        public event EventHandler<FocusEventArgs> EntryUnfocused;
        public event EventHandler<TextChangedEventArgs> TextChanged;
        #endregion

        #region Bindable Properties
        public static BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialEntry), defaultBindingMode: BindingMode.TwoWay);
        public static BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(MaterialEntry), defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldVal, newval) =>
        {
            var matEntry = (MaterialEntry)bindable;
            matEntry.EntryField.Placeholder = (string)newval;
            matEntry.HiddenLabel.Text = (string)newval;
        });

        public static BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(MaterialEntry), defaultValue: false, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntry)bindable;
            matEntry.EntryField.IsPassword = (bool)newVal;
        });
        public static BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(MaterialEntry), defaultValue: Keyboard.Default, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntry)bindable;
            matEntry.EntryField.Keyboard = (Keyboard)newVal;
        });
        public static BindableProperty AccentColorProperty = BindableProperty.Create(nameof(AccentColor), typeof(Color), typeof(MaterialEntry), defaultValue: Color.Accent);
        public static BindableProperty InvalidColorProperty = BindableProperty.Create(nameof(InvalidColor), typeof(Color), typeof(MaterialEntry), Color.Red, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntry)bindable;
            matEntry.UpdateValidation();
        });
        public static BindableProperty DefaultColorProperty = BindableProperty.Create(nameof(DefaultColor), typeof(Color), typeof(MaterialEntry), Color.Gray, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntry)bindable;
            matEntry.UpdateValidation();
        });
        public static BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColorProperty), typeof(Color), typeof(MaterialEntry), Color.Gray, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntry)bindable;
            matEntry.EntryField.PlaceholderColor = (Color)newVal;
        });
        public static BindableProperty TextBackgroundColorProperty = BindableProperty.Create(nameof(TextBackgroundColorProperty), typeof(Color), typeof(MaterialEntry), Color.Transparent, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntry)bindable;
            matEntry.EntryField.BackgroundColor = (Color)newVal;
        });
        public static BindableProperty TextColorProperty = BindableProperty.Create(nameof(DefaultColor), typeof(Color), typeof(MaterialEntry), Color.Black, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntry)bindable;
            matEntry.EntryField.TextColor = (Color)newVal;
        });
        public static BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(MaterialEntry), true, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntry)bindable;
            matEntry.UpdateValidation();
        });
        public static BindableProperty HiddenLabelTextSizeProperty = BindableProperty.Create(nameof(HiddenLabelTextSizeProperty), typeof(double), typeof(MaterialEntry), 10.0, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntry)bindable;
            matEntry.HiddenLabel.FontSize = (double)newVal;
        });
        public static BindableProperty CompletedProperty = BindableProperty.Create(nameof(Completed), typeof(EventHandler), typeof(MaterialEntry), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var matEntry = (MaterialEntry)bindable;
            matEntry.Completed = (EventHandler)newValue;
        });
        #endregion

        #region Public Properties

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

        public Color TextBackgroundColor
        {
            get
            {
                return (Color)GetValue(TextBackgroundColorProperty);
            }
            set
            {
                SetValue(TextBackgroundColorProperty, value);
            }
        }

        public Color TextColor
        {
            get
            {
                return (Color)GetValue(TextColorProperty);
            }
            set
            {
                SetValue(TextColorProperty, value);
            }
        }

        [TypeConverter(typeof(FontSizeConverter))]
        public double HiddenLabelTextSize
        {
            get
            {
                return (double)GetValue(HiddenLabelTextSizeProperty);
            }
            set
            {
                SetValue(HiddenLabelTextSizeProperty, value);
            }
        }

        public Color PlaceholderColor
        {
            get
            {
                return (Color)GetValue(PlaceholderColorProperty);
            }
            set
            {
                SetValue(PlaceholderColorProperty, value);
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

        public EventHandler Completed
        {
            get => null;
            set => EntryField.Completed += value;
        }

        #endregion

        public MaterialEntry()
        {
            InitializeComponent();
            EntryField.BindingContext = this;
            BottomBorder.BackgroundColor = DefaultColor;
            EntryField.TextChanged += (s, a) =>
            {
                TextChanged?.Invoke(s, a);
            };

            EntryField.Focused += async (s, a) =>
            {
                EntryFocused?.Invoke(this, a);
                await CalculateLayoutFocused();

            };
            EntryField.Unfocused += async (s, a) =>
            {
                EntryUnfocused?.Invoke(this, a);
                await CalculateLayoutUnfocused();
            };
            EntryField.PropertyChanged += async (sender, args) =>
            {
                if (args.PropertyName == nameof(EntryField.Text) && !EntryField.IsFocused && !String.IsNullOrEmpty(EntryField.Text))
                {
                    await CalculateLayoutUnfocused();
                }
            };

            UpdateValidation();
        }

        /// <summary>
        /// Calculates the layout when unfocused. Includes running the animation to update the bottom border color and the floating label
        /// </summary>
        /// <returns>The layout unfocused.</returns>
        private async Task CalculateLayoutUnfocused()
        {
            if (IsValid)
            {
                HiddenLabel.TextColor = DefaultColor;
                BottomBorder.BackgroundColor = DefaultColor;
            }
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
                HiddenLabel.IsVisible = true;
                await HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, 0, BottomBorder.Height), 200);
            }
        }

        /// <summary>
        /// Calculates the layout when focused. Includes running the animation to update the bottom border color and the floating label
        /// </summary>
        private async Task CalculateLayoutFocused()
        {
            HiddenLabel.IsVisible = true;
            HiddenLabel.TextColor = AccentColor;
            HiddenBottomBorder.BackgroundColor = AccentColor;
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