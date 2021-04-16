// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="IKeyHelper.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Logic.Misc
{
    using Crypt.Model.Enum;
    using Crypt.Model.Interfaces;

    /// <summary>
    /// Interface responsible to require the necessary methods and properties in order to have a fully functioning key handler helper.
    /// </summary>
    internal interface IKeyHelper
    {
        /// <summary>
        /// Performs the key expansion logic based on the key of the <see cref="IAESObject"/> provided.
        /// </summary>
        /// <param name="aesObject">Object containing the necessary data for the key.</param>
        /// <returns>Returns with the expanded key. Its length is determiend by the <see cref="EncryptionSize"/>.</returns>
        public byte[] KeyExpansion(IAESObject aesObject);

        /// <summary>
        /// Core operations (repetative operations) that the are necessary to expand a key.
        /// </summary>
        /// <param name="inputBits">Bits that are curently being examined and manipulated.</param>
        /// <param name="index">Index of the current RCon iteration.</param>
        public void KeyExpansionCore(byte[] inputBits, int index);

        /// <summary>
        /// Generates a random HEX key based on the hard-typed character pool.
        /// </summary>
        /// <param name="encryptObject">Object containing the necessary encryption data.</param>
        /// <param name="givenSize">Determines the size of the key.</param>
        public void GenerateRandomKey(IAESObject encryptObject, EncryptionSize givenSize);
    }
}
