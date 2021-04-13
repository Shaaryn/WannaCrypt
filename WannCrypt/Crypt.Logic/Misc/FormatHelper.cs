// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="FormatHelper.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Logic.Misc
{
    using System;
    using System.Collections.Generic;
    using Crypt.Model.TextModels;
    using Crypt.Model.TextModels.Interfaces;

    /// <summary>
    /// Class responsible for formatting and arranging oeprations mostly.
    /// </summary>
    public class FormatHelper : IFormatHelper
    {
        private const int BlockSize = 16;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="encryptObject">Object that contains the necessary encryption data.</param>
        /// <param name="decryptObject">Object that contains the necessary decryption data.</param>
        public void CopyKeyToDecrypt(IAESObject encryptObject, IAESObject decryptObject)
        {
            decryptObject.KeyString = encryptObject.KeyString;

            return;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="encryptObject">Object containing the necessary encryption data.</param>
        /// <returns>Return with an array that has the text split up in 16 bit blocks.</returns>
        public byte[] CalculatePadding(TextEncryptionObject encryptObject)
        {
            int totalLength = encryptObject.MessageByte.Length;
            byte[] tmp;

            if (totalLength % BlockSize != 0)
            {
                // How many whole 16 bits block we have
                double times16 = (double)totalLength / (double)BlockSize;

                // Round up so we can use it as a terminator.
                int ceilingTimes16 = Convert.ToInt32(Math.Ceiling(times16));

                tmp = new byte[ceilingTimes16 * BlockSize];
                for (int i = 0; i < tmp.Length; i++)
                {
                    if (i < totalLength)
                    {
                        tmp[i] = encryptObject.MessageByte[i];
                    }
                    else
                    {
                        tmp[i] = 0;
                    }
                }

                return tmp;
            }

            return null;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="decryptionObject">Object containing the necessary decryption data.</param>
        /// <returns>Returns with a lsit of arrays containing the decrypted message.</returns>
        public List<byte[]> CalculateArrays(TextDecryptionObject decryptionObject)
        {
            List<byte[]> tmpList = new List<byte[]>();

            int grossTotalLength = decryptionObject.EncryptedMessageString.Length;

            // Since every 3rd character is a hypen ('-') and we dont need them.
            // The first has, but the last one does not have a corresponding hypen, hence
            // the solution is always res = x.66666, we only need x that determines the number of hypens we have.
            int netTotalLength = grossTotalLength - Convert.ToInt32(Math.Floor((double)(grossTotalLength / 3)));
            byte[] tmpCipher = new byte[BlockSize];

            int n = 0;
            for (int i = 0; i < grossTotalLength; i += 3)
            {
                tmpCipher[n] = Convert.ToByte(decryptionObject.EncryptedMessageString.Substring(i, 2), 16);
                n++;

                if (n % BlockSize == 0)
                {
                    tmpList.Add(tmpCipher);
                    n = 0;

                    // Avoiding outer variable trap.
                    tmpCipher = new byte[BlockSize];
                }
            }

            return tmpList;
        }
    }
}
