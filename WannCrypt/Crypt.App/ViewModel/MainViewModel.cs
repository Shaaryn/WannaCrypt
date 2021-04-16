// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="MainViewModel.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.ViewModel
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
    /// ViewModel representing the data in the window.
    /// </summary>
    internal class MainViewModel : ViewModelBase, INotifyPropertyChanged
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

        private bool isFileEncryption;
        private bool isFileDecryption;
        private string keyStringEncryption;
        private string keyStringDecryption;

        private TextEncryptionObject encryptTextObject;
        private TextDecryptionObject decryptTextObject;

        private FileEncryptionObject encryptFileObject;
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
                this.encryptTextObject.Size = value;
                this.decryptTextObject.Size = value;
                this.encryptFileObject.Size = value;
                this.decryptFileObject.Size = value;
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
        /// Defines whether we are decrypting a text message or a file.
        /// </summary>
        public bool IsFileDecryption
        {
            get { return isFileDecryption; }
            set { isFileDecryption = value; }
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
        /// Command that enables the user to generate a random key.
        /// </summary>
        public ICommand GenerateKeyCommand { get; set; }

        /// <summary>
        /// Command that enables the user to encrypt a file or a text message.
        /// </summary>
        public ICommand EncryptCommand { get; set; }

        /// <summary>
        /// Command that enables the user to decrypt a file or a textr message.
        /// </summary>
        public ICommand DecryptCommand { get; set; }

        /// <summary>
        /// Command that enables the user to copy and use the encryption key as the decryption key.
        /// </summary>
        public ICommand CopyKeyCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
            service = new CryptService();

            encryptTextObject = new TextEncryptionObject();
            decryptTextObject = new TextDecryptionObject();
            encryptFileObject = new FileEncryptionObject();
            decryptFileObject = new FileDecryptionObject();
            CryptSize = default;

            RelayGenerateKeyCommand();
            RelayCopyKeyCommand();
            RelayEncryptionCommand();
            RelayDecryptionCommand();
        }

        private void RelayGenerateKeyCommand()
        {
            this.GenerateKeyCommand = new RelayCommand(() =>
            {
                this.service.HelpMeKey.GenerateRandomKey(this.EncryptTextObject, this.encryptFileObject, this.encryptTextObject.Size);
                if (this.encryptTextObject.KeyString != this.keyStringEncryption || this.encryptFileObject.KeyString != this.keyStringEncryption)
                {
                    KeyStringEncryption = this.encryptTextObject.KeyString;
                }
            });
        }

        private void RelayEncryptionCommand()
        {
            this.EncryptCommand = new RelayCommand(() =>
            {
                if (!IsFileEncryption)
                {
                    this.service.ExecuteTextEncryption(this.EncryptTextObject);
                }
                else
                {
                    this.service.ExecuteFileEncryption(this.EncryptFileObject);
                }
            });
        }

        private void RelayDecryptionCommand()
        {
            this.DecryptCommand = new RelayCommand(() =>
            {
                if (!IsFileEncryption)
                {
                    this.service.ExecuteTextDecryption(this.DecryptTextObject);
                }
                else
                {
                    this.service.ExecuteFileDecryption(this.DecryptFileObject);
                }
            });
        }

        private void RelayCopyKeyCommand()
        {
            this.CopyKeyCommand = new RelayCommand(() =>
            {
                    this.service.HelpMeFormat.CopyKeyToDecrypt(this.encryptTextObject, this.decryptTextObject, this.encryptFileObject, this.decryptFileObject);
                    if (this.decryptTextObject.KeyString != this.keyStringDecryption || this.decryptFileObject.KeyString != this.keyStringDecryption)
                    {
                        KeyStringDecryption = this.decryptTextObject.KeyString;
                    }
            });
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
