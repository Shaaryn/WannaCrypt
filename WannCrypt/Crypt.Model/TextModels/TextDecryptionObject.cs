// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="TextDecryptionObject.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Model.TextModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Crypt.Model.TextModels.Enum;
    using Crypt.Model.TextModels.Interfaces;

    /// <summary>
    /// Model that is responsible of storing the data regarding a text that is being decrypted.
    /// </summary>
    public sealed class TextDecryptionObject : IAESObject
    {
        private string keyString;
        private byte[] key;
        private byte[] expandedKey;

        private string message;
        private byte[] messageByte;
        private string encryptedMessageString;
        private List<byte[]> encryptedMessageCiphers;
        private byte[] encryptedMessage;

        private EncryptionSize size;
        private RoundSize round;

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
        /// Used to store the encrypted message in blocksized(.Length = 16) arrays.
        /// </summary>
        public List<byte[]> EncryptedMessageCiphers
        {
            get { return this.encryptedMessageCiphers; }
            set { this.encryptedMessageCiphers = value; }
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

        // UI configuration

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
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
