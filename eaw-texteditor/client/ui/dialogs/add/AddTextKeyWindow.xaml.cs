using System;
using System.Windows;
using eaw_texteditor.shared.data.dialogs.edit;
using MahApps.Metro.SimpleChildWindow;
using ts.translation;
using ts.translation.common.typedefs;
using ts.translation.data.holder.observables;

namespace eaw_texteditor.client.ui.dialogs.add
{
    /// <summary>
    /// Interaction logic for AddTextKeyWindow.xaml
    /// </summary>
    public partial class AddTextKeyWindow : ChildWindow
    {
        internal EditTextKeyWindowData FormData { get; set; }
        public AddTextKeyWindow(PGLanguage selectedLanguage, ObservableTranslationData translationEnglish, ObservableTranslationData translationGerman, ObservableTranslationData translationFrench, ObservableTranslationData translationItalian, ObservableTranslationData translationSpanish)
        {
            InitializeComponent();
            FormData = new EditTextKeyWindowData() {SelectedLanguage = selectedLanguage, TranslationEnglish = translationEnglish, TranslationGerman = translationGerman, TranslationFrench = translationFrench, TranslationItalian = translationItalian, TranslationSpanish = translationSpanish};
            DataContext = FormData;
            FormData.IsKeyEditable = true;
            FormData.IsValidKey = false;
            FormData.IsBoltVisible = Visibility.Visible;
        }



        private void OnAddClick(object sender, RoutedEventArgs e)
        {
            this.Close(true);
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            this.Close(false);
        }

        private void AddTextKeyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (PGLanguage loadedLanguage in PGTEXTS.GetLoadedLanguages())
            {
                switch (loadedLanguage)
                {
                    case PGLanguage.ENGLISH:
                        _valueEnglishLabel.Visibility = Visibility.Visible;
                        _valueEnglishTextBox.Visibility = Visibility.Visible;
                        break;
                    case PGLanguage.FRENCH:
                        _valueFrenchLabel.Visibility = Visibility.Visible;
                        _valueFrenchTextBox.Visibility = Visibility.Visible;
                        break;
                    case PGLanguage.ITALIAN:
                        _valueItalianLabel.Visibility = Visibility.Visible;
                        _valueItalianTextBox.Visibility = Visibility.Visible;
                        break;
                    case PGLanguage.GERMAN:
                        _valueGermanLabel.Visibility = Visibility.Visible;
                        _valueGermanTextBox.Visibility = Visibility.Visible;
                        break;
                    case PGLanguage.SPANISH:
                        _valueSpanishLabel.Visibility = Visibility.Visible;
                        _valueSpanishTextBox.Visibility = Visibility.Visible;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
