// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="IDecryptionObject.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Model.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents an <see cref="IAESObject"/> that is meant to be decrypted.
    /// </summary>
    public interface IDecryptionObject : IAESObject
    {
        /// <summary>
        /// Used to store the encrypted message in blocksized(.Length = 16) arrays.
        /// </summary>
        public List<byte[]> EncryptedPaddedMessage { get; set; }
    }
}
