using System.Collections.Generic;
using eaw_texteditor.shared.data.dialogs.settings;
using MahApps.Metro.SimpleChildWindow;
using ts.translation;
using ts.translation.common.typedefs;

namespace eaw_texteditor.client.ui.dialogs.settings
{
    public partial class SettingsWindow : ChildWindow
    {
        internal SettingsWindowData FormData { get; set; }
        public SettingsWindow()
        {
            InitializeComponent();
            FormData = new SettingsWindowData(PGTEXTS.GetLoadedLanguages() as List<PGLanguage>);
            DataContext = FormData;
        }
    }
}
