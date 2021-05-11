// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="IDHObject.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Model.Interfaces
{
    using Crypt.Model.Enum;

    /// <summary>
    /// Represents a DH key exchange compatible object.
    /// </summary>
    public interface IDHObject : IAESObject, IEncryptionObject, IDecryptionObject
    {
        /// <summary>
        /// Field used to identify the partner.
        /// </summary>
        public PartnerSide PartnerID { get; set; }

        /// <summary>
        /// String representation of the hex key that will be used to calculate the full key.
        /// </summary>
        public string PrivateKeyString { get; set; }

        /// <summary>
        /// Byte representation of the key that is being used.
        /// </summary>
        public byte[] PrivateKey { get; set; }

        /// <summary>
        /// String representation of the message from the conversational partner.
        /// Suitable for human eyes.
        /// </summary>
        public string ReceivedMessage { get; set; }

        /// <summary>
        /// Byte representation of the message from the conversational partner.
        /// Not that suitable for human eyes.
        /// </summary>
        public byte[] ReceivedMessageByte { get; set; }
    }
}
