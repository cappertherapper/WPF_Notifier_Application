using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
namespace WPFDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Path to the executable directory
        public string ExeDirectory { get; }

        //Path to the data file that contains saved messages
        public string DataFilePath { get; }

        // Collection to hold the messages
        public ListOfMessages MyMessages { get; } = new ListOfMessages();

        
        public MainWindow()
        {
            InitializeComponent();

            // Event handler for window close
            this.Closed += new EventHandler(Window_Closing);

            ExeDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            DataFilePath = System.IO.Path.Combine(ExeDirectory, "data.json");

            // If data file exists, read and deserialize it
            if (File.Exists(DataFilePath))
            {
                string loadedJsonString = File.ReadAllText(DataFilePath);
                MyMessages = JsonConvert.DeserializeObject<ListOfMessages>(loadedJsonString);

                // Attach event to each message
                foreach (var message in MyMessages)
                {
                    message.MessageTimeExpiredEvent += () =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            MyMessages.Remove(message);
                        });
                    };
                }
                this.DataContext = this;
            }
            else
            {
                MyMessages = new ListOfMessages();
                this.DataContext = this;
            }
        }

        // Event handler for submit button click
        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidTimeInterval(timerText.Text))
            {
                double timeInSeconds = double.Parse(timerText.Text);
                MyMessages.Add(messageText.Text, timeInSeconds);
            }
            else
            {
                MessageBox.Show("Please enter a valid number for the timer.");
            }
        }

        // Method for unit-testing valid time-interval inputs
        public bool IsValidTimeInterval(string input)
        {
            if (double.TryParse(input, out double parsedValue))
            {
                // Check if the number is in the desired range (0 to 500)
                return parsedValue > 0 && parsedValue <= 500;
            }
            return false;
        }

        // Save the current state of messages into a data file when window closes
        public void Window_Closing(object sender, EventArgs e)
        {
            string jsonString = JsonConvert.SerializeObject(MyMessages);
            string exeDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string path = System.IO.Path.Combine(exeDirectory, "data.json");
            File.WriteAllText(path, jsonString);
        }
        
    }
}
