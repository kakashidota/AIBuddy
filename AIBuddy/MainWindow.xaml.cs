using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace AIBuddy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string API_URL = "https://api.openai.com/v1/completions";
        private const string API_KEY = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void sendButton_Click(object sender, RoutedEventArgs e)
        {

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {API_KEY}");

            var json = new
            {
                prompt = inputBox.Text,
                model = "text-davinci-003",
                max_tokens = 1000,
                temperature = 0.2
            };


            var responseContent = await client.PostAsync(API_URL, new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));

            var resContext  = await  responseContent.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<dynamic>(resContext);
            respondText.Text = data.choices[0].text;





        }
    }
}
