using SuaveControls.MaterialForms.UWP.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuaveControls.MaterialForms.UWP
{
    public class RendererInitializer
    {
        public static void Init()
        {
            BorderlessDatePickerRenderer.Init();
            BorderlessTimePickerRenderer.Init();
            BorderlessPickerRenderer.Init();
            BorderlessEditorRenderer.Init();
            BorderlessEntryRenderer.Init();
        }
    }
}
