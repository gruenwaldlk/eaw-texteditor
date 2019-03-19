using System.Windows;
using eaw_texteditor.Properties;
using MahApps.Metro;

namespace eaw_texteditor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ThemeManager.ChangeAppStyle(Current, ThemeManager.GetAccent(Settings.Default.USR_ACCENT_COLOUR), ThemeManager.GetAppTheme(Settings.Default.USR_BASE_THEME));
            base.OnStartup(e);
        }
    }
}
