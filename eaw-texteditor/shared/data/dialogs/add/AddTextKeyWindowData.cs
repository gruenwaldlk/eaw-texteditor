using eaw_texteditor.shared.data.dialogs.edit;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using ts.translation;

namespace eaw_texteditor.shared.data.dialogs.add
{
    internal class AddTextKeyWindowData : EditTextKeyWindowData
    {
        private bool _isDuplicate;

        public bool IsDuplicate
        {
            get => _isDuplicate;
            set
            {
                _isDuplicate = value;
                SetValidStateForKeyTextBox();
                OnPropertyChanged(nameof(IsValidKey));
            }
        }

        public bool IsValid
        {
            get
            {
                return !IsDuplicate && IsValidKey;
            }
        }

        private Visibility _isDuplicateVisible = Visibility.Collapsed;

        public Visibility IsDuplicateVisible
        {
            get => _isDuplicateVisible;
            set
            {
                _isDuplicateVisible = value;
                OnPropertyChanged(nameof(IsDuplicateVisible));
            }
        }

        protected override void SetValidStateForKeyTextBox()
        {
            IsInvalidVisible = !IsValidKey ? Visibility.Visible : Visibility.Collapsed;
            IsDuplicateVisible = IsDuplicate ? Visibility.Visible : Visibility.Collapsed;
            if (!IsValidKey || IsDuplicate)
            {
                BackgroundColor = new SolidColorBrush() { Color = Color.FromArgb(100, 220, 20, 60) };
                IsBoltVisible = Visibility.Visible;
            }
            else
            {
                BackgroundColor = new SolidColorBrush() { Color = Color.FromArgb(255, 255, 255, 255) };
                IsBoltVisible = Visibility.Collapsed;
            }
            OnPropertyChanged(nameof(IsValid));
        }

        protected override string DoValidate(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                IsValidKey = false;
                IsDuplicate = false;
                return string.Empty;
            }
            string newValue = value.TrimEnd();
            Regex regEx = new Regex("^[A-Z0-9_]*$");
            IsValidKey = regEx.Match(newValue).Success;
            IsDuplicate = PGTEXTS.HasText(newValue);
            return newValue;
        }
    }
}