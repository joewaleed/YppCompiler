using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Ypp_Compiler {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            this.MaxWidth = SystemParameters.WorkArea.Width;
            this.MaxHeight = SystemParameters.WorkArea.Height;
            MessageBox.Show("Thank you for trying ypp compiler early access.\nPlease note that this is v0.1 and some functionalities might not work.\nEnjoy!", "Welcome",MessageBoxButton.OK,MessageBoxImage.Information);
            TextEditor.Text = """
                int x = 10;
                incase(x>y){
                     text message = "x is greater than y";
                } else {
                    text message = "x is not greater than y";
                }
                repeat 5 {
                    x += 1;
                }

                repeat {
                    x += 1;
                } until x == 20;

                repeat x > 30 {
                    x += 1;
                }
                """;
        }

        public class TokenRow {
            public string Token { get; set; }
            public string Lexeme { get; set; }
        }

        private void Run_Click(object sender, RoutedEventArgs e) {
            MatchCollection matches = TextEditor.Text.GetMatch(); //Note : this is an extension method defined in RegexValues.cs
            List<TokenRow> tokenRowsList = new List<TokenRow>();

            foreach (Match match in matches)
                        tokenRowsList.Add(new TokenRow { 
                            Token = match.Value,
                            Lexeme = match.Value.IsMatch() //This is another extension method defined in RegexValues.cs
                        });
            

            Scanner_Output.ItemsSource = tokenRowsList;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        private void Documentation_Click(object sender, RoutedEventArgs e) {
            string url = "https://github.com/joewaleed/YppCompiler";
            try {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo {
                    FileName = url,
                    UseShellExecute = true
                });
            } catch (Exception ex) {
                MessageBox.Show($"Unable to open documentation", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Edit Menu
        private void Copy_Click(object sender, RoutedEventArgs e) {
            Clipboard.SetText(TextEditor.SelectedText);
        }

        private void Cut_Click(object sender, RoutedEventArgs e) {
            Clipboard.SetText(TextEditor.SelectedText);
            TextEditor.SelectedText = "";
        }

        private void Paste_Click(object sender, RoutedEventArgs e) {
            string textToPaste = Clipboard.GetText();
            int caretIndex = TextEditor.CaretIndex;
            if (Clipboard.ContainsText()) {

                string newText = TextEditor.Text.Insert(caretIndex, textToPaste);
                TextEditor.Text = newText;

                TextEditor.CaretIndex = caretIndex + textToPaste.Length;
            }
        }

        private void Undo_Click(object sender, RoutedEventArgs e) {
            if(TextEditor.CanUndo) TextEditor.Undo();
        }

        private void Redo_Click(object sender, RoutedEventArgs e) {
            if(TextEditor.CanRedo) TextEditor.Redo();
        }
        #endregion

        #region View Menu
        private void SetTheme(string themeFile) {
            var uri = new Uri(themeFile, UriKind.Relative);

            ResourceDictionary newTheme = new ResourceDictionary { Source = uri };

            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(newTheme);

            Properties.Settings.Default.CurrentTheme = themeFile;
            Properties.Settings.Default.Save();
        }

        private void SetDark_Click(object sender, RoutedEventArgs e) {
            SetTheme("/Assets/Theme/DarkMode.xaml");
        }
        private void SetLight_Click(object sender, RoutedEventArgs e) {
            SetTheme("/Assets/Theme/LightMode.xaml");
        }
        #endregion
    }
}