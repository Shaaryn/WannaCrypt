// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="AESEncryptionView.xaml.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.App.View
{
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using Crypt.App.ViewModel;
    using Crypt.App.ViewModel.AES;
    using MaterialDesignThemes.Wpf;
    using Microsoft.Win32;

    /// <summary>
    /// Interaction logic for AESEncryptionView.xaml
    /// </summary>
    public partial class AESView : Page
    {
        private EncryptionViewModel encryptVM;
        private DecryptionViewModel decryptVM;

        /// <summary>
        /// Initializes a new instance of the <see cref="AESView"/> class.
        /// </summary>
        public AESView()
        {
            InitializeComponent();

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

        private async void OpenEncryptResultDirectory(object sender, RoutedEventArgs e)
        {
            DialogHost dh = new DialogHost();
            //var result = await dh.ShowDialog(this.mainVM);

            //string path = Directory.GetCurrentDirectory() + "\\EncryptedFiles";
            //OpenFileDialog dlg = new OpenFileDialog();
            //dlg.InitialDirectory = path;
            //dlg.ShowDialog();
        }

        private void OpenDecryptResultDirectory(object sender, RoutedEventArgs e)
        {
            string path = Directory.GetCurrentDirectory() + "\\DecryptedFiles";
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = path;
            dlg.ShowDialog();
        }

        private void DecideEncryptionType(object sender, RoutedEventArgs e)
        {
            if (isFileEncrypt.IsChecked ?? false)
            {
                this.encryptVM.IsFileEncryption = true;
            }
            else if (!isFileEncrypt.IsChecked ?? false)
            {
                this.encryptVM.IsFileEncryption = false;
            }

            if (isFileDecrypt.IsChecked ?? false)
            {
                this.decryptVM.IsFileDecryption = true;
            }
            else if (!isFileDecrypt.IsChecked ?? false)
            {
                this.decryptVM.IsFileDecryption = false;
            }
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
