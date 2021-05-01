using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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

namespace DARYO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            float a = 2.4f;
        }
        
        private void firstCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (firstCombobox.Text != "")
            {
                if (firstCombobox.SelectedIndex == 0) secondCombobox.SelectedIndex = 0;
                else secondCombobox.SelectedIndex = 1;
                translate();
            }
        }

        private void secondCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (secondCombobox.Text != "")
            {
                if (secondCombobox.SelectedIndex == 0) firstCombobox.SelectedIndex = 0;
                else firstCombobox.SelectedIndex = 1;
                translate();
            }
        }

        private async void translate()
        {
            try
            {
                string lang_from, lang_to;
                if (firstCombobox.Text == "Uzbek")
                {
                    lang_from = "uz"; lang_to = "kaa";
                }
                else
                {`
                    lang_from = "kaa"; lang_to = "uz";
                }
                string json = "{ \"body\": { \"lang_from\": \"" + lang_from + "\", \"lang_to\": \"" + lang_to + "\", \"text\": \"" + firstAreaTxt.Text + "\" } }";
                var client = new HttpClient();
                var response = await client.PostAsync("https://api.from-to.uz/api/v1/translate", new StringContent(json, Encoding.UTF8, "application/json"));
                var responseString = await response.Content.ReadAsStringAsync();
                var res = JObject.Parse(responseString)["result"];
                secondAreaTxt.Text = res.ToString();
            }
            catch
            {
                secondAreaTxt.Text = "No Internet...";
            }
        }

        private void firstAreaTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            translate();
        }

        private void translatorBtn_Click(object sender, RoutedEventArgs e)
        {
            var converter = new BrushConverter();
            var color = converter.ConvertFromString("#209CEE");
            translatorBtn.Background = (Brush)color;
            translatorBtn.Foreground = Brushes.White;

            aboutBtn.Background = Brushes.White;
            aboutBtn.Foreground = Brushes.DodgerBlue;
        }

        private void aboutBtn_Click(object sender, RoutedEventArgs e)
        {
            var converter = new BrushConverter();
            var color = converter.ConvertFromString("#209CEE");
            aboutBtn.Background = (Brush)color;
            aboutBtn.Foreground = Brushes.White;

            translatorBtn.Background = Brushes.White;
            translatorBtn.Foreground = Brushes.DodgerBlue;

            borderFace.Visibility = Visibility.Visible;
            var aboutWindow = new About();
            aboutWindow.Owner = this;
            aboutWindow.ShowDialog();
            translatorBtn_Click(sender, null);
            borderFace.Visibility = Visibility.Collapsed;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //string path = AppDomain.CurrentDomain.BaseDirectory;
            //string FileName = string.Format("{0}Resources\\free-screen-recorder-softnic.exe", System.IO.Path.GetFullPath(System.IO.Path.Combine(path, @"..\..\")));
            //Process.Start(FileName);

        }
    }
}
