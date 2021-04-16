// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="IEncryptionObject.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Model.Interfaces
{
    /// <summary>
    /// Represents an <see cref="IAESObject"/> that is meant to be encrypted.
    /// </summary>
    public interface IEncryptionObject : IAESObject
    {
        /// <summary>
        /// The message that ia already extended with palceholder 0s in order to have a compatible length (length % 16 == 0).
        /// </summary>
        public byte[] PaddedMessage { get; set; }
    }
}
