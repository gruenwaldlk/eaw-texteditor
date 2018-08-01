using eaw_texteditor.shared.data.dialogs.edit;
using MahApps.Metro.SimpleChildWindow;
using ts.translation.data.holder.observables;

namespace eaw_texteditor.client.ui.dialogs.edit
{
    /// <summary>
    /// Interaction logic for EditTextKeyWindow.xaml
    /// </summary>
    public partial class EditTextKeyWindow : ChildWindow
    {
        
        public EditTextKeyWindow(ObservableTranslationData translation)
        {
            InitializeComponent();
            DataContext = new EditTextKeyWindowData() {Translation = translation};
        }
    }
}
