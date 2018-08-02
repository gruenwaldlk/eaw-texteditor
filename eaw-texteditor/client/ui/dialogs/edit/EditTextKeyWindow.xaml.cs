using eaw_texteditor.Properties;
using eaw_texteditor.shared.data.dialogs.edit;
using MahApps.Metro.SimpleChildWindow;
using ts.translation;
using ts.translation.data.holder.observables;

namespace eaw_texteditor.client.ui.dialogs.edit
{
    /// <summary>
    /// Interaction logic for EditTextKeyWindow.xaml
    /// </summary>
    public partial class EditTextKeyWindow : ChildWindow
    {
        internal EditTextKeyWindowData FormData { get; set; }
        public EditTextKeyWindow(ObservableTranslationData translation)
        {
            InitializeComponent();
            FormData = new EditTextKeyWindowData() {Translation = translation};
            if (FormData.Key == string.Empty && FormData.Value == string.Empty)
            {
                FormData.IsKeyEditable = true;
                FormData.IsValidKey = false;
            }
            DataContext = FormData;
        }

        private void OnClosingFinished(object sender, System.Windows.RoutedEventArgs e)
        {
            PGTEXTS.SetText(FormData.Key, FormData.Value, Settings.Default.USR_LOADED_LANGUAGE);
        }
    }
}
