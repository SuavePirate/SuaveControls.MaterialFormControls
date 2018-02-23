using System;
using System.ComponentModel;
using CoreGraphics;
using SuaveControls.MaterialForms;
using SuaveControls.MaterialForms.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MaterialButton), typeof(MaterialButtonRenderer))]
namespace SuaveControls.MaterialForms.iOS.Renderers
{
    public class MaterialButtonRenderer : ButtonRenderer
    {
        public static void Initialize()
        {
            // empty, but used for beating the linker
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            CreateMaterialShadow();
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            CreateMaterialShadow();
        }

        public override void LayoutSubviews()
        {
            base.LayoutIfNeeded();
            Layer.ShadowPath = UIBezierPath.FromRect(Layer.Bounds).CGPath;
        }


        /// <summary>
        /// Creates the material shadow.
        /// </summary>
        private void CreateMaterialShadow()
        {

            var materialButton = (MaterialButton)Element;

            // Update shadow to match better material design standards of elevation
            Layer.ShadowRadius = materialButton.Elevation;
            Layer.ShadowColor = UIColor.Gray.CGColor;
            Layer.ShadowOffset = new CGSize(2, 2);
            Layer.ShadowOpacity = 0.80f;
            Layer.ShadowPath = UIBezierPath.FromRect(Layer.Bounds).CGPath;
            Layer.MasksToBounds = false;

        }
    }
}
