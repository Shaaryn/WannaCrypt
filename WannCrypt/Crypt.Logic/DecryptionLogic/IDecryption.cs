// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="IDecryption.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Logic.DecryptionLogic
{
    using Crypt.Model.TextModels;

    /// <summary>
    /// Interface responsible to require the necessary methods for decryption.
    /// </summary>
    public interface IDecryption
    {
        /// <summary>
        /// Main method of the encryption process, ensures that the subfunctions are called in order.
        /// </summary>
        /// <param name="decryptObject">Object that contains the necessary data for decryption.</param>
        /// <param name="offset">Offset variable determining the current block that is being decrypted.</param>
        /// <returns>Array of bytes containing the currently decrypted block.</returns>
        byte[] Decrypt(TextDecryptionObject decryptObject, int offset);

        /// <summary>
        /// Method responsible for the 'InvSubBytes' step.
        /// Inverse of the <see cref="Encryption.Encryption.SubBytes"/> function.
        /// </summary>
        void InvSubBytes();

        /// <summary>
        /// Method responsible for the 'InvShiftRows' step.
        /// Inverse of the <see cref="Encryption.Encryption.ShiftRows"/> function.
        /// </summary>
        void InvShiftRows();

        /// <summary>
        /// Method responsible for the 'InvMixColumns' step.
        /// Inverse of the <see cref="Encryption.Encryption.MixColumns"/> function.
        /// </summary>
        void InvMixColumns();

        /// <summary>
        /// Method responsible for the 'AddRoundKey' step.
        /// Adds a part of the predefined (calculated) extended key to the state during each round.
        /// Its logic remains the same during encryption and decryption.
        /// </summary>
        /// <param name="currentKey">Defines the key used for the current round key.</param>
        /// <param name="roundIndex">Defines the offset of the key (block) which will be used.</param>
        void AddRoundKey(byte[] currentKey, int roundIndex);
    }
}
