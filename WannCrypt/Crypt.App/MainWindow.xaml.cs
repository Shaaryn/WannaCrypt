// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="MainWindow.xaml.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt
{
    using System.Windows;
    using System.Windows.Controls;
    using Crypt.ViewModel;

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
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".png";

            // dlg.Filter = "Text documents (.txt)|*.txt";
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
    }
}
