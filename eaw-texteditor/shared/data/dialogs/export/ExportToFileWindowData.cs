using System.ComponentModel;
using System.Runtime.CompilerServices;
using ts.translation.Annotations;
using ts.translation.common.typedefs;

namespace eaw_texteditor.shared.data.dialogs.export
{
    class ExportToFileWindowData : INotifyPropertyChanged
    {
        private string _exportPath;
        private TSFileTypes _exportType;

        public string ExportPath
        {
            get => _exportPath;
            set
            {
                _exportPath = value;
                OnPropertyChanged(nameof(ExportPath));
            }
        }

        public TSFileTypes ExportType
        {
            get => _exportType;
            set
            {
                _exportType = value;
                OnPropertyChanged(nameof(ExportType));
            }
        }

        public bool ResultOk { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
