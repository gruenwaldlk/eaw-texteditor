using System;
using System.Windows;
using eaw_texteditor.shared.data.dialogs.edit;
using MahApps.Metro.SimpleChildWindow;
using ts.translation;
using ts.translation.common.typedefs;
using ts.translation.data.holder.observables;

namespace eaw_texteditor.client.ui.dialogs.edit
{
    /// <summary>
    /// Interaction logic for EditTextKeyWindow.xaml
    /// </summary>
    public partial class EditTextKeyWindow : ChildWindow
    {
        internal EditTextKeyWindowData FormData { get; set; }
        public EditTextKeyWindow(PGLanguage selectedLanguage, ObservableTranslationData translationEnglish, ObservableTranslationData translationGerman, ObservableTranslationData translationFrench, ObservableTranslationData translationItalian, ObservableTranslationData translationSpanish)
        {
            InitializeComponent();
            FormData = new EditTextKeyWindowData() { SelectedLanguage = selectedLanguage, TranslationEnglish = translationEnglish, TranslationGerman = translationGerman, TranslationFrench = translationFrench, TranslationItalian = translationItalian, TranslationSpanish = translationSpanish };
            if (FormData.Key == string.Empty && (FormData.EnglishText == string.Empty || FormData.SpanishText == string.Empty || FormData.FrenchText == string.Empty || FormData.GermanText == string.Empty || FormData.ItalianText == string.Empty))
            {
                FormData.IsKeyEditable = true;
                FormData.IsValidKey = false;
            }
            DataContext = FormData;
        }

        private void OnClosingFinished(object sender, RoutedEventArgs e)
        {
            foreach (PGLanguage loadedLanguage in PGTEXTS.GetLoadedLanguages())
            {
                switch (loadedLanguage)
                {
                    case PGLanguage.ENGLISH:
                        if (string.IsNullOrEmpty(FormData.EnglishText))
                        {
                            FormData.EnglishText = $"TODO: {FormData.FallbackText}";
                        }
                        PGTEXTS.SetText(FormData.Key, FormData.EnglishText, loadedLanguage);
                        break;
                    case PGLanguage.FRENCH:
                        if (string.IsNullOrEmpty(FormData.FrenchText))
                        {
                            FormData.FrenchText = $"TODO: {FormData.FallbackText}";
                        }
                        PGTEXTS.SetText(FormData.Key, FormData.FrenchText, loadedLanguage);
                        break;
                    case PGLanguage.ITALIAN:
                        if (string.IsNullOrEmpty(FormData.ItalianText))
                        {
                            FormData.ItalianText = $"TODO: {FormData.FallbackText}";
                        }
                        PGTEXTS.SetText(FormData.Key, FormData.ItalianText, loadedLanguage);
                        break;
                    case PGLanguage.GERMAN:
                        if (string.IsNullOrEmpty(FormData.GermanText))
                        {
                            FormData.GermanText = $"TODO: {FormData.FallbackText}";
                        }
                        PGTEXTS.SetText(FormData.Key, FormData.GermanText, loadedLanguage);
                        break;
                    case PGLanguage.SPANISH:
                        if (string.IsNullOrEmpty(FormData.SpanishText))
                        {
                            FormData.SpanishText = $"TODO: {FormData.FallbackText}";
                        }
                        PGTEXTS.SetText(FormData.Key, FormData.SpanishText, loadedLanguage);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
        }

        private void EditTextKeyWindow_Loaded(object sender, RoutedEventArgs e)
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
