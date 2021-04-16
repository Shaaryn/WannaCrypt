// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="MainWindow.xaml.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt
{
    using System.Diagnostics;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using Crypt.ViewModel;
    using Microsoft.Win32;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel mainVM;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.mainVM = this.FindResource("MainVM") as MainViewModel;
        }

        private void OpenFileExplorerToBrowse(object sender, RoutedEventArgs e)
        {
            string path = Directory.GetCurrentDirectory() + "\\.TryOuts";
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = path;
            dlg.DefaultExt = ".png";
            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                Button senderButton = sender as Button;

                if (senderButton.Tag.ToString() == "encrypt")
                {
                    this.mainVM.EncryptFileObject.Path = dlg.FileName;
                    this.mainVM.EncryptFileObject.FileName = dlg.SafeFileName;
                    this.mainVM.EncryptFileObject.Extension = dlg.DefaultExt;
                }
                else if (senderButton.Tag.ToString() == "decrypt")
                {
                    this.mainVM.DecryptFileObject.Path = dlg.FileName;
                    this.mainVM.DecryptFileObject.FileName = dlg.SafeFileName;
                    this.mainVM.DecryptFileObject.Extension = dlg.DefaultExt;
                }
            }
        }

        private void DecideEncryptionType(object sender, RoutedEventArgs e)
        {
            if (isFileEncrypt.IsChecked ?? false)
            {
                this.mainVM.IsFileEncryption = true;
            }

            if (isFileDecrypt.IsChecked ?? false)
            {
                this.mainVM.IsFileDecryption = true;
            }
        }

        private void OpenEncryptResultDirectory(object sender, RoutedEventArgs e)
        {
            string path = Directory.GetCurrentDirectory() + "\\EncryptedFiles";
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = path;
            dlg.ShowDialog();
        }

        private void OpenDecryptResultDirectory(object sender, RoutedEventArgs e)
        {
            string path = Directory.GetCurrentDirectory() + "\\DecryptedFiles";
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = path;
            dlg.ShowDialog();
        }
    }
}
