using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SuaveControls.MaterialForms.Android.Renderers;

namespace SuaveControls.MaterialForms.Android
{
    public static class RendererInitializer
    {
        public static void Init()
        {
            BorderlessEntryRenderer.Init();
            BorderlessEditorRenderer.Init();
            BorderlessPickerRenderer.Init();
            BorderlessTimePickerRenderer.Init();
            BorderlessDatePickerRenderer.Init();
            MaterialButtonRenderer.Init();
        }
    }
}