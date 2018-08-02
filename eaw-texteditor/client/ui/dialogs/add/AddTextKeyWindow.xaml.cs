using System.Windows;
using eaw_texteditor.shared.data.dialogs.edit;
using MahApps.Metro.SimpleChildWindow;
using ts.translation.data.holder.observables;

namespace eaw_texteditor.client.ui.dialogs.add
{
    /// <summary>
    /// Interaction logic for AddTextKeyWindow.xaml
    /// </summary>
    public partial class AddTextKeyWindow : ChildWindow
    {
        internal EditTextKeyWindowData FormData { get; set; }
        public AddTextKeyWindow(ObservableTranslationData translation)
        {
            InitializeComponent();
            FormData = new EditTextKeyWindowData() { Translation = translation };
            DataContext = FormData;
            FormData.IsKeyEditable = true;
            FormData.IsValidKey = false;
            FormData.IsBoltVisible = Visibility.Visible;
        }

        private void OnAddClick(object sender, RoutedEventArgs e)
        {
            this.Close(true);
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            this.Close(false);
        }
    }
}
