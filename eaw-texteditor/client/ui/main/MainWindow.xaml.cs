using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using eaw_texteditor.shared.common.util.ui;
using eaw_texteditor.shared.data.main;
using MahApps.Metro.Controls;
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
        private static bool IsInSetup = true;
        private MainWindowData _formData { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ImportFormData(new MainWindowData());
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            IsInSetup = false;
        }

        internal void ImportFormData(MainWindowData data)
        {
            _formData = data;
            DataContext = data;
        }

        private void _searchExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            Console.Write("Search executed!");
        }

        private void AdvancedSearchCheckBoxCheckedChanged(object sender, RoutedEventArgs e)
        {
            Console.WriteLine($"Stuff changed! {_formData.IsAdvancedSearchCheckBoxChecked}");
            _advancedSearchFormGroupBox.IsEnabled = _formData.IsAdvancedSearchCheckBoxChecked;
            _formData.ObservableTranslationDataHolder.Add(new ObservableTranslationData($"{Guid.NewGuid()}", "Value"));
            Random rnd = new Random();
            int index = rnd.Next(0, _formData.ObservableTranslationDataHolder.Count);
            _formData.ObservableTranslationDataHolder[index].Value += "EDIT!";
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("TEST");
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void _translationDataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("TEST");
        }

        private void _settingsExecuteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _basicEditorDataGrid_OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject source = (DependencyObject)e.OriginalSource;
            DataGridRow row = UiUtility.TryFindParent<DataGridRow>(source);
            if (row == null) return;

            Console.WriteLine("Click Event!");

            e.Handled = true;
        }

        private void _importExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            PGTEXTS.LoadFromFile("I:\\Workspace\\eaw-texteditor\\test\\TranslationManifest.xml");
            _formData.ObservableTranslationDataHolder = ObservableTranslationUtility.GetTranslationDataAsObservable();
            _basicEditorDataGrid.ItemsSource = _formData.ObservableTranslationDataHolder;
        }

        private void _exportExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            PGTEXTS.SaveToFile("I:\\Workspace\\eaw-texteditor\\test\\export");
            PGTEXTS.SaveToFile("I:\\Workspace\\eaw-texteditor\\test\\export", TSFileTypes.FileTypeDat);
        }
    }
}
