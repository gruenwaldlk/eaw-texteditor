using System.Windows;
using System.Windows.Forms;
using eaw_texteditor.shared.data.dialogs.load;
using MahApps.Metro.SimpleChildWindow;
using ts.translation.common.typedefs;
using MessageBox = System.Windows.Forms.MessageBox;

namespace eaw_texteditor.client.ui.dialogs.load
{
    /// <summary>
    /// Interaction logic for LoadFromFileWindow.xaml
    /// </summary>
    public partial class LoadFromFileWindow : ChildWindow
    {
        internal LoadFromFileWindowData FormData { get; set; }

        public LoadFromFileWindow()
        {
            InitializeComponent();
            ImportFormData(new LoadFromFileWindowData());
        }

        private void ImportFormData(LoadFromFileWindowData data)
        {
            FormData = data;
            DataContext = data;
        }

        private void _loadFromXmlButton_OnClick(object sender, RoutedEventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "xml files (*.xml)|*.xml";
                openFileDialog.CheckPathExists = true;
                openFileDialog.CheckFileExists = true;
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FormData.ImportPath = openFileDialog.FileName;
                    FormData.ImportType = TSFileTypes.FileTypeXml;
                    FormData.ResultOk = true;
                    this.Close();
                }
            }
        }

        private void _loadFromDatButton_OnClick(object sender, RoutedEventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "dat files (*.dat)|mastertextfile_*.dat";
                openFileDialog.CheckPathExists = true;
                openFileDialog.CheckFileExists = true;
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FormData.ImportPath = openFileDialog.FileName;
                    FormData.ImportType = TSFileTypes.FileTypeDat;
                    FormData.ResultOk = true;
                    this.Close();
                }
            }
        }
    }
}
