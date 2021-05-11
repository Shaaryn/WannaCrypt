// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="CryptServiceAES.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Logic.AES
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Crypt.Logic.AES.DecryptionLogic;
    using Crypt.Logic.AES.EncryptionLogic;
    using Crypt.Logic.Misc;
    using Crypt.Model;
    using Crypt.Model.AES;
    using Crypt.Model.Enum;
    using Crypt.Model.Interfaces;

    /// <summary>
    /// Service provider responsible for the execution and flow control of the encryption and decryption procedures regarding AES.
    /// </summary>
    public class CryptServiceAES : ICryptServiceAES
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
        /// Initializes a new instance of the <see cref="CryptServiceAES"/> class.
        /// </summary>
        public CryptServiceAES()
        {
            helpMeFormat = new FormatHelper();
            helpMeKey = new KeyHelper();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="encryptTextObject">Object that contains the necessary data for text encryption.</param>
        public void ExecuteTextEncryption(IEncryptionObject encryptTextObject)
        {
            // Define the rounds
            encryptTextObject.Round = DefineRounds(encryptTextObject);

            // Bytify the text
            encryptTextObject.MessageByte = new byte[encryptTextObject.Message.Length];
            for (int i = 0; i < encryptTextObject.Message.Length; i++)
            {
                encryptTextObject.MessageByte[i] = Convert.ToByte(encryptTextObject.Message[i]);
            }

            // Pad the message
            encryptTextObject.PaddedMessage = helpMeFormat.CalculatePadding(encryptTextObject);

            // Calculate the expanded key
            encryptTextObject.Key = new byte[(int)encryptTextObject.Size];
            int n = 0;
            if (encryptTextObject.KeyString != null)
            {
                for (int i = 0; i < encryptTextObject.KeyString.Length; i += 3)
                {
                    encryptTextObject.Key[n] = Convert.ToByte(encryptTextObject.KeyString.Substring(i, 2), 16);
                    n++;
                }
            }

            encryptTextObject.ExpandedKey = helpMeKey.KeyExpansion(encryptTextObject);

            // Execute the encryption process for each block
            Encryption encryption = new Encryption();
            byte[] tmpEncryptedMessage = new byte[BlockSize];
            encryptTextObject.EncryptedMessage = new byte[encryptTextObject.PaddedMessage.Length];

            for (int i = 0; i < encryptTextObject.PaddedMessage.Length; i += BlockSize)
            {
                tmpEncryptedMessage = encryption.Encrypt(encryptTextObject, i);
                for (int j = 0; j < BlockSize; j++)
                {
                    encryptTextObject.EncryptedMessage[j + i] = tmpEncryptedMessage[j];
                }
            }

            encryptTextObject.EncryptedMessageString = BitConverter.ToString(encryptTextObject.EncryptedMessage);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="decryptTextObject">Object that contains the necessary data for text decryption.</param>
        public void ExecuteTextDecryption(IDecryptionObject decryptTextObject)
        {
            // Defines the rounds
            decryptTextObject.Round = DefineRounds(decryptTextObject);

            // Break the message into blocksized arrays
            decryptTextObject.EncryptedPaddedMessage = new List<byte[]>();
            decryptTextObject.EncryptedPaddedMessage = helpMeFormat.CalculateArrays(null, decryptTextObject);

            // Calculate the expanded key.
            decryptTextObject.Key = new byte[(int)decryptTextObject.Size];
            int k = 0;
            for (int i = 0; i < decryptTextObject.KeyString.Length; i += 3)
            {
                decryptTextObject.Key[k] = Convert.ToByte(decryptTextObject.KeyString.Substring(i, 2), 16);
                k++;
            }

            decryptTextObject.ExpandedKey = helpMeKey.KeyExpansion(decryptTextObject);

            // Execute the decryption process for each block
            Decryption decryption = new Decryption();
            byte[] tmpDecryptedMessageByte = new byte[BlockSize];
            decryptTextObject.MessageByte = new byte[BlockSize * decryptTextObject.EncryptedPaddedMessage.Count];

            for (int i = 0; i < decryptTextObject.EncryptedPaddedMessage.Count; i++)
            {
                tmpDecryptedMessageByte = decryption.Decrypt(decryptTextObject, i);
                for (int j = 0; j < BlockSize; j++)
                {
                    decryptTextObject.MessageByte[(i * BlockSize) + j] = tmpDecryptedMessageByte[j];
                }
            }

            decryptTextObject.Message = Encoding.UTF8.GetString(decryptTextObject.MessageByte);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="publicSpace">object that contains data regarding the public space.</param>
        /// <param name="receiverDHObject">object that will receive the message currently being transmitted.</param>
        public void ExecuteDHTextDecryption(IPublicSpaceObject publicSpace, IDHObject receiverDHObject)
        {
            // Defines the rounds
            receiverDHObject.Round = DefineRounds(receiverDHObject);

            // Break the message into blocksized arrays
            receiverDHObject.EncryptedPaddedMessage = new List<byte[]>();

            switch (receiverDHObject.PartnerID)
            {
                case PartnerSide.Right:

                    receiverDHObject.EncryptedPaddedMessage = helpMeFormat.CalculateArrays(publicSpace.LeftPartyPublicMessage);
                    break;
                case PartnerSide.Left:

                    receiverDHObject.EncryptedPaddedMessage = helpMeFormat.CalculateArrays(publicSpace.RightPartyPublicMessage);
                    break;
            }

            // Calculate the expanded key.
            receiverDHObject.Key = new byte[(int)receiverDHObject.Size];
            int k = 0;
            for (int i = 0; i < receiverDHObject.KeyString.Length; i += 3)
            {
                receiverDHObject.Key[k] = Convert.ToByte(receiverDHObject.KeyString.Substring(i, 2), 16);
                k++;
            }

            receiverDHObject.ExpandedKey = helpMeKey.KeyExpansion(receiverDHObject);

            // Execute the decryption process for each block
            Decryption decryption = new Decryption();
            byte[] tmpDecryptedMessageByte = new byte[BlockSize];

            receiverDHObject.ReceivedMessageByte = new byte[BlockSize * receiverDHObject.EncryptedPaddedMessage.Count];

            for (int i = 0; i < receiverDHObject.EncryptedPaddedMessage.Count; i++)
            {
                tmpDecryptedMessageByte = decryption.Decrypt(receiverDHObject, i);
                for (int j = 0; j < BlockSize; j++)
                {
                    receiverDHObject.ReceivedMessageByte[(i * BlockSize) + j] = tmpDecryptedMessageByte[j];
                }
            }

            receiverDHObject.ReceivedMessage = Encoding.UTF8.GetString(receiverDHObject.ReceivedMessageByte);
        }

        /// <summary>
        /// <inheritdoc/>.
        /// </summary>
        /// <param name="progressReport">Object that reports the progress of the encryption.</param>
        /// <param name="encryptFileObject">Object that contains the necessary data for file encryption.</param>
        /// <returns>Awaitable asynchronous operation.</returns>
        public async Task ExecuteFileEncryptionAsync(IProgress<ProgressObject> progressReport, FileEncryptionObject encryptFileObject)
        {
            // Read the file
            encryptFileObject.MessageByte = File.ReadAllBytes(encryptFileObject.Path);

            // Define the rounds
            encryptFileObject.Round = DefineRounds(encryptFileObject);

            // Pad the message + async model/variable
            encryptFileObject.PaddedMessage = helpMeFormat.CalculatePadding(encryptFileObject);
            ProgressObject report = new ProgressObject();
            report.MaximumNumberOfBlocks = encryptFileObject.PaddedMessage.Length / 16;

            // Calculate the expanded key
            encryptFileObject.Key = new byte[(int)encryptFileObject.Size];
            int n = 0;
            if (encryptFileObject.KeyString != null)
            {
                for (int i = 0; i < encryptFileObject.KeyString.Length; i += 3)
                {
                    encryptFileObject.Key[n] = Convert.ToByte(encryptFileObject.KeyString.Substring(i, 2), 16);
                    n++;
                }
            }

            encryptFileObject.ExpandedKey = helpMeKey.KeyExpansion(encryptFileObject);

            // Execute the encryption process for each block
            Encryption encryption = new Encryption();
            byte[] tmpEncryptedMessage = new byte[BlockSize];
            encryptFileObject.EncryptedMessage = new byte[encryptFileObject.PaddedMessage.Length];

            for (int i = 0; i < encryptFileObject.PaddedMessage.Length; i += BlockSize)
            {
                // Async operation
                tmpEncryptedMessage = await Task.Run(() => encryption.Encrypt(encryptFileObject, i));
                report.CurrentProgress = ((i / 16) * 100) / report.MaximumNumberOfBlocks;
                progressReport.Report(report);

                for (int j = 0; j < BlockSize; j++)
                {
                    encryptFileObject.EncryptedMessage[j + i] = tmpEncryptedMessage[j];
                }
            }

            encryptFileObject.EncryptedMessageString = BitConverter.ToString(encryptFileObject.EncryptedMessage);

            string resultPath = Directory.GetCurrentDirectory() +
                "\\EncryptedFiles" +
                $"\\[Encrypted]{encryptFileObject.FileName}";

            File.WriteAllBytes(resultPath, encryptFileObject.EncryptedMessage);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="progressReport">Object that repost the progress of the decryption.</param>
        /// <param name="decryptFileObject">Object that contains the necessary data for file decryption.</param>
        /// <returns>Awaitable asynchronous operation.</returns>
        public async Task ExecuteFileDecryptionAsync(IProgress<ProgressObject> progressReport, FileDecryptionObject decryptFileObject)
        {
            // Read the file
            decryptFileObject.MessageByte = File.ReadAllBytes(decryptFileObject.Path);

            // Define the rounds
            decryptFileObject.Round = DefineRounds(decryptFileObject);

            // Break the file into blocksized arrays
            decryptFileObject.EncryptedPaddedMessage = new List<byte[]>();
            decryptFileObject.EncryptedPaddedMessage = helpMeFormat.CalculateArrays(null, decryptFileObject);
            ProgressObject report = new ProgressObject();
            report.MaximumNumberOfBlocks = decryptFileObject.EncryptedPaddedMessage.Count;

            // Calculate the expanded key.
            decryptFileObject.Key = new byte[(int)decryptFileObject.Size];
            int k = 0;
            for (int i = 0; i < decryptFileObject.KeyString.Length; i += 3)
            {
                decryptFileObject.Key[k] = Convert.ToByte(decryptFileObject.KeyString.Substring(i, 2), 16);
                k++;
            }

            decryptFileObject.ExpandedKey = helpMeKey.KeyExpansion(decryptFileObject);

            // Execute the decryption process for each block
            Decryption decryption = new Decryption();
            byte[] tmpDecryptedMessageByte = new byte[BlockSize];
            decryptFileObject.MessageByte = new byte[BlockSize * decryptFileObject.EncryptedPaddedMessage.Count];

            for (int i = 0; i < decryptFileObject.EncryptedPaddedMessage.Count; i++)
            {
                // Async oepration
                tmpDecryptedMessageByte = await Task.Run(() => decryption.Decrypt(decryptFileObject, i));
                report.CurrentProgress = (i * 100) / report.MaximumNumberOfBlocks;
                progressReport.Report(report);

                for (int j = 0; j < BlockSize; j++)
                {
                    decryptFileObject.MessageByte[(i * BlockSize) + j] = tmpDecryptedMessageByte[j];
                }
            }

            string resultPath = Directory.GetCurrentDirectory() +
                "\\DecryptedFiles" +
                $"\\[Decrypted]{decryptFileObject.FileName}";

            File.WriteAllBytes(resultPath, decryptFileObject.MessageByte);
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
