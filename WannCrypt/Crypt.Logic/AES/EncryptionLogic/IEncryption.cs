// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="IEncryption.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Logic.AES.EncryptionLogic
{
    using Crypt.Model.Interfaces;

    /// <summary>
    /// Interface responsible to require the necessary methods for encryption.
    /// </summary>
    public interface IEncryption
    {
        /// <summary>
        /// Main method of the encryption process, ensures that the subfunctions are called in order.
        /// </summary>
        /// <param name="encryptObject"> Object that contains the necessary data for encryption.</param>
        /// <param name="offset">Offset varaible determining the current block that is being encrypted.</param>
        /// <returns>Array of bytes containing the currently encrypted block.</returns>
        byte[] Encrypt(IEncryptionObject encryptObject, int offset);

        /// <summary>
        /// Method responsible for the 'SubBytes' step.
        /// Simple looks up the provided substitution box and replaces the element with its corresponsing pair.
        /// </summary>
        void SubBytes();

        /// <summary>
        /// Method responsible for the 'ShiftRows' step.
        /// Every row (except the first (0)) is cyclically shifted by a predefined offset value.
        /// </summary>
        void ShiftRows();

        /// <summary>
        /// Method responsible for the 'MixColumns' step.
        /// Every column is treated as a four-term polynomial.
        /// Considering matrix multiplication in Galois fields we simply just XOR the values.
        /// </summary>
        void MixColumns();

        /// <summary>
        /// Method responsible for the 'AddRoundKey' step.
        /// Adds a part of the predefined (calculated) extended key to the state during each round.
        /// </summary>
        /// <param name="currentKey">Defines the key used for the current round key.</param>
        /// <param name="roundIndex">Defines the offset of the key (block) which will be used.</param>
        void AddRoundKey(byte[] currentKey, int roundIndex);
    }
}