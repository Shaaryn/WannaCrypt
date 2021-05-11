// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="ICryptServiceAES.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Logic.AES
{
    using System;
    using System.Threading.Tasks;
    using Crypt.Logic.Misc;
    using Crypt.Model;
    using Crypt.Model.AES;
    using Crypt.Model.Interfaces;

    /// <summary>
    /// Interface responsible to require the necessary methods and properties in order to have a fully fucntioning crypt service.
    /// </summary>
    public interface ICryptServiceAES
    {
        /// <summary>
        /// Property of the <see cref="FormatHelper"/> class that defines how the string and bytes should be formatted.
        /// </summary>
        public FormatHelper HelpMeFormat { get; set; }

        /// <summary>
        /// Property of the <see cref="KeyHelper"/> class that defines how the key should be generated (if needed), expanded and copied.
        /// </summary>
        public KeyHelper HelpMeKey { get; set; }

        /// <summary>
        /// After all necessary preparations are done, executes the encryption procedure.
        /// </summary>
        /// <param name="encryptTextObject">Object that contains the necessary data for text encryption.</param>
        void ExecuteTextEncryption(IEncryptionObject encryptTextObject);

        /// <summary>
        /// After all the necessary preparations are done, executes the decryption procedure.
        /// </summary>
        /// <param name="decryptTextObject">Object that contains the necessary data for text decryption.</param>
        void ExecuteTextDecryption(IDecryptionObject decryptTextObject);

        /// <summary>
        /// After all the necessary preparations are done, executes the decryption procedure.
        /// </summary>
        /// <param name="publicSpace">Object that contains data regarding the public space.</param>
        /// <param name="receiverDHObject">Object that will receive the message currently being transmitted.</param>
        void ExecuteDHTextDecryption(IPublicSpaceObject publicSpace, IDHObject receiverDHObject);

        /// <summary>
        /// After all necessary preparations are done, executes the encryption procedure.
        /// </summary>
        /// <param name="progressReport">Object that reports the progress of the encryption.</param>
        /// <param name="encryptFileObject">Object that contains the necessary data for file encryption.</param>
        /// <returns>Awaitable asynchronous operation.</returns>
        Task ExecuteFileEncryptionAsync(IProgress<ProgressObject> progressReport, FileEncryptionObject encryptFileObject);

        /// <summary>
        /// After all necessary preparations are done, executes the decryption procedure.
        /// </summary>
        /// <param name="progressReport">Object that repost the progress of the decryption.</param>
        /// <param name="decryptFileObject">Object that contains the necessary data for file decryption.</param>
        /// <returns>Awaitable asynchronous operation.</returns>
        Task ExecuteFileDecryptionAsync(IProgress<ProgressObject> progressReport, FileDecryptionObject decryptFileObject);
    }
}
