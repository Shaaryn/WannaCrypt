// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="FormatHelper.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Logic.Misc
{
    using System;
    using System.Collections.Generic;
    using Crypt.Model.AES;
    using Crypt.Model.Enum;
    using Crypt.Model.Interfaces;

    /// <summary>
    /// Class responsible for formatting and arranging oeprations mostly.
    /// </summary>
    public class FormatHelper : IFormatHelper
    {
        private const int BlockSize = 16;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="encryptTextObject">Object storing data about a text based encryption.</param>
        /// <param name="decryptTextObject">Object storing data about a text based decryption.</param>
        /// <param name="encryptFileObject">Object storing data about a file based encryption.</param>
        /// <param name="decryptFileObject">Object storing data about a file based decryption.</param>
        public void CopyKeyToDecrypt(IEncryptionObject encryptTextObject, IDecryptionObject decryptTextObject, IEncryptionObject encryptFileObject, IDecryptionObject decryptFileObject)
        {
            decryptTextObject.KeyString = encryptTextObject.KeyString;
            decryptFileObject.KeyString = encryptFileObject.KeyString;

            return;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="encryptObject">Object containing the necessary encryption data.</param>
        /// <returns>Return with an array that has the text split up in 16 bit blocks.</returns>
        public byte[] CalculatePadding(IEncryptionObject encryptObject)
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
            else
            {
                return encryptObject.MessageByte;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="encryptedMessageString">String representation of the message we want to split into arrays.</param>
        /// <param name="decryptionObject">Object containing the necessary decryption data.</param>
        /// <returns>Returns with a lsit of arrays containing the decrypted message.</returns>
        public List<byte[]> CalculateArrays(string encryptedMessageString, IDecryptionObject decryptionObject = null)
        {
            List<byte[]> tmpList = new List<byte[]>();
            byte[] tmpCipher = new byte[BlockSize];

            if (decryptionObject is null && encryptedMessageString is not null)
            {
                // Since every 3rd character is a hypen ('-') and we dont need them.
                // The first has, but the last one does not have a corresponding hypen, hence
                // the solution is always res = x.66666, we only need x that determines the number of hypens we have.
                // netTotalLength = grossTotalLength - Convert.ToInt32(Math.Floor((double)(grossTotalLength / 3)));
                int n = 0;
                for (int i = 0; i < encryptedMessageString.Length; i += 3)
                {
                    tmpCipher[n] = Convert.ToByte(encryptedMessageString.Substring(i, 2), 16);
                    n++;

                    if (n % BlockSize == 0)
                    {
                        tmpList.Add(tmpCipher);
                        n = 0;

                        // Avoiding outer variable trap.
                        tmpCipher = new byte[BlockSize];
                    }
                }
            }
            else if (decryptionObject is TextDecryptionObject)
            {
                // Since every 3rd character is a hypen ('-') and we dont need them.
                // The first has, but the last one does not have a corresponding hypen, hence
                // the solution is always res = x.66666, we only need x that determines the number of hypens we have.
                // netTotalLength = grossTotalLength - Convert.ToInt32(Math.Floor((double)(grossTotalLength / 3)));
                int n = 0;
                for (int i = 0; i < decryptionObject.EncryptedMessageString.Length; i += 3)
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
            }
            else if (decryptionObject is FileDecryptionObject)
            {
                int n = 0;
                for (int i = 0; i < decryptionObject.MessageByte.Length; i++)
                {
                    tmpCipher[n] = decryptionObject.MessageByte[i];
                    n++;

                    if (n % BlockSize == 0)
                    {
                        tmpList.Add(tmpCipher);
                        n = 0;

                        // Avoiding outer variable trap.
                        tmpCipher = new byte[BlockSize];
                    }
                }
            }

            return tmpList;
        }

        /// <summary>
        /// Converts the input string into an array of bytes.
        /// </summary>
        /// <param name="input">String formatted as HEX.</param>
        /// <param name="size">Size of the currently used key.</param>
        /// <returns>Array of bytes.</returns>
        public byte[] ConvertHexToByteArray(string input, EncryptionSize size)
        {
            byte[] tmp = new byte[(int)size];
            int n = 0;

            for (int i = 0; i < input.Length; i += 3)
            {
                tmp[n] = Convert.ToByte(input.Substring(i, 2), 16);
                n++;
            }

            return tmp;
        }

        /// <summary>
        /// Converts the input byte array into a formatted string(HEX).
        /// </summary>
        /// <param name="input">Array of bytes that needs to be converted.</param>
        /// <returns>Formatted string(HEX).</returns>
        public string ConvertByteArrayToHex(byte[] input)
        {
            return BitConverter.ToString(input);
        }
    }
}
