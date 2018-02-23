using System;
using System.ComponentModel;
using Android.Graphics;
using Android.Support.V4.View;
using SuaveControls.MaterialForms;
using SuaveControls.MaterialForms.Android.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Animation = Android.Animation;

[assembly: ExportRenderer(typeof(MaterialButton), typeof(MaterialButtonRenderer))]
namespace SuaveControls.MaterialForms.Android.Renderers
{
    public class MaterialButtonRenderer : Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer
    {
        public static void Init() { }

        /// <summary>
        /// Set up the elevation from load
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement == null)
                return;

            // we need to reset the StateListAnimator to override the setting of Elevation on touch down and release.
            Control.StateListAnimator = new Animation.StateListAnimator();

            // set the elevation manually
            UpdateMaterialShadow();

        }

        /// <summary>
        /// Update the elevation when updated from Xamarin.Forms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "Elevation")
            {
                UpdateMaterialShadow();
            }
        }

        private void UpdateMaterialShadow()
        {
            var materialButton = (MaterialButton)Element;
            ViewCompat.SetElevation(this, materialButton.Elevation);
            ViewCompat.SetElevation(Control, materialButton.Elevation);
            UpdateLayout();
        }
    }
}
