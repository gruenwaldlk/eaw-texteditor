using eaw_texteditor.shared.data.dialogs.export;
using MahApps.Metro.SimpleChildWindow;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;
using ts.translation.common.typedefs;

namespace eaw_texteditor.client.ui.dialogs.export
{
    /// <summary>
    /// Interaction logic for ExportToFileWindow.xaml
    /// </summary>
    public partial class ExportToFileWindow : ChildWindow
    {
        internal ExportToFileWindowData FormData { get; set; }

        public ExportToFileWindow()
        {
            InitializeComponent();
            ImportFormData(new ExportToFileWindowData());
        }

        private void ImportFormData(ExportToFileWindowData data)
        {
            FormData = data;
            DataContext = data;
        }

        private void _exportToXmlButton_OnClick(object sender, RoutedEventArgs e)
        {
            using (CommonOpenFileDialog saveFileDialog = new CommonOpenFileDialog())
            {
                saveFileDialog.IsFolderPicker = true;
                saveFileDialog.AllowNonFileSystemItems = false;
                saveFileDialog.Multiselect = false;
                saveFileDialog.EnsurePathExists = true;
                if (saveFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    FormData.ExportPath = saveFileDialog.FileName;
                    FormData.ExportType = TSFileTypes.FileTypeXmlv1;
                    FormData.ResultOk = true;
                    Close();
                }
            }
        }

        private void _exportToDatButton_OnClick(object sender, RoutedEventArgs e)
        {
            using (CommonOpenFileDialog saveFileDialog = new CommonOpenFileDialog())
            {
                saveFileDialog.IsFolderPicker = true;
                saveFileDialog.AllowNonFileSystemItems = false;
                saveFileDialog.Multiselect = false;
                saveFileDialog.EnsurePathExists = true;
                if (saveFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    FormData.ExportPath = saveFileDialog.FileName;
                    FormData.ExportType = TSFileTypes.FileTypeDat;
                    FormData.ResultOk = true;
                    Close();
                }
            }
        }
    }
}