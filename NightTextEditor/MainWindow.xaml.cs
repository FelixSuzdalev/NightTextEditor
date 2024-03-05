using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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




namespace NightTextEditor
{
    public partial class MainWindow : Window
    {
        const string TITLE = "NightTextEditor";
        public MainWindow()
        {
            InitializeComponent();
            Input.TextChanged += TextBox_QuantitySymbol;
        }

        private void Button_Open(object sender, RoutedEventArgs e) //Обработчик кнопки "Открыть"
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if ((bool)(openFileDialog.ShowDialog()))
            {
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);

                this.Title = TITLE + " - " + fileInfo.Name;
                this.Way.Text = fileInfo.FullName;
                this.Input.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void Button_Safe(object sender, RoutedEventArgs e) //Обработчик кнопки "Сохранить"
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Сохранение текстового документа";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, this.Input.Text);
                FileInfo fileInfo = new FileInfo(saveFileDialog.FileName);
                this.Way.Text = fileInfo.FullName;
            }
        }
        private void TextBox_QuantitySymbol(object sender, TextChangedEventArgs e) //Подсчёт символов и слов в тексте
        {
            /* В данном случае абзац считается за 2 символа
            int K = Input.Text.Length;
            QuantitySymbol.Text = $"Количество символов: {K}"*/
            
            string text0 = Input.Text;
            int K = text0.Count(c => c != '\n'); //За один
            QuantitySymbol.Text = $"Cимволов: {K}";

            string text1 = Input.Text;
            string[] words = text1.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int Kword = words.Length;
            Words.Text = $"Cлов: {Kword}";

            
            string text2 = Input.Text;
            string[] paragraphs = text2.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            int Kparagraph= paragraphs.Length;
            Paragraph.Text = $"Aбзацев: {Kparagraph}";
        }

        private void FontFamilyComboBox_(object sender, SelectionChangedEventArgs e) //Выбор Шрифта
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            Input.FontFamily = new FontFamily(selectedItem.Content.ToString());
        }

        private void FontSizeComboBox_(object sender, SelectionChangedEventArgs e) //Размер шрифта
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            Input.FontSize = Convert.ToDouble(selectedItem.Content);
        }
    }
}
