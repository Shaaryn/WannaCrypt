// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="IAESObject.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Model.Interfaces
{
    using System.ComponentModel;
    using Crypt.Model.Enum;

    /// <summary>
    /// Represents an AES encryption compatible object.
    /// </summary>
    public interface IAESObject : INotifyPropertyChanged
    {
        // Key properties

        /// <summary>
        /// String representation of the hey key that will be used to encryp / decrypt the message.
        /// </summary>
        public string KeyString { get; set; }

        /// <summary>
        /// Byte representation of the key that is being used.
        /// </summary>
        public byte[] Key { get; set; }

        /// <summary>
        /// Expanded key generated from the Key property.
        /// </summary>
        public byte[] ExpandedKey { get; set; }

        // Data properties

        /// <summary>
        /// String message which will be encrypted or the result of the decryption.
        /// Suitable for human eyes.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Byte representation of the Message.
        /// Not that suitable for human eyes.
        /// </summary>
        public byte[] MessageByte { get; set; }

        /// <summary>
        /// String representation of the gibberish encrypted message.
        /// Not that suitable for human eyes.
        /// </summary>
        public string EncryptedMessageString { get; set; }

        /// <summary>
        /// Encrypted message represented as bytes.
        /// Definitely not for human eyes.
        /// </summary>
        public byte[] EncryptedMessage { get; set; }

        // Configuration properties

        /// <summary>
        /// Size of the encryption, can be 128, 192 or 256.
        /// </summary>
        public EncryptionSize Size { get; set; }

        /// <summary>
        /// Defines the rounds the encryption will evaluate, can be 9, 11 or 13.
        /// Determiend by the Size property.
        /// </summary>
        public RoundSize Round { get; set; }

        /// <summary>
        /// Method which will be used to refresh the UI.
        /// </summary>
        /// <param name="propertyName">Name of the proeprty that is changing.</param>
        void OnPropertyChanged(string propertyName);
    }
}
