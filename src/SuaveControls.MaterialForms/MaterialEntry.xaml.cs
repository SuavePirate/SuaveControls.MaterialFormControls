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
        public static BindableProperty DefaultColorProperty = BindableProperty.Create(nameof(DefaultColor), typeof(Color), typeof(MaterialEntry), Color.Gray);
        public static BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(MaterialEntry), true, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntry)bindable;
            matEntry.UpdateValidation();
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

#endregion

        public MaterialEntry()
        {
            InitializeComponent();
            EntryField.BindingContext = this;
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
        }

        /// <summary>
        /// Calculates the layout when unfocused. Includes running the animation to update the bottom border color and the floating label
        /// </summary>
        /// <returns>The layout unfocused.</returns>
        private async Task CalculateLayoutUnfocused()
        {
            HiddenLabel.TextColor = DefaultColor;
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
            if(IsValid)
            {

                BottomBorder.Color = DefaultColor;
                HiddenBottomBorder.Color = AccentColor;
                if(IsFocused)
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
                BottomBorder.Color = InvalidColor;
                HiddenBottomBorder.Color = InvalidColor;
                HiddenLabel.TextColor = InvalidColor;
            }
        }
    }
}