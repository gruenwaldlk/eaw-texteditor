using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ts.translation.Annotations;
using ts.translation.data.holder.observables;

namespace eaw_texteditor.shared.data.main
{
    public class MainWindowData : INotifyPropertyChanged
    {
        public bool IsAdvancedSearchCheckBoxChecked { get; set; }
        private bool _useSearchKey;

        public bool UseKeySearch
        {
            get => _useSearchKey;
            set
            {
                _useSearchKey = value;
                TryRefresh();
            }
        }

        private bool _useValueSearch;

        public bool UseValueSearch
        {
            get => _useValueSearch;
            set
            {
                _useValueSearch = value;
                TryRefresh();
            }
        }

        private bool _useSimpleSearch;

        public bool UseSimpleSearch
        {
            get => _useSimpleSearch;
            set
            {
                _useSimpleSearch = value;
                TryRefresh();
            }
        }

        private bool _useWildCardSearch;

        public bool UseWildCardSearch
        {
            get => _useWildCardSearch;
            set
            {
                _useWildCardSearch = value;
                TryRefresh();
            }
        }

        private bool _useRegExSearch;

        public bool UseRegExSearch
        {
            get => _useRegExSearch;
            set
            {
                _useRegExSearch = value;
                TryRefresh();
            }
        }

        private bool _matchCase;

        public bool MatchCase
        {
            get => _matchCase;
            set
            {
                _matchCase = value;
                TryRefresh();
            }

        }

        private string _searchTerm;

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                _searchTerm = value;
                OnPropertyChanged(nameof(SearchTerm));
                if (_listItemCollection == null) return;
                _listItemCollection.Filter = GetFilter();
                _listItemCollection.Refresh();
            }
        }

        private Predicate<object> GetFilter()
        {
            if (UseSimpleSearch)
            {
                if (UseKeySearch)
                {
                    if (MatchCase)
                    {
                        return Key_UseSimpleSearch_MatchCase;
                    }
                    else
                    {
                        return Key_UseSimpleSearch_IgnoreCase;
                    }
                }
                else
                {
                    if (MatchCase)
                    {
                        return Value_UseSimpleSearch_MatchCase;
                    }
                    else
                    {
                        return Value_UseSimpleSearch_IgnoreCase;
                    }
                }
            }
            else
            {
                if (UseKeySearch)
                {
                    if (MatchCase)
                    {
                        return Key_UseSimpleSearch_MatchCase;
                    }
                    else
                    {
                        return Key_UseSimpleSearch_IgnoreCase;
                    }
                }
                else
                {
                    if (MatchCase)
                    {
                        return Value_UseSimpleSearch_MatchCase;
                    }
                    else
                    {
                        return Value_UseSimpleSearch_IgnoreCase;
                    }
                }
            }
        }

        private bool Key_UseSimpleSearch_MatchCase(object value)
        {
            if (!(value is ObservableTranslationData entry)) return false;
            return string.IsNullOrEmpty(_searchTerm) || entry.Key.Contains(_searchTerm);
        }

        private bool Key_UseSimpleSearch_IgnoreCase(object value)
        {
            if (!(value is ObservableTranslationData entry)) return false;
            return string.IsNullOrEmpty(_searchTerm) || entry.Key.ToLower().Contains(_searchTerm.ToLower());
        }

        private bool Value_UseSimpleSearch_MatchCase(object value)
        {
            if (!(value is ObservableTranslationData entry)) return false;
            return string.IsNullOrEmpty(_searchTerm) || entry.Value.Contains(_searchTerm);
        }

        private bool Value_UseSimpleSearch_IgnoreCase(object value)
        {
            if (!(value is ObservableTranslationData entry)) return false;
            return string.IsNullOrEmpty(_searchTerm) || entry.Value.ToLower().Contains(_searchTerm.ToLower());
        }


        private ICollectionView _listItemCollection;
        public ICollectionView ListItemCollection
        {
            get => _listItemCollection;
            set {
                _listItemCollection = value;
                OnPropertyChanged(nameof(ListItemCollection));

            }
        }

        public void TryRefresh()
        {
            string t = SearchTerm;
            SearchTerm = t;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
