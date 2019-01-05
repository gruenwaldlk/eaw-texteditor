using System.ComponentModel;
using System.Windows.Media;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using ts.translation.Annotations;
using ts.translation.data.holder.observables;

namespace eaw_texteditor.shared.data.dialogs.edit
{
    class EditTextKeyWindowData : INotifyPropertyChanged
    {
        private ObservableTranslationData _translation;

        public bool TranslationChanged = false;

        public ObservableTranslationData Translation
        {
            get => _translation;
            set
            {
                _translation = value;
                TranslationChanged = true;
                OnPropertyChanged(nameof(Translation));
            }
        }

        public string Key
        {
            get => DoValidate(_translation?.Key);
            set
            {
                if (_translation == null) return;
                _translation.Key = DoValidate(value);
                OnPropertyChanged(nameof(Key));
            }
        }

        public string Value
        {
            get => _translation?.Value;
            set
            {
                if (_translation == null) return;
                _translation.Value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        private bool _isKeyEditable;

        public bool IsKeyEditable
        {
            get => _isKeyEditable;
            set
            {
                _isKeyEditable = value;
                OnPropertyChanged(nameof(IsKeyEditable));
            }
        }

        private bool _isValidKey;

        public bool IsValidKey
        {
            get => _isValidKey;
            set
            {
                _isValidKey = value;
                SetValidStateForKeyTextBox();
                OnPropertyChanged(nameof(IsValidKey));
            }
        }

        private void SetValidStateForKeyTextBox()
        {
            if (!IsValidKey)
            {
                BackgroundColor = new SolidColorBrush() {Color = Color.FromArgb(100, 220, 20, 60)};
                IsBoltVisible = Visibility.Visible;
            }
            else{
                BackgroundColor = new SolidColorBrush() {Color = Color.FromArgb(255, 255, 255, 255)};
                IsBoltVisible = Visibility.Collapsed;
            }
        }

        private Brush _backgroundBrush;

        public Brush BackgroundColor
        {
            get => _backgroundBrush;
            set
            {
                _backgroundBrush = value;
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }

        private string DoValidate(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                IsValidKey = false;
                return string.Empty;
            }
            string newValue = value.TrimEnd();
            Regex regEx = new Regex("^[A-Z0-9_]*$");
            IsValidKey = regEx.Match(newValue).Success;
            return newValue;
        }

        private Visibility _isBoltVisible = Visibility.Collapsed;

        public Visibility IsBoltVisible
        {
            get => _isBoltVisible;
            set
            {
                _isBoltVisible = value;
                OnPropertyChanged(nameof(IsBoltVisible));
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
