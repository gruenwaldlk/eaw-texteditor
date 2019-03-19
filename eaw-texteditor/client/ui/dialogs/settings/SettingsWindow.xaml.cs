using System;
using System.Collections.Generic;
using System.Windows;
using eaw_texteditor.client.ui.main;
using eaw_texteditor.shared.data.dialogs.settings;
using MahApps.Metro.Controls.Dialogs;
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

        private async void _performProjectFixup_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                PGTEXTS.PerformTranslationFixup(FormData.SelectedLanguage);
            }
            catch (Exception ex)
            {
                await ((MainWindow)this.Parent).ShowMessageAsync("Warning!", $"Something went wrong.\n{ex}");
            }
        }
    }
}
