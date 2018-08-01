using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using eaw_texteditor.client.ui.dialogs.edit;
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

            Console.WriteLine("Click Event!");
            if (!(row.Item is ObservableTranslationData translationItem))
            {
                e.Handled = true;
                return;
            }
            Console.WriteLine($"{translationItem.Key} = {translationItem.Value}");
            var result = await this.ShowChildWindowAsync<bool>(new EditTextKeyWindow(translationItem) { IsModal = true }, ChildWindowManager.OverlayFillBehavior.FullWindow);

            e.Handled = true;
        }

        private void _importExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            PGTEXTS.LoadFromFile("I:\\Workspace\\eaw-texteditor\\test\\TranslationManifest.xml");
            CollectionViewSource collectionViewSource = new CollectionViewSource {Source = ObservableTranslationUtility.GetTranslationDataAsObservable()};
            FormData.ListItemCollection = collectionViewSource.View;
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
    }
}
