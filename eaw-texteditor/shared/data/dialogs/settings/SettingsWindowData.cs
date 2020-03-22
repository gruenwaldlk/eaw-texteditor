using System.Collections.Generic;
using ts.translation.common.typedefs;

namespace eaw_texteditor.shared.data.dialogs.settings
{
    class SettingsWindowData : AWindowData
    {
        private List<PGLanguage> _languages = new List<PGLanguage>();

        public List<PGLanguage> Languages
        {
            get => _languages;
            set
            {
                _languages = value;
                OnPropertyChanged(nameof(Languages));
            }
        }

        private PGLanguage _selectedLanguage = Properties.Settings.Default.USR_LOADED_LANGUAGE;

        public PGLanguage SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                _selectedLanguage = value;
                OnPropertyChanged(nameof(SelectedLanguage));
            }
        }

        public SettingsWindowData(List<PGLanguage> languages)
        {
            if (languages != null)
            {
                Languages = languages;
            }
        }
    }
}
