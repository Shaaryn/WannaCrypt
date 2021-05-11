// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="PrivateSpaceObject.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Model.DH
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Crypt.Model.Enum;
    using Crypt.Model.Interfaces;

    /// <summary>
    /// Model that is responsible of storing the data regarding a text message that is being encrypted, decrypted and transmitted.
    /// It extends the <see cref="IAESObject"/> interface in a way that it can hold further information regarding DH key exchange.
    /// </summary>
    public class PrivateSpaceObject : IDHObject
    {
        private PartnerSide partnerID;

        private string privateKeyString;
        private string keyString;
        private byte[] privateKey;
        private byte[] key;
        private byte[] expandedKey;

        private string message;
        private byte[] messageByte;
        private byte[] paddedMessage;
        private string encryptedMessageString;
        private List<byte[]> encryptedPaddedMessage;
        private byte[] encryptedMessage;
        private string receivedMessage;
        private byte[] receivedMessageByte;

        private EncryptionSize size;
        private RoundSize round;

        /// <summary>
        /// <inheritdoc/>.
        /// </summary>
        public PartnerSide PartnerID
        {
            get { return this.partnerID; }
            set { this.partnerID = value; }
        }

        /// <summary>
        /// <inheritdoc/>.
        /// </summary>
        public string PrivateKeyString
        {
            get
            {
                return this.privateKeyString;
            }

            set
            {
                this.privateKeyString = value;
                this.OnPropertyChanged(nameof(this.PrivateKeyString));
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string KeyString
        {
            get
            {
                return this.keyString;
            }

            set
            {
                this.keyString = value;
                this.OnPropertyChanged(nameof(this.KeyString));
            }
        }

        /// <summary>
        /// <inheritdoc/>.
        /// </summary>
        public byte[] PrivateKey
        {
            get
            {
                return this.privateKey;
            }

            set
            {
                this.privateKey = value;
                this.OnPropertyChanged(nameof(this.PrivateKey));
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public byte[] Key
        {
            get { return this.key; }
            set { this.key = value; }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public byte[] ExpandedKey
        {
            get { return this.expandedKey; }
            set { this.expandedKey = value; }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Message
        {
            get
            {
                return this.message;
            }

            set
            {
                this.message = value;
                this.OnPropertyChanged(nameof(this.Message));
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public byte[] MessageByte
        {
            get { return this.messageByte; }
            set { this.messageByte = value; }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public byte[] PaddedMessage
        {
            get { return this.paddedMessage; }
            set { this.paddedMessage = value; }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string EncryptedMessageString
        {
            get
            {
                return this.encryptedMessageString;
            }

            set
            {
                this.encryptedMessageString = value;
                this.OnPropertyChanged(nameof(this.EncryptedMessageString));
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<byte[]> EncryptedPaddedMessage
        {
            get { return this.encryptedPaddedMessage; }
            set { this.encryptedPaddedMessage = value; }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public byte[] EncryptedMessage
        {
            get { return this.encryptedMessage; }
            set { this.encryptedMessage = value; }
        }

        /// <summary>
        /// <inheritdoc/>.
        /// </summary>
        public string ReceivedMessage
        {
            get
            {
                return this.receivedMessage;
            }

            set
            {
                this.receivedMessage = value;
                this.OnPropertyChanged(nameof(this.ReceivedMessage));
            }
        }

        /// <summary>
        /// <inheritdoc/>.
        /// </summary>
        public byte[] ReceivedMessageByte
        {
            get
            {
                return this.receivedMessageByte;
            }

            set
            {
                this.receivedMessageByte = value;
                this.OnPropertyChanged(nameof(this.receivedMessageByte));
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public EncryptionSize Size
        {
            get { return this.size; }
            set { this.size = value; }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public RoundSize Round
        {
            get { return this.round; }
            set { this.round = value; }
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
