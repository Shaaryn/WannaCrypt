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
    using Crypt.App.ViewModel;
    using Microsoft.Win32;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel mainVM;
        private EncryptionViewModel encryptVM;
        private DecryptionViewModel decryptVM;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.mainVM = this.FindResource("MainVM") as MainViewModel;
            this.encryptVM = this.FindResource("EncryptionVM") as EncryptionViewModel;
            this.decryptVM = this.FindResource("DecryptionVM") as DecryptionViewModel;
        }

        private void OpenFileExplorerToBrowse(object sender, RoutedEventArgs e)
        {
            string path = Directory.GetCurrentDirectory();
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = path;
            dlg.DefaultExt = ".png";
            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                Button senderButton = sender as Button;

                if (senderButton.Tag.ToString() == "encrypt")
                {
                    this.encryptVM.EncryptFileObject.Path = dlg.FileName;
                    this.encryptVM.EncryptFileObject.FileName = dlg.SafeFileName;
                    this.encryptVM.EncryptFileObject.Extension = dlg.DefaultExt;
                }
                else if (senderButton.Tag.ToString() == "decrypt")
                {
                    this.decryptVM.DecryptFileObject.Path = dlg.FileName;
                    this.decryptVM.DecryptFileObject.FileName = dlg.SafeFileName;
                    this.decryptVM.DecryptFileObject.Extension = dlg.DefaultExt;
                }
            }
        }

        private void DecideEncryptionType(object sender, RoutedEventArgs e)
        {
            if (isFileEncrypt.IsChecked ?? false)
            {
                this.encryptVM.IsFileEncryption = true;
            }

            if (isFileDecrypt.IsChecked ?? false)
            {
                this.decryptVM.IsFileDecryption = true;
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

        private void CopyKey(object sender, RoutedEventArgs e)
        {
            this.decryptVM.KeyStringDecryption = this.encryptVM.KeyStringEncryption;
        }

        private void CopyConfiguration(object sender, RoutedEventArgs e)
        {
            this.decryptVM.CryptSize = this.encryptVM.CryptSize;
        }
    }
}
