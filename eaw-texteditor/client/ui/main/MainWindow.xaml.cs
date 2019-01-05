using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using eaw_texteditor.client.ui.dialogs.add;
using eaw_texteditor.client.ui.dialogs.edit;
using eaw_texteditor.client.ui.dialogs.export;
using eaw_texteditor.client.ui.dialogs.load;
using eaw_texteditor.client.ui.dialogs.settings;
using eaw_texteditor.shared.common.util.ui;
using eaw_texteditor.shared.data.main;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
        private DependencyObject CurrentRightClickedObject { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ImportFormData(new MainWindowData());
        }

        private void ImportFormData(MainWindowData data)
        {
            data.IsKeySearchChecked = true;
            data.UseSimpleSearch = true;
            data.IsMatchCaseChecked = true;
            data.IsTranslationDataLoaded = false;
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
            EditTextKeyWindow w = new EditTextKeyWindow(translationItem) { IsModal = true };
            await this.ShowChildWindowAsync<bool>(w, ChildWindowManager.OverlayFillBehavior.FullWindow);
            if (w.FormData.TranslationChanged)
            {
                FormData.IsEdited = true;
            }
            e.Handled = true;
        }

        private async void _importExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            LoadFromFileWindow childWindow = new LoadFromFileWindow() { IsModal = true };
            await this.ShowChildWindowAsync<bool>(childWindow, ChildWindowManager.OverlayFillBehavior.FullWindow);
            if (!childWindow.FormData.ResultOk)
            {
                return;
            }

            IsEnabled = false;
            _mainBoxTabControl.Visibility = Visibility.Collapsed;
            _mainBoxLoadingControl.Visibility = Visibility.Visible;
            try
            {
                PGTEXTS.LoadFromFile(childWindow.FormData.ImportPath, childWindow.FormData.ImportType);
                foreach (PGLanguage loadedLanguage in PGTEXTS.GetLoadedLanguages())
                {
                    if (FormData.Sources.ContainsKey(loadedLanguage))
                    {
                        FormData.Sources.Remove(loadedLanguage);
                    }
                    FormData.Sources.Add(loadedLanguage, new CollectionViewSource() {Source = ObservableTranslationUtility.GetTranslationDataAsObservable(loadedLanguage)});
                }
                FormData.SelectedLanguage = Properties.Settings.Default.USR_LOADED_LANGUAGE;
                FormData.IsTranslationDataLoaded = true;
            }
            catch (Exception ex)
            {
                if (!IsEnabled)
                {
                    IsEnabled = true;
                    _mainBoxTabControl.Visibility = Visibility.Visible;
                    _mainBoxLoadingControl.Visibility = Visibility.Collapsed;
                }
                await this.ShowMessageAsync("Warning!", $"Something went wrong.\n{ex}");
            }

            if (IsEnabled) return;
            IsEnabled = true;
            _mainBoxTabControl.Visibility = Visibility.Visible;
            _mainBoxLoadingControl.Visibility = Visibility.Collapsed;
        }

        private async void _exportExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            ExportToFileWindow childWindow = new ExportToFileWindow() { IsModal = true};
            await this.ShowChildWindowAsync<bool>(childWindow, ChildWindowManager.OverlayFillBehavior.FullWindow);
            if (!childWindow.FormData.ResultOk)
            {
                return;
            }

            if (Directory.EnumerateFileSystemEntries(childWindow.FormData.ExportPath).Any())
            {
                MessageDialogResult dialogResult = await this.ShowMessageAsync("Warning!", $"The export folder is not empty.\nThis may overwrite {(childWindow.FormData.ExportType == TSFileTypes.FileTypeXml ? "an existing \'TranslationManifest.xml\' file." : "any existing \'MASTERTEXTFILE_{LANGUAGE}.DAT\' files.")}\nCurrently selected directory: {childWindow.FormData.ExportPath}\n\nContinue exporting?", MessageDialogStyle.AffirmativeAndNegative);
                if (dialogResult != MessageDialogResult.Affirmative)
                {
                    return;
                }
            }

            IsEnabled = false;
            _mainBoxTabControl.Visibility = Visibility.Collapsed;
            _mainBoxLoadingControl.Visibility = Visibility.Visible;
            try
            {
                PGTEXTS.SaveToFile(childWindow.FormData.ExportPath, childWindow.FormData.ExportType);
            }
            catch (Exception ex)
            {
                if (!IsEnabled)
                {
                    IsEnabled = true;
                    _mainBoxTabControl.Visibility = Visibility.Visible;
                    _mainBoxLoadingControl.Visibility = Visibility.Collapsed;
                }
                await this.ShowMessageAsync("Warning!", $"Something went wrong.\n{ex}");
            }

            FormData.IsEdited = false;
            if (IsEnabled) return;
            IsEnabled = true;
            _mainBoxTabControl.Visibility = Visibility.Visible;
            _mainBoxLoadingControl.Visibility = Visibility.Collapsed;
        }

        private void OnRefreshClick(object sender, RoutedEventArgs e)
        {
            FormData.TryRefresh();
        }

        private void OnClearClick(object sender, RoutedEventArgs e)
        {
            FormData.SearchTerm = string.Empty;
        }

        private async void MenuItemNew_OnClick(object sender, RoutedEventArgs e)
        {
            AddTextKeyWindow addWindow = new AddTextKeyWindow(new ObservableTranslationData(string.Empty, string.Empty)) {IsModal = true};
            if (!await this.ShowChildWindowAsync<bool>(addWindow, ChildWindowManager.OverlayFillBehavior.FullWindow)) return;
            PGTEXTS.SetText(addWindow.FormData.Key, addWindow.FormData.Value, FormData.SelectedLanguage);
            if (FormData.Sources[FormData.SelectedLanguage].Source is ObservableCollection<ObservableTranslationData> src)
            {
                FormData.IsEdited = true;
                src.Add(addWindow.FormData.Translation);
            }

        }

        private void OnSearchClick(object sender, RoutedEventArgs e)
        {
            FormData.TryRefresh();
        }

        private async void MenuItemEdit_OnClick(object sender, RoutedEventArgs e)
        {
            DataGridRow row = UiUtility.TryFindParent<DataGridRow>(CurrentRightClickedObject);
            if (row == null)
            {
                CurrentRightClickedObject = null;
                return;
            }

            if (!(row.Item is ObservableTranslationData translationItem))
            {
                CurrentRightClickedObject = null;
                e.Handled = true;
                return;
            }
            EditTextKeyWindow w = new EditTextKeyWindow(translationItem) { IsModal = true };
            await this.ShowChildWindowAsync<bool>(w, ChildWindowManager.OverlayFillBehavior.FullWindow);
            if (w.FormData.TranslationChanged)
            {
                FormData.IsEdited = true;
            }
            CurrentRightClickedObject = null;
            e.Handled = true;
        }

        private void _basicEditorDataGrid_OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            CurrentRightClickedObject = (DependencyObject) e.OriginalSource;
        }

        private async void MainWindow_OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool shutdown = true;
            if (FormData.IsEdited)
            {
                e.Cancel = true;
                MetroDialogSettings mySettings = new MetroDialogSettings() {AffirmativeButtonText = "Quit", NegativeButtonText = "Cancel", AnimateShow = true, AnimateHide = false};

                MessageDialogResult result = await this.ShowMessageAsync("Quit application?", "The current data contains unsaved changes!\n" + "Closing the application will result in the loss of all changes.\n" + "If you want to keep your changes, export the data before continuing.\n\n" + "Continue without exporting?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

                shutdown = result == MessageDialogResult.Affirmative;
            }

            if (shutdown)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
