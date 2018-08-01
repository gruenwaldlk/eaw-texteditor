using System.ComponentModel;
using System.Runtime.CompilerServices;
using ts.translation.Annotations;

namespace eaw_texteditor.shared.data
{
    public abstract class AWindowData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}