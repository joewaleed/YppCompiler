using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Data;

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
                ?(x>y){
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
                MessageBox.Show($"Unable to open documentation: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}