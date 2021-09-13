using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace TextEditorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<double> fontSizes = new List<double>()
            {
                8,9,10,11,12,14,16,18,20,22,24,26,28,36,48,72
            };
            cbFontSize.ItemsSource = fontSizes;
        }
        private void Drag(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (WindowState == WindowState.Maximized)
                {
                    WindowState = WindowState.Normal;
                }
            }
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e) => Close();

        private void Button_Maximized_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void Button_Minimized_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void BtnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "All files|*.*|Text files|*.txt";
            openFileDialog1.FilterIndex = 2;

            if (openFileDialog1.ShowDialog() == true)
            {
                using (StreamReader streamReader = new StreamReader(openFileDialog1.FileName))
                {
                    txbText.Text = streamReader.ReadToEnd();
                    txbFilePath.Text = openFileDialog1.FileName;
                }

            }
            txbFilePath.IsEnabled = true;
            btnCopy.IsEnabled = true;
            btnCut.IsEnabled = true;
            btnPaste.IsEnabled = true;
            btnSave.IsEnabled = true;
            btnSelectAll.IsEnabled = true;
            cbFontFamily.IsEnabled = true;
            cbFontSize.IsEnabled = true;
            txbText.IsEnabled = true;

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txbFilePath.Text.Length != 0)
            {
                File.WriteAllText(txbFilePath.Text, txbText.Text);
                MessageBox.Show("Succesfully saved", "Completed", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please write a file path name!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ButtonTextOperations_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.Content.ToString() == "Cut")
                    txbText.Cut();
                else if (btn.Content.ToString() == "Copy")
                    txbText.Copy();
                else if (btn.Content.ToString() == "Paste")
                    txbText.Paste();
                else if (btn.Content.ToString() == "Select all")
                    txbText.SelectAll();
                txbText.Focus();
            }
        }

        private void CbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e) => txbText.FontFamily = new FontFamily(cbFontFamily.SelectedItem.ToString());

        private void CbFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e) => txbText.FontSize = double.Parse(cbFontSize.SelectedItem.ToString());

        private void ToggleButtonAutoSave_Click(object sender, RoutedEventArgs e)
        {
            if (ToggleButtonAutoSave.IsChecked == true)
            {
                if (txbFilePath.Text.Length != 0)
                {
                    File.WriteAllText(txbFilePath.Text, txbText.Text);
                }
                else
                {
                    MessageBox.Show("Please write a file path name!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ToggleButtonAutoSave.IsChecked = false;
                }
            }
        }

        private void TxbText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ToggleButtonAutoSave.IsChecked == true)
            {
                if (txbFilePath.Text.Length != 0)
                {
                    File.WriteAllText(txbFilePath.Text, txbText.Text);
                }
                else
                {
                    MessageBox.Show("Please write a file path name!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ToggleButtonAutoSave.IsChecked = false;
                }
            }
        }
    }
}
