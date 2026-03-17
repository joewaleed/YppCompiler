using System.Configuration;
using System.Data;
using System.Windows;

namespace Ypp_Compiler {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            string savedTheme = Ypp_Compiler.Properties.Settings.Default.CurrentTheme;

            if (!string.IsNullOrEmpty(savedTheme)) {
                var uri = new Uri(savedTheme, UriKind.Relative);
                ResourceDictionary theme = new ResourceDictionary { Source = uri };

                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(theme);
            }
        }
    }
}

