// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="KeyHelper.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Logic.Misc
{
    using System;
    using Crypt.Model;
    using Crypt.Model.TextModels;
    using Crypt.Model.TextModels.Enum;
    using Crypt.Model.TextModels.Interfaces;

    /// <summary>
    /// Class responsible for the key generation and expansion.
    /// </summary>
    public class KeyHelper
    {
        private static Random rnd = new Random();

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="aesObject">Object containing the necessary data for the key.</param>
        /// <returns>Returns with the expanded key. Its length is determiend by the <see cref="EncryptionSize"/>.</returns>
        public byte[] KeyExpansion(IAESObject aesObject)
        {
            // 128 -> 10 * 16 +16
            // 10 : We need a 16 bytes key for each round
            // 16(at the end): we add the original key as well.
            int keySize = (((int)aesObject.Round + 1) * 16) + 16;

            byte[] expandedKey = new byte[keySize];

            // Copy the original key as it is.
            for (int i = 0; i < aesObject.Key.Length; i++)
            {
                expandedKey[i] = aesObject.Key[i];
            }

            int generatedBytes = aesObject.Key.Length;
            int rConIndex = 1;
            byte[] tmpCore = new byte[4];

            do
            {
                // Read the recently generated 4 bytes for the core.
                for (int i = 0; i < tmpCore.Length; i++)
                {
                    tmpCore[i] = expandedKey[i + generatedBytes - 4];
                }

                // Perform the core once for each key.Length()-th byte key.
                if (generatedBytes % aesObject.Key.Length == 0)
                {
                    KeyExpansionCore(tmpCore, rConIndex);
                    rConIndex++;
                }
                else if (aesObject.Key.Length == 32 && generatedBytes % aesObject.Key.Length == 16)
                {
                    tmpCore[0] = Tables.SBox[tmpCore[0]];
                    tmpCore[1] = Tables.SBox[tmpCore[1]];
                    tmpCore[2] = Tables.SBox[tmpCore[2]];
                    tmpCore[3] = Tables.SBox[tmpCore[3]];
                }

                for (int i = 0; i < tmpCore.Length; i++)
                {
                    expandedKey[generatedBytes] = (byte)(expandedKey[generatedBytes - aesObject.Key.Length] ^ tmpCore[i]);
                    generatedBytes++;
                }
            }
            while (generatedBytes < keySize);

            return expandedKey;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="inputBits">Bits that are curently being examined and manipulated.</param>
        /// <param name="index">Index of the current RCon iteration.</param>
        public void KeyExpansionCore(byte[] inputBits, int index)
        {
            // Left Rotation
            byte tmp = inputBits[0];
            inputBits[0] = inputBits[1];
            inputBits[1] = inputBits[2];
            inputBits[2] = inputBits[3];
            inputBits[3] = tmp;

            // SBox lookup
            inputBits[0] = Tables.SBox[inputBits[0]];
            inputBits[1] = Tables.SBox[inputBits[1]];
            inputBits[2] = Tables.SBox[inputBits[2]];
            inputBits[3] = Tables.SBox[inputBits[3]];

            // RCon
            inputBits[0] ^= Tables.RCon[index];
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="encryptObject">Object containing the necessary encryption data.</param>
        /// <param name="givenSize">Determines the size of the key.</param>
        public void GenerateRandomKey(TextEncryptionObject encryptObject, EncryptionSize givenSize)
        {
            string allCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789,?;.:-*_<>#&@{}/-+=";

            byte[] generatedKey = new byte[(int)givenSize];

            for (int i = 0; i < (int)givenSize; i++)
            {
                generatedKey[i] = Convert.ToByte(allCharacters[rnd.Next(0, allCharacters.Length - 1)]);
            }

            encryptObject.Key = generatedKey;

            encryptObject.KeyString = BitConverter.ToString(generatedKey);
        }
    }
}
