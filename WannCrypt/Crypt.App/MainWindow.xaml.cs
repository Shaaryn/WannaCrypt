// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="MainWindow.xaml.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using Crypt.App.View;
    using Crypt.App.ViewModel.AES;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private EncryptionViewModel encryptVM;
        private DecryptionViewModel decryptVM;

        private Page currentPage;
        private AESView viewAES;
        private DHView viewDH;

        /// <summary>
        /// Currently displayed (and being used) page.
        /// </summary>
        public Page CurrentPage
        {
            get
            {
                return currentPage;
            }

            set
            {
                currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        /// <summary>
        /// View(Page) of the AES form.
        /// </summary>
        public AESView ViewAES
        {
            get { return viewAES; }
            set { viewAES = value; }
        }

        /// <summary>
        /// View(Page) of the DH form.
        /// </summary>
        public DHView ViewDH
        {
            get { return viewDH; }
            set { viewDH = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.encryptVM = this.FindResource("EncryptionVM") as EncryptionViewModel;
            this.decryptVM = this.FindResource("DecryptionVM") as DecryptionViewModel;

            ViewAES = new AESView();
            currentPage = ViewAES;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DHPage(object sender, RoutedEventArgs e)
        {
            if (this.currentPage is not DHView)
            {
                if (this.ViewDH != null)
                {
                    this.CurrentPage = this.ViewDH;
                }
                else
                {
                    this.CurrentPage = new DHView();
                }
            }
        }

        private void AESPage(object sender, RoutedEventArgs e)
        {
            if (this.currentPage is not AESView)
            {
                if (this.ViewAES != null)
                {
                    this.CurrentPage = this.ViewAES;
                }
                else
                {
                    this.CurrentPage = new AESView();
                }
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="propertyName">Name of the property that is changing.</param>
        public void OnPropertyChanged(string propertyName)
        {
            if (propertyName != null)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
