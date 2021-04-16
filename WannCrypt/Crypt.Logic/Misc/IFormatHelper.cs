// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="IFormatHelper.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Logic.Misc
{
    using System.Collections.Generic;
    using Crypt.Model.Interfaces;

    /// <summary>
    /// Interface responsible to require the necessary methods and properties in order to have a fully functioning formatting helper.
    /// </summary>
    internal interface IFormatHelper
    {
        /// <summary>
        /// Constant block size of 16 bits.
        /// </summary>
        private const int BlockSize = 16;

        /// <summary>
        /// Copies the encryption key's string value to the decryption's key.
        /// </summary>
        /// <param name="encryptTextObject">Object storing data about a text based encryption.</param>
        /// <param name="decryptTextObject">Object storing data about a text based decryption.</param>
        /// <param name="encryptFileObject">Object storing data about a file based encryption.</param>
        /// <param name="decryptFileObject">Object storing data about a file based decryption.</param>
        public void CopyKeyToDecrypt(IEncryptionObject encryptTextObject, IDecryptionObject decryptTextObject, IEncryptionObject encryptFileObject, IDecryptionObject decryptFileObject);

        /// <summary>
        /// Calculates how many 0's we have to append to the string to be in the desired length (length % 16 = 0).
        /// </summary>
        /// <param name="encryptObject">Object containing the necessary encryption data.</param>
        /// <returns>Return with an array that has the text split up in 16 bit blocks.</returns>
        public byte[] CalculatePadding(IEncryptionObject encryptObject);

        /// <summary>
        /// Calculates how many arrays we need to store the decrypted message in 16 bit blocks.
        /// </summary>
        /// <param name="decryptionObject">Object containing the necessary decryption data.</param>
        /// <returns>Returns with a lsit of arrays containing the decrypted message.</returns>
        public List<byte[]> CalculateArrays(IDecryptionObject decryptionObject);
    }
}
