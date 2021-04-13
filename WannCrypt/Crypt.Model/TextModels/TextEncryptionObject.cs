// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="TextEncryptionObject.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Model.TextModels
{
    using System.ComponentModel;
    using Crypt.Model.TextModels.Enum;
    using Crypt.Model.TextModels.Interfaces;

    /// <summary>
    /// Object that is responsible of storing the data regarding a text that is being encrypted.
    /// </summary>
    public sealed class TextEncryptionObject : IAESObject
    {
        private string keyString;
        private byte[] key;
        private byte[] expandedKey;

        private string message;
        private byte[] messageByte;
        private byte[] paddedMessage;
        private string encryptedMessageString;
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
        /// Message that is extended to have a (length % 16 == 0). Not used places are filled with 0s.
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
        /// <param name="propertyName">Name of the proeprty that is changing.</param>
        public void OnPropertyChanged(string propertyName)
        {
            if (propertyName != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
