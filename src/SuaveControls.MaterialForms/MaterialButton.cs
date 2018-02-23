using System;
using Xamarin.Forms;

namespace SuaveControls.MaterialForms
{
    /// <summary>
    /// Material button. Allows for the update of elevation of the control.
    /// </summary>
    public class MaterialButton : Button
    {
        public static BindableProperty ElevationProperty = BindableProperty.Create(nameof(Elevation), typeof(float), typeof(MaterialButton), 4.0f);

        public float Elevation
        {
            get
            {
                return (float)GetValue(ElevationProperty);
            }
            set
            {
                SetValue(ElevationProperty, value);
            }
        }
    }
}
