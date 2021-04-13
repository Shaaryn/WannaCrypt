// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="CryptService.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Crypt.Logic.DecryptionLogic;
    using Crypt.Logic.EncryptionLogic;
    using Crypt.Logic.Misc;
    using Crypt.Model.TextModels;
    using Crypt.Model.TextModels.Enum;
    using Crypt.Model.TextModels.Interfaces;

    /// <summary>
    /// Service provider responsible for the execution and flow control of the encryption and decryption procedures.
    /// </summary>
    public class CryptService : ICryptService
    {
        private const int BlockSize = 16;

        private FormatHelper helpMeFormat;
        private KeyHelper helpMeKey;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public FormatHelper HelpMeFormat
        {
            get { return helpMeFormat; }
            set { helpMeFormat = value; }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public KeyHelper HelpMeKey
        {
            get { return helpMeKey; }
            set { helpMeKey = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CryptService"/> class.
        /// </summary>
        public CryptService()
        {
            helpMeFormat = new FormatHelper();
            helpMeKey = new KeyHelper();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="encryptObject"> Object that contains the necessary data for encryption.</param>
        public void ExecuteEncryption(TextEncryptionObject encryptObject)
        {
            // Define the rounds
            encryptObject.Round = DefineRounds(encryptObject);

            // Bytify the text
            encryptObject.MessageByte = new byte[encryptObject.Message.Length];
            for (int i = 0; i < encryptObject.Message.Length; i++)
            {
                encryptObject.MessageByte[i] = Convert.ToByte(encryptObject.Message[i]);
            }

            // Pad the message
            encryptObject.PaddedMessage = helpMeFormat.CalculatePadding(encryptObject);

            // Calculate the expanded key
            encryptObject.Key = new byte[(int)encryptObject.Size];
            int n = 0;
            if (encryptObject.KeyString != null)
            {
                for (int i = 0; i < encryptObject.KeyString.Length; i += 3)
                {
                    encryptObject.Key[n] = Convert.ToByte(encryptObject.KeyString.Substring(i, 2), 16);
                    n++;
                }
            }

            encryptObject.ExpandedKey = helpMeKey.KeyExpansion(encryptObject);

            // Execute the encryption process for each block
            Encryption encryption = new Encryption();
            byte[] tmpEncryptedMessage = new byte[BlockSize];
            encryptObject.EncryptedMessage = new byte[encryptObject.PaddedMessage.Length];

            for (int i = 0; i < encryptObject.PaddedMessage.Length; i += BlockSize)
            {
                tmpEncryptedMessage = encryption.Encrypt(encryptObject, i);
                for (int j = 0; j < BlockSize; j++)
                {
                    encryptObject.EncryptedMessage[j + i] = tmpEncryptedMessage[j];
                }
            }

            encryptObject.EncryptedMessageString = BitConverter.ToString(encryptObject.EncryptedMessage);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="decryptObject"> Object that contains the necessary data for decryption.</param>
        public void ExecuteDecryption(TextDecryptionObject decryptObject)
        {
            // Defines the rounds
            decryptObject.Round = DefineRounds(decryptObject);

            // Break the message into blocksized arrays
            decryptObject.EncryptedMessageCiphers = new List<byte[]>();
            decryptObject.EncryptedMessageCiphers = helpMeFormat.CalculateArrays(decryptObject);

            // Calculate the expanded key.
            decryptObject.Key = new byte[(int)decryptObject.Size];
            int k = 0;
            for (int i = 0; i < decryptObject.KeyString.Length; i += 3)
            {
                decryptObject.Key[k] = Convert.ToByte(decryptObject.KeyString.Substring(i, 2), 16);
                k++;
            }

            decryptObject.ExpandedKey = helpMeKey.KeyExpansion(decryptObject);

            // Execute the decryption process for each block
            Decryption decryption = new Decryption();
            byte[] tmpDecryptedMessageByte = new byte[BlockSize];
            decryptObject.MessageByte = new byte[BlockSize * decryptObject.EncryptedMessageCiphers.Count];

            for (int i = 0; i < decryptObject.EncryptedMessageCiphers.Count; i++)
            {
                tmpDecryptedMessageByte = decryption.Decrypt(decryptObject, i);
                for (int j = 0; j < BlockSize; j++)
                {
                    decryptObject.MessageByte[(i * BlockSize) + j] = tmpDecryptedMessageByte[j];
                }
            }

            decryptObject.Message = Encoding.UTF8.GetString(decryptObject.MessageByte);
        }

        /// <summary>
        /// Defines the <see cref="RoundSize"/> pair of the given <see cref="EncryptionSize"/> property.
        /// </summary>
        /// <param name="aesObject">Object containing the set <see cref="Encryption"/> property.</param>
        /// <returns>Returns the corresponding <see cref="RoundSize"/> of the passed object's <see cref="EncryptionSize"/> property.</returns>
        private RoundSize DefineRounds(IAESObject aesObject)
        {
            switch (aesObject.Size)
            {
                case EncryptionSize.bits128:
                    return RoundSize.bits128;
                case EncryptionSize.bits192:
                    return RoundSize.bits192;
                case EncryptionSize.bits256:
                    return RoundSize.bits256;
            }

            return RoundSize.bits128;
        }
    }
}
