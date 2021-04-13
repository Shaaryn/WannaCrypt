// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="EncryptionSize.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Model.TextModels.Enum
{
    /// <summary>
    /// Enum determining the key size of the selected encryption type.
    /// </summary>
    public enum EncryptionSize
    {
        /// <summary>
        /// It takes a 16 bits long HEX key to encrypt / decrypt a message in a 128 bits encryption.
        /// </summary>
        bits128 = 16,

        /// <summary>
        /// It takes a 24 bits long HEX key to encrypt / decrypt a message in a 192 bits encryption.
        /// </summary>
        bits192 = 24,

        /// <summary>
        /// It takes a 32 bits long HEX key to encrypt / decrypt a message in a 256 bits encryption.
        /// </summary>
        bits256 = 32,
    }
}
