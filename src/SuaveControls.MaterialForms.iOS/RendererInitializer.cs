using SuaveControls.MaterialForms.iOS.Renderers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuaveControls.MaterialForms.iOS
{
    public class RendererInitializer
    {
        public static void Init()
        {
            BorderlessDatePickerRenderer.Init();
            BorderlessEntryRenderer.Init();
            BorderlessTimePickerRenderer.Init();
            BorderlessPickerRenderer.Init();
            BorderlessEditorRenderer.Init();
            MaterialButtonRenderer.Initialize();
        }
    }
}
