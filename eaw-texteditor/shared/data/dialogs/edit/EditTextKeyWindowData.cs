using System;
using System.ComponentModel;
using System.Windows.Media;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using ts.translation.Annotations;
using ts.translation.common.typedefs;
using ts.translation.data.holder.observables;

namespace eaw_texteditor.shared.data.dialogs.edit
{
    class EditTextKeyWindowData : INotifyPropertyChanged
    {
        private ObservableTranslationData _translationEnglish;
        private ObservableTranslationData _translationGerman;
        private ObservableTranslationData _translationFrench;
        private ObservableTranslationData _translationItalian;
        private ObservableTranslationData _translationSpanish;

        public string FallbackText { get; set; }

        public bool TranslationChanged = false;

        public ObservableTranslationData TranslationEnglish
        {
            get => _translationEnglish;
            set
            {
                _translationEnglish = value;
                TranslationChanged = true;
                OnPropertyChanged(nameof(TranslationEnglish));
            }
        }

        public ObservableTranslationData TranslationGerman
        {
            get => _translationGerman;
            set
            {
                _translationGerman = value;
                TranslationChanged = true;
                OnPropertyChanged(nameof(TranslationGerman));
            }
        }

        public ObservableTranslationData TranslationFrench
        {
            get => _translationFrench;
            set
            {
                _translationFrench = value;
                TranslationChanged = true;
                OnPropertyChanged(nameof(TranslationFrench));
            }
        }

        public ObservableTranslationData TranslationItalian
        {
            get => _translationItalian;
            set
            {
                _translationItalian = value;
                TranslationChanged = true;
                OnPropertyChanged(nameof(TranslationItalian));
            }
        }


        public ObservableTranslationData TranslationSpanish
        {
            get => _translationSpanish;
            set
            {
                _translationSpanish = value;
                TranslationChanged = true;
                OnPropertyChanged(nameof(TranslationSpanish));
            }
        }

        public string Key
        {
            get
            {
                if (_translationEnglish != null)
                {
                    return DoValidate(_translationEnglish?.Key);
                }
                if (_translationGerman != null)
                {
                    return DoValidate(_translationGerman?.Key);
                }
                if (_translationFrench != null)
                {
                    return DoValidate(_translationFrench?.Key);
                }
                if (_translationItalian != null)
                {
                    return DoValidate(_translationItalian?.Key);
                }
                if (_translationSpanish != null)
                {
                    return DoValidate(_translationSpanish?.Key);
                }

                return DoValidate(string.Empty);
            }
            set
            {
                string validKey = DoValidate(value);
                if (_translationEnglish != null)
                {
                    _translationEnglish.Key = validKey;
                }
                if (_translationGerman != null)
                {
                    _translationGerman.Key = validKey;
                }
                if (_translationFrench != null)
                {
                    _translationFrench.Key = validKey;
                }
                if (_translationItalian != null)
                {
                    _translationItalian.Key = validKey;
                }
                if (_translationSpanish != null)
                {
                    _translationSpanish.Key = validKey;
                }
                OnPropertyChanged(nameof(Key));
            }
        }

        public string EnglishText
        {
            get => _translationEnglish?.Value;
            set
            {
                if (_translationEnglish == null) return;
                _translationEnglish.Value = value;
                DoUpdateFallbackText();
                OnPropertyChanged(nameof(EnglishText));
            }
        }

        public string GermanText
        {
            get => _translationGerman?.Value;
            set
            {
                if (_translationGerman == null) return;
                _translationGerman.Value = value;
                DoUpdateFallbackText();
                OnPropertyChanged(nameof(GermanText));
            }
        }

        public string FrenchText
        {
            get => _translationFrench?.Value;
            set
            {
                if (_translationFrench == null) return;
                _translationFrench.Value = value;
                DoUpdateFallbackText();
                OnPropertyChanged(nameof(FrenchText));
            }
        }

        public string ItalianText
        {
            get => _translationItalian?.Value;
            set
            {
                if (_translationItalian == null) return;
                _translationItalian.Value = value;
                DoUpdateFallbackText();
                OnPropertyChanged(nameof(ItalianText));
            }
        }

        private void DoUpdateFallbackText()
        {
            switch (SelectedLanguage)
            {
                case PGLanguage.ENGLISH:
                    FallbackText = EnglishText;
                    break;
                case PGLanguage.FRENCH:
                    FallbackText = FrenchText;
                    break;
                case PGLanguage.ITALIAN:
                    FallbackText = ItalianText;
                    break;
                case PGLanguage.GERMAN:
                    FallbackText = GermanText;
                    break;
                case PGLanguage.SPANISH:
                    FallbackText = SpanishText;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string SpanishText
        {
            get => _translationSpanish?.Value;
            set
            {
                if (_translationSpanish == null) return;
                _translationSpanish.Value = value;
                DoUpdateFallbackText();
                OnPropertyChanged(nameof(SpanishText));
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

        public PGLanguage SelectedLanguage { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            DoUpdateFallbackText();
        }
    }
}
