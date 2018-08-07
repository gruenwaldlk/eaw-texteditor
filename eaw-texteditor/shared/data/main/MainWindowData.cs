using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Data;
using eaw_texteditor.shared.common.util.search;
using ts.translation.Annotations;
using ts.translation.common.typedefs;
using ts.translation.data.holder.observables;

namespace eaw_texteditor.shared.data.main
{
    public class MainWindowData : INotifyPropertyChanged
    {
        private bool _isAdvancedSearchCheckBoxChecked;

        public bool IsAdvancedSearchCheckBoxChecked
        {
            get => _isAdvancedSearchCheckBoxChecked;
            set
            {
                _isAdvancedSearchCheckBoxChecked = value;
                if (!value)
                {
                    UseSimpleSearch = true;
                    IsMatchCaseChecked = true;
                }
                OnPropertyChanged(nameof(IsAdvancedSearchCheckBoxChecked));
            }
        }

        private bool _isKeySearchChecked;

        public bool IsKeySearchChecked
        {
            get => _isKeySearchChecked;
            set
            {
                _isKeySearchChecked = value;
                OnPropertyChanged(nameof(IsKeySearchChecked));
            }
        }

        private bool _isValueSearchChecked;

        public bool IsValueSearchChecked
        {
            get => _isValueSearchChecked;
            set
            {
                _isValueSearchChecked = value;
                OnPropertyChanged(nameof(IsValueSearchChecked));
            }
        }

        private bool _useSimpleSearch;

        public bool UseSimpleSearch
        {
            get => _useSimpleSearch;
            set
            {
                _useSimpleSearch = value;
                OnPropertyChanged(nameof(UseSimpleSearch));
            }
        }

        private bool _useWildCardSearch;

        public bool UseWildCardSearch
        {
            get => _useWildCardSearch;
            set
            {
                _useWildCardSearch = value;
                OnPropertyChanged(nameof(UseWildCardSearch));
            }
        }

        private bool _useRegExSearch;

        public bool UseRegExSearch
        {
            get => _useRegExSearch;
            set
            {
                _useRegExSearch = value;
                OnPropertyChanged(nameof(UseRegExSearch));
            }
        }

        private bool _isMatchCaseChecked;

        public bool IsMatchCaseChecked
        {
            get => _isMatchCaseChecked;
            set
            {
                _isMatchCaseChecked = value;
                OnPropertyChanged(nameof(IsMatchCaseChecked));
            }

        }

        private string _searchTerm;

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                _searchTerm = value;
                if (TranslationCollection == null) return;
                if (UseRegExSearch)
                {
                    OnPropertyChanged(nameof(SearchTerm));
                    return;
                }

                if (UseWildCardSearch)
                {
                    OnPropertyChanged(nameof(SearchTerm));
                    return;
                }
                TryRefresh();
                OnPropertyChanged(nameof(SearchTerm));
            }
        }

        private Predicate<object> GetFilter()
        {
            if (IsKeySearchChecked)
            {
                if (UseSimpleSearch)
                {
                    if (IsMatchCaseChecked)
                    {
                        return Key_UseSimpleSearch_MatchCase;
                    }
                    else
                    {
                        return Key_UseSimpleSearch_IgnoreCase;
                    }    
                }

                if (UseRegExSearch)
                {
                    if (IsMatchCaseChecked)
                    {
                        return Key_UseRegExSearch_MatchCase;
                    }
                    else
                    {
                        return Key_UseRegExSearch_IgnoreCase;
                    }
                }

                if (UseWildCardSearch)
                {
                    if (IsMatchCaseChecked)
                    {
                        return Key_UsePatternMatchSearch_MatchCase;
                    }
                    else
                    {
                        return Key_UsePatternMatchSearch_IgnoreCase;
                    }
                }
            }
            else
            {
                if (UseSimpleSearch)
                {
                    if (IsMatchCaseChecked)
                    {
                        return Value_UseSimpleSearch_MatchCase;
                    }
                    else
                    {
                        return Value_UseSimpleSearch_IgnoreCase;
                    }
                }
                if (UseRegExSearch)
                {
                    if (IsMatchCaseChecked)
                    {
                        return Value_UseRegExSearch_MatchCase;
                    }
                    else
                    {
                        return Value_UseRegExSearch_IgnoreCase;
                    }
                }

                if (UseWildCardSearch)
                {
                    if (IsMatchCaseChecked)
                    {
                        return Value_UsePatternMatchSearch_MatchCase;
                    }
                    else
                    {
                        return Value_UsePatternMatchSearch_IgnoreCase;
                    }
                }
            }

            return Key_UseSimpleSearch_IgnoreCase;
        }

        private bool Key_UseSimpleSearch_MatchCase(object value)
        {
            if (!(value is ObservableTranslationData entry)) return false;
            return string.IsNullOrEmpty(SearchTerm) || entry.Key.Contains(SearchTerm);
        }

        private bool Key_UseSimpleSearch_IgnoreCase(object value)
        {
            if (!(value is ObservableTranslationData entry)) return false;
            return string.IsNullOrEmpty(SearchTerm) || entry.Key.ToLower().Contains(SearchTerm.ToLower());
        }

        private bool Key_UseRegExSearch_MatchCase(object value)
        {
            if (!(value is ObservableTranslationData entry)) return false;
            return string.IsNullOrEmpty(SearchTerm) || SearchUtility.RegExMatch(SearchTerm, entry.Key);
        }

        private bool Key_UseRegExSearch_IgnoreCase(object value)
        {
            if (!(value is ObservableTranslationData entry)) return false;
            return string.IsNullOrEmpty(SearchTerm) || SearchUtility.RegExMatch(SearchTerm, entry.Key, RegexOptions.IgnoreCase);
        }
        private bool Key_UsePatternMatchSearch_MatchCase(object value)
        {
            if (!(value is ObservableTranslationData entry)) return false;
            return string.IsNullOrEmpty(SearchTerm) || SearchUtility.PatternMatch(SearchTerm, entry.Key);
        }
        private bool Key_UsePatternMatchSearch_IgnoreCase(object value)
        {
            if (!(value is ObservableTranslationData entry)) return false;
            return string.IsNullOrEmpty(SearchTerm) || SearchUtility.PatternMatch(SearchTerm, entry.Key, RegexOptions.IgnoreCase);
        }

        private bool Value_UseRegExSearch_MatchCase(object value)
        {
            if (!(value is ObservableTranslationData entry)) return false;
            return string.IsNullOrEmpty(SearchTerm) || SearchUtility.RegExMatch(SearchTerm, entry.Value);
        }

        private bool Value_UseRegExSearch_IgnoreCase(object value)
        {
            if (!(value is ObservableTranslationData entry)) return false;
            return string.IsNullOrEmpty(SearchTerm) || SearchUtility.RegExMatch(SearchTerm, entry.Value, RegexOptions.IgnoreCase);
        }

        private bool Value_UseSimpleSearch_MatchCase(object value)
        {
            if (!(value is ObservableTranslationData entry)) return false;
            return string.IsNullOrEmpty(SearchTerm) || entry.Value.Contains(SearchTerm);
        }

        private bool Value_UseSimpleSearch_IgnoreCase(object value)
        {
            if (!(value is ObservableTranslationData entry)) return false;
            return string.IsNullOrEmpty(SearchTerm) || entry.Value.ToLower().Contains(SearchTerm.ToLower());
        }

        private bool Value_UsePatternMatchSearch_MatchCase(object value)
        {
            if (!(value is ObservableTranslationData entry)) return false;
            return string.IsNullOrEmpty(SearchTerm) || SearchUtility.PatternMatch(SearchTerm, entry.Value);
        }
        private bool Value_UsePatternMatchSearch_IgnoreCase(object value)
        {
            if (!(value is ObservableTranslationData entry)) return false;
            return string.IsNullOrEmpty(SearchTerm) || SearchUtility.PatternMatch(SearchTerm, entry.Value, RegexOptions.IgnoreCase);
        }


        private ICollectionView _translationCollection;
        public ICollectionView TranslationCollection
        {
            get => _translationCollection;
            set {
                _translationCollection = value;
                OnPropertyChanged(nameof(TranslationCollection));

            }
        }

        private PGLanguage _selectedLanguage = Properties.Settings.Default.USR_LOADED_LANGUAGE;

        public PGLanguage SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (!Sources.ContainsKey(value)) return;
                _selectedLanguage = value;
                TranslationCollection = Sources[value].View;
                Properties.Settings.Default.USR_LOADED_LANGUAGE = value;
                OnPropertyChanged(nameof(SelectedLanguage));
            }
        }

        public void TryRefresh()
        {
            if (TranslationCollection == null) return;
            TranslationCollection.Filter = GetFilter();
            TranslationCollection.Refresh();
        }

        Dictionary<PGLanguage, CollectionViewSource> _sources = new Dictionary<PGLanguage, CollectionViewSource>();

        public Dictionary<PGLanguage, CollectionViewSource> Sources { get => _sources;
            set
            {
                _sources = value;
                OnPropertyChanged(nameof(Sources));
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
