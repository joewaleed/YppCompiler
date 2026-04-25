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
            MessageBox.Show("Thank you for trying ypp compiler early access.\nPlease note that this is v0.5 and some functionalities might not work.\nEnjoy!", "Welcome",MessageBoxButton.OK,MessageBoxImage.Information);
            TextEditor.Text = """
                int x = 10;
                int y = 15;
                incase(x>y){
                     x =5;
                } else {
                    x = 15;
                }

                repeat {
                    x = x+1;
                } until (x == 20);

                """;
        }

        private void Run_Click(object sender, RoutedEventArgs e) {
            MatchCollection matches = TextEditor.Text.GetMatch(); //Note : this is an extension method defined in RegexValues.cs
            List<Token> tokenList = new List<Token>();

            foreach (Match match in matches)
                        tokenList.Add(new Token{ 
                            Value = match.Value,
                            Type = match.Value.IsMatch() //This is another extension method defined in RegexValues.cs
                        });
            

            Scanner_Output.ItemsSource = tokenList;

            try {
                if (tokenList.Count == 0) return;

                yppParser parser = new yppParser(tokenList);
                parser.ParseStart();

                MessageBox.Show("Parsing completed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            } catch(Exception ex) {
                MessageBox.Show($"Parsing failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            } catch (Exception) {
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