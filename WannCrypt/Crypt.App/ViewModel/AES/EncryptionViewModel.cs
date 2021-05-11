// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="EncryptionViewModel.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.App.ViewModel.AES
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using Crypt.Logic;
    using Crypt.Logic.AES;
    using Crypt.Model;
    using Crypt.Model.AES;
    using Crypt.Model.Enum;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    /// <summary>
    /// ViewModel specifically for encrypting a text message or a file.
    /// </summary>
    internal class EncryptionViewModel : ViewModelBase, INotifyPropertyChanged
    {
        /// <summary>
        /// Property that helps to display the available encryption types (<see cref="EncryptionSize"/>).
        /// </summary>
        public static Array EncryptSizes
        {
            get { return Enum.GetValues(typeof(EncryptionSize)); }
        }

        private CryptServiceAES service;
        private EncryptionSize cryptSize;

        private double fileEncryptionProgressValue = 0;
        private bool isFileEncryption;
        private bool isBrowseEnable = true;
        private string keyStringEncryption;

        private TextEncryptionObject encryptTextObject;
        private FileEncryptionObject encryptFileObject;

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
                this.encryptTextObject.Size = value;
                this.encryptFileObject.Size = value;
            }
        }

        /// <summary>
        /// Represents the progress of the file encryption process.
        /// </summary>
        public double FileEncryptionProgressValue
        {
            get
            {
                return fileEncryptionProgressValue;
            }

            set
            {
                fileEncryptionProgressValue = value;
                OnPropertyChanged(nameof(FileEncryptionProgressValue));
            }
        }

        /// <summary>
        /// Defines whether we are encrypting a text message or a file.
        /// </summary>
        public bool IsFileEncryption
        {
            get { return isFileEncryption; }
            set { isFileEncryption = value; }
        }

        /// <summary>
        /// Defines whether the browse button is active or not.
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
        /// Displays the currently used encryption key.
        /// </summary>
        public string KeyStringEncryption
        {
            get
            {
                return keyStringEncryption;
            }

            set
            {
                keyStringEncryption = value;
                OnPropertyChanged(nameof(KeyStringEncryption));
                this.encryptTextObject.KeyString = value;
                this.encryptFileObject.KeyString = value;
            }
        }

        /// <summary>
        /// Object storing data about a text based encryption.
        /// </summary>
        public TextEncryptionObject EncryptTextObject
        {
            get
            {
                return encryptTextObject;
            }

            set
            {
                encryptTextObject = value;
                OnPropertyChanged(nameof(encryptTextObject));
            }
        }

        /// <summary>
        /// Object storing data about a file based encryption.
        /// </summary>
        public FileEncryptionObject EncryptFileObject
        {
            get
            {
                return encryptFileObject;
            }

            set
            {
                encryptFileObject = value;
                OnPropertyChanged(nameof(EncryptFileObject));
            }
        }

        /// <summary>
        /// Command that enables the user to generate a random key.
        /// </summary>
        public ICommand GenerateKeyCommand { get; set; }

        /// <summary>
        /// Command that enables the user to encrypt a file or a text message.
        /// </summary>
        public ICommand EncryptCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EncryptionViewModel"/> class.
        /// </summary>
        public EncryptionViewModel()
        {
            this.service = new CryptServiceAES();

            this.encryptTextObject = new TextEncryptionObject();
            this.encryptFileObject = new FileEncryptionObject();

            RelayEncryptionCommand();
            RelayGenerateKeyCommand();
        }

        private void RelayGenerateKeyCommand()
        {
            this.GenerateKeyCommand = new RelayCommand(() =>
            {
                (this.EncryptTextObject.Key, KeyStringEncryption) = this.service.HelpMeKey.GenerateRandomKey(this.encryptTextObject.Size);
            });
        }

        private void RelayEncryptionCommand()
        {
            this.EncryptCommand = new RelayCommand(async () =>
            {
                if (!IsFileEncryption)
                {
                    this.service.ExecuteTextEncryption(this.EncryptTextObject);
                }
                else
                {
                    IsBrowseEnable = false;

                    Progress<ProgressObject> progress = new Progress<ProgressObject>();
                    progress.ProgressChanged += OnEncryptionProgressChanged;

                    await this.service.ExecuteFileEncryptionAsync(progress, this.EncryptFileObject);
                }
            });
        }

        private void OnEncryptionProgressChanged(object sender, ProgressObject e)
        {
            if (e.CurrentProgress == 99)
            {
                // Otherwise we would never reach 100% because exact 100% is just returning from the function.
                FileEncryptionProgressValue = 100;
                IsBrowseEnable = true;
            }
            else
            {
                FileEncryptionProgressValue = e.CurrentProgress;
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
