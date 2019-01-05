using System.ComponentModel;
using System.Runtime.CompilerServices;
using ts.translation.Annotations;
using ts.translation.common.typedefs;

namespace eaw_texteditor.shared.data.dialogs.load
{
    internal class LoadFromFileWindowData : INotifyPropertyChanged
    {
        private string _importPath;
        private TSFileTypes _importType;

        public string ImportPath
        {
            get => _importPath;
            set
            {
                _importPath = value;
                OnPropertyChanged(nameof(ImportPath));
            }
        }

        public TSFileTypes ImportType
        {
            get => _importType;
            set
            {
                _importType = value;
                OnPropertyChanged(nameof(ImportType));
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
