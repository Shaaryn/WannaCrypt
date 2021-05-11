// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="SecretConversationViewModel.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.App.ViewModel.DH
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using Crypt.Logic.AES;
    using Crypt.Logic.DH;
    using Crypt.Model.DH;
    using Crypt.Model.Enum;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    /// <summary>
    /// ViewModel for the DH-AES Secret Conversation page.
    /// </summary>
    internal class SecretConversationViewModel : ViewModelBase, INotifyPropertyChanged
    {
        /// <summary>
        /// Property that helps to display the available encryption types (<see cref="EncryptionSize"/>).
        /// </summary>
        public static Array EncryptSizes
        {
            get { return Enum.GetValues(typeof(EncryptionSize)); }
        }

        private CryptServiceDH serviceDH;
        private CryptServiceAES serviceAES;

        private EncryptionSize cryptSize;

        private PrivateSpaceObject rightPartner;
        private PrivateSpaceObject leftPartner;
        private PublicSpaceObject publicSpace;

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
                this.rightPartner.Size = value;
                this.leftPartner.Size = value;
            }
        }

        /// <summary>
        /// Object storing data about the right-side partner.
        /// </summary>
        public PrivateSpaceObject RightPartner
        {
            get { return rightPartner; }
            set { rightPartner = value; }
        }

        /// <summary>
        /// Object storing data about the left-side partner.
        /// </summary>
        public PrivateSpaceObject LeftPartner
        {
            get { return leftPartner; }
            set { leftPartner = value; }
        }

        /// <summary>
        /// Object storing data about the public space.
        /// </summary>
        public PublicSpaceObject PublicSpace
        {
            get { return publicSpace; }
            set { publicSpace = value; }
        }

        /// <summary>
        /// Command that enables the user to generate a random key for the right-side partner.
        /// </summary>
        public ICommand GenerateKeyForRightPartner { get; set; }

        /// <summary>
        /// Command that enables the user to generate a public key for the right-side partner.
        /// </summary>
        public ICommand GeneratePublicKeyForRightPartner { get; set; }

        /// <summary>
        /// Command that enables the user to assemble a full key.
        /// </summary>
        public ICommand AssembleKeyForRightPartner { get; set; }

        /// <summary>
        /// Command that enables the user to generate a random key for the left-side partner.
        /// </summary>
        public ICommand GenerateKeyForLeftPartner { get; set; }

        /// <summary>
        /// Command that enables the user to generate a public key for the left-side partner.
        /// </summary>
        public ICommand GeneratePublicKeyForLeftPartner { get; set; }

        /// <summary>
        /// Command that enables the user to assemble a full key.
        /// </summary>
        public ICommand AssembleKeyForLeftPartner { get; set; }

        /// <summary>
        /// Command that enables the user to send a message from the right side to the left side.
        /// </summary>
        public ICommand SendMessageFromRightToLeft { get; set; }

        /// <summary>
        /// Command that enables the user to receive a message from the public space to the left side.
        /// </summary>
        public ICommand ReceiveMessageFromRightToLeft { get; set; }

        /// <summary>
        /// Command that enables the user to send a message from the left side to the right side.
        /// </summary>
        public ICommand SendMessageFromLeftToRight { get; set; }

        /// <summary>
        /// Command that enables the user to receive a message from the public space to the rightside.
        /// </summary>
        public ICommand ReceiveMessageFromLeftToRight { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretConversationViewModel"/> class.
        /// </summary>
        public SecretConversationViewModel()
        {
            // Proeprties
            this.publicSpace = new PublicSpaceObject();
            this.rightPartner = new PrivateSpaceObject() { PartnerID = PartnerSide.Right };
            this.leftPartner = new PrivateSpaceObject() { PartnerID = PartnerSide.Left };

            // Services
            this.serviceDH = new CryptServiceDH(this.publicSpace);
            this.serviceAES = new CryptServiceAES();

            // Command groups
            RelayGenerateRandomKeyCommand();
            RelayGeneratePublicKeyCommand();
            RelayAssembleKeysCommand();

            RelayEncryptionCommand();
            RelayDecryptionCommand();
        }

        private void RelayGenerateRandomKeyCommand()
        {
            this.GenerateKeyForLeftPartner = new RelayCommand(() =>
            {
                (this.LeftPartner.PrivateKey, this.LeftPartner.PrivateKeyString) = this.serviceDH.HelpMeKey.GenerateRandomKey(this.leftPartner.Size);
            });

            this.GenerateKeyForRightPartner = new RelayCommand(() =>
            {
                (this.RightPartner.PrivateKey, this.RightPartner.PrivateKeyString) = this.serviceDH.HelpMeKey.GenerateRandomKey(this.rightPartner.Size);
            });
        }

        private void RelayGeneratePublicKeyCommand()
        {
            this.GeneratePublicKeyForLeftPartner = new RelayCommand(() =>
            {
                this.serviceDH.GeneratePublicKeyForParty(this.LeftPartner);
            });

            this.GeneratePublicKeyForRightPartner = new RelayCommand(() =>
            {
                this.serviceDH.GeneratePublicKeyForParty(this.RightPartner);
            });
        }

        private void RelayAssembleKeysCommand()
        {
            this.AssembleKeyForRightPartner = new RelayCommand(() =>
            {
                this.serviceDH.CombineFullKeyForParty(this.RightPartner);
            });

            this.AssembleKeyForLeftPartner = new RelayCommand(() =>
            {
                this.serviceDH.CombineFullKeyForParty(this.LeftPartner);
            });
        }

        private void RelayEncryptionCommand()
        {
            this.SendMessageFromLeftToRight = new RelayCommand(() =>
            {
                this.serviceAES.ExecuteTextEncryption(this.leftPartner);
                this.publicSpace.LeftPartyPublicMessageByte = this.leftPartner.EncryptedMessage;
                this.publicSpace.LeftPartyPublicMessage = this.leftPartner.EncryptedMessageString;
            });

            this.SendMessageFromRightToLeft = new RelayCommand(() =>
            {
                this.serviceAES.ExecuteTextEncryption(this.RightPartner);
                this.PublicSpace.RightPartyPublicMessageByte = this.RightPartner.EncryptedMessage;
                this.PublicSpace.RightPartyPublicMessage = this.RightPartner.EncryptedMessageString;
            });
        }

        private void RelayDecryptionCommand()
        {
            this.ReceiveMessageFromLeftToRight = new RelayCommand(() =>
            {
                this.serviceAES.ExecuteDHTextDecryption(this.PublicSpace, this.RightPartner);
            });

            this.ReceiveMessageFromRightToLeft = new RelayCommand(() =>
            {
                this.serviceAES.ExecuteDHTextDecryption(this.PublicSpace, this.LeftPartner);
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
