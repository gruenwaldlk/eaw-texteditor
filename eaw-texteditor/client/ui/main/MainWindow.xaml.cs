using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using eaw_texteditor.client.ui.dialogs.add;
using eaw_texteditor.client.ui.dialogs.edit;
using eaw_texteditor.client.ui.dialogs.settings;
using eaw_texteditor.shared.common.util.ui;
using eaw_texteditor.shared.data.main;
using MahApps.Metro.Controls;
using MahApps.Metro.SimpleChildWindow;
using ts.translation;
using ts.translation.common.typedefs;
using ts.translation.common.util.observable;
using ts.translation.data.holder.observables;

namespace eaw_texteditor.client.ui.main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private MainWindowData FormData { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ImportFormData(new MainWindowData());
        }

        private void ImportFormData(MainWindowData data)
        {
            data.UseKeySearch = true;
            data.UseSimpleSearch = true;
            data.MatchCase = true;
            FormData = data;
            DataContext = data;
        }

        private void AdvancedSearchCheckBoxCheckedChanged(object sender, RoutedEventArgs e)
        {
            _advancedSearchFormGroupBox.IsEnabled = FormData.IsAdvancedSearchCheckBoxChecked;
        }

        private async void _settingsExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow childWindow = new SettingsWindow() {IsModal = true};
            await this.ShowChildWindowAsync<bool>(childWindow, ChildWindowManager.OverlayFillBehavior.FullWindow);
            FormData.SelectedLanguage = childWindow.FormData.SelectedLanguage;
        }

        private async void _basicEditorDataGrid_OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
            {
                e.Handled = true;
                return;
            }

            DependencyObject source = (DependencyObject)e.OriginalSource;
            DataGridRow row = UiUtility.TryFindParent<DataGridRow>(source);
            if (row == null) return;
            
            if (!(row.Item is ObservableTranslationData translationItem))
            {
                e.Handled = true;
                return;
            }
            await this.ShowChildWindowAsync<bool>(new EditTextKeyWindow(translationItem) { IsModal = true }, ChildWindowManager.OverlayFillBehavior.FullWindow);
            e.Handled = true;
        }

        private void _importExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            PGTEXTS.LoadFromFile("I:\\Workspace\\eaw-texteditor\\test\\TranslationManifest.xml");
            foreach (PGLanguage loadedLanguage in PGTEXTS.GetLoadedLanguages())
            {
                if (FormData.Sources.ContainsKey(loadedLanguage))
                {
                    FormData.Sources.Remove(loadedLanguage);
                }
                FormData.Sources.Add(loadedLanguage, new CollectionViewSource() {Source = ObservableTranslationUtility.GetTranslationDataAsObservable(loadedLanguage)});
            }
            FormData.SelectedLanguage = Properties.Settings.Default.USR_LOADED_LANGUAGE;
        }

        private void _exportExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            PGTEXTS.SaveToFile("I:\\Workspace\\eaw-texteditor\\test\\export");
            PGTEXTS.SaveToFile("I:\\Workspace\\eaw-texteditor\\test\\export", TSFileTypes.FileTypeDat);
        }

        private void OnRefreshClick(object sender, RoutedEventArgs e)
        {
            FormData.TryRefresh();
        }

        private void OnClearClick(object sender, RoutedEventArgs e)
        {
            FormData.SearchTerm = string.Empty;
        }

        private async void OnMenuNew(object sender, RoutedEventArgs e)
        {
            AddTextKeyWindow editWindow = new AddTextKeyWindow(new ObservableTranslationData(string.Empty, string.Empty)) {IsModal = true};
            if (!await this.ShowChildWindowAsync<bool>(editWindow, ChildWindowManager.OverlayFillBehavior.FullWindow)) return;
            PGTEXTS.SetText(editWindow.FormData.Key, editWindow.FormData.Value, FormData.SelectedLanguage);
            if (FormData.Sources[FormData.SelectedLanguage].Source is ObservableCollection<ObservableTranslationData> src)
            {
                src.Add(editWindow.FormData.Translation);
            }

        }
    }
}
