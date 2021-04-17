// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="DecryptionViewModel.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.App.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using Crypt.Logic;
    using Crypt.Model;
    using Crypt.Model.Enum;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    /// <summary>
    /// ViewModel specifically for decrypting a text message or a file.
    /// </summary>
    internal class DecryptionViewModel : ViewModelBase, INotifyPropertyChanged
    {
        /// <summary>
        /// Property that helps to display the available encryption types (<see cref="EncryptionSize"/>).
        /// </summary>
        public static Array EncryptSizes
        {
            get { return Enum.GetValues(typeof(EncryptionSize)); }
        }

        private CryptService service;
        private EncryptionSize cryptSize;

        private double fileDecryptionProgressValue = 0;
        private bool isFileDecryption;
        private bool isBrowseEnable = true;
        private string keyStringDecryption;

        private TextDecryptionObject decryptTextObject;
        private FileDecryptionObject decryptFileObject;

        /// <summary>
        /// Defines the <see cref="EncryptionSize"/> based on the UI. Determines the correspondingfield for each encrypt and decrypt object.
        /// </summary>
        public EncryptionSize CryptSize
        {
            get
            {
                return cryptSize;
            }

            set
            {
                cryptSize = value;
                OnPropertyChanged(nameof(CryptSize));
                this.decryptTextObject.Size = value;
                this.decryptFileObject.Size = value;
            }
        }

        /// <summary>
        /// Represents the progress of the file decryption process.
        /// </summary>
        public double FileDecryptionProgressValue
        {
            get
            {
                return fileDecryptionProgressValue;
            }

            set
            {
                fileDecryptionProgressValue = value;
                OnPropertyChanged(nameof(FileDecryptionProgressValue));
            }
        }

        /// <summary>
        /// Defines whether we are decrypting a text message or a file.
        /// </summary>
        public bool IsFileDecryption
        {
            get { return isFileDecryption; }
            set { isFileDecryption = value; }
        }

        /// <summary>
        /// Defines whether the brose button is active or not.
        /// </summary>
        public bool IsBrowseEnable
        {
            get
            {
                return isBrowseEnable;
            }

            set
            {
                isBrowseEnable = value;
                OnPropertyChanged(nameof(IsBrowseEnable));
            }
        }

        /// <summary>
        /// Displayes the currently used decryption key.
        /// </summary>
        public string KeyStringDecryption
        {
            get
            {
                return keyStringDecryption;
            }

            set
            {
                keyStringDecryption = value;
                OnPropertyChanged(nameof(KeyStringDecryption));
                this.decryptTextObject.KeyString = value;
                this.decryptFileObject.KeyString = value;
            }
        }

        /// <summary>
        /// Object storing data about a text based decryption.
        /// </summary>
        public TextDecryptionObject DecryptTextObject
        {
            get
            {
                return decryptTextObject;
            }

            set
            {
                decryptTextObject = value;
                OnPropertyChanged(nameof(decryptTextObject));
            }
        }

        /// <summary>
        /// Object storing data about a file based decryption.
        /// </summary>
        public FileDecryptionObject DecryptFileObject
        {
            get
            {
                return decryptFileObject;
            }

            set
            {
                decryptFileObject = value;
                OnPropertyChanged(nameof(DecryptFileObject));
            }
        }

        /// <summary>
        /// Command that enables the user to decrypt a file or a textr message.
        /// </summary>
        public ICommand DecryptCommand { get; set; }

        /// <summary>
        /// Command that enables the user to copy and use the encryption key as the decryption key.
        /// </summary>
        public ICommand CopyKeyCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DecryptionViewModel"/> class.
        /// </summary>
        public DecryptionViewModel()
        {
            this.service = new CryptService();

            this.decryptTextObject = new TextDecryptionObject();
            this.decryptFileObject = new FileDecryptionObject();

            RelayDecryptionCommand();
        }

        private void RelayDecryptionCommand()
        {
            this.DecryptCommand = new RelayCommand(async () =>
            {
                if (!IsFileDecryption)
                {
                    this.service.ExecuteTextDecryption(this.DecryptTextObject);
                }
                else
                {
                    IsBrowseEnable = false;

                    Progress<ProgressModel> progress = new Progress<ProgressModel>();
                    progress.ProgressChanged += OnDecryptionProgressChanged;

                    await this.service.ExecuteFileDecryptionAsync(progress, this.DecryptFileObject);
                }
            });
        }

        private void OnDecryptionProgressChanged(object sender, ProgressModel e)
        {
            if (e.CurrentProgress == 99)
            {
                // Otherwise we would never reach 100% because exact 100% is just returning from the function.
                FileDecryptionProgressValue = 100;
                IsBrowseEnable = true;
            }
            else
            {
                FileDecryptionProgressValue = e.CurrentProgress;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public new event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Method which will be used to refresh the UI.
        /// </summary>
        /// <param name="propertyName">Name of the proeprty that is changing.</param>
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
