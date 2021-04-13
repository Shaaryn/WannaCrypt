using Crypt.Logic;
using Crypt.Model.TextModels;
using Crypt.Model.TextModels.Enum;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Crypt.ViewModel
{
    class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {

        // Properties and fields

        private CryptService service;
        private EncryptionSize cryptSize;

        private bool isMessage;
        private bool isFile;

        private TextEncryptionObject encryptObject;
        private TextDecryptionObject decryptObject;

        //private FileObject selectedEncryptFile;
        //private FileObject selectedDecryptFile;

        public EncryptionSize CryptSize
        {
            get { return cryptSize; }
            set 
            {
                cryptSize = value;
                OnPropertyChanged(nameof(CryptSize));
                this.encryptObject.Size = value;
                this.decryptObject.Size = value;
            }
        }

        public bool IsMessage
        {
            get { return isMessage; }
            set { isMessage = value; }
        }

        public bool IsFile
        {
            get { return isFile; }
            set { isFile = value; }
        }

        public TextEncryptionObject EncryptObject
        {
            get { return encryptObject; }
            set 
            {
                encryptObject = value;
                OnPropertyChanged(nameof(encryptObject));
            }
        }

        public TextDecryptionObject DecryptObject
        {
            get { return decryptObject; }
            set 
            {
                decryptObject = value;
                OnPropertyChanged(nameof(decryptObject));
            }
        }

        public Array EncryptSizes
        {
            get { return Enum.GetValues(typeof(EncryptionSize)); }
        }

        // Command(s)

        public ICommand GenerateKeyCommand { get; set; }
        public ICommand EncryptCommand { get; set; }
        public ICommand DecryptCommand { get; set; }
        public ICommand CopyKeyCommand { get; set; }

        public MainViewModel()
        {
            if (this.IsInDesignMode)
            {
                //selectedEncryptFile = new FileObject();
                //selectedEncryptFile.FileName = "EncryptionFile.bat";
                //selectedEncryptFile.Path = "RandomPath\\EncryptionFile.bat";

                //selectedDecryptFile = new FileObject();
                //selectedDecryptFile.FileName = "DecryptionFile.bat";
                //selectedDecryptFile.Path = "RandomPath\\DecryptionFile.bat";

                encryptObject = new TextEncryptionObject();
                encryptObject.Message = "Encrypt this message, shall we?";
                encryptObject.Size = EncryptionSize.bits128;
            }

            service = new CryptService();

            //selectedEncryptFile = new FileObject();
            //selectedDecryptFile = new FileObject();
            encryptObject = new TextEncryptionObject();
            decryptObject = new TextDecryptionObject();
            CryptSize = new EncryptionSize();

            this.GenerateKeyCommand = new RelayCommand(() => this.service.HelpMeKey.GenerateRandomKey(this.EncryptObject, this.encryptObject.Size));
            this.EncryptCommand = new RelayCommand(() => this.service.ExecuteEncryption(this.EncryptObject));
            this.DecryptCommand = new RelayCommand(() => this.service.ExecuteDecryption(this.DecryptObject));
            this.CopyKeyCommand = new RelayCommand(() => this.service.HelpMeFormat.CopyKeyToDecrypt(this.encryptObject, this.decryptObject));
        }

        public new event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
