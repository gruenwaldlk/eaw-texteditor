using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ts.translation.Annotations;
using ts.translation.data.holder.observables;

namespace eaw_texteditor.shared.data.main
{
    public class MainWindowData : INotifyPropertyChanged
    {
        public bool IsAdvancedSearchCheckBoxChecked { get; set; }
        public string SearchTerm { get; set; }
        private ObservableCollection<ObservableTranslationData> _observableTranslationDataHolder = new ObservableCollection<ObservableTranslationData>();

        public ObservableCollection<ObservableTranslationData> ObservableTranslationDataHolder
        {
            get => _observableTranslationDataHolder;
            set
            {
                _observableTranslationDataHolder = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
