// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="ICryptService.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Logic
{
    using Crypt.Logic.Misc;
    using Crypt.Model;

    /// <summary>
    /// Interface responsible to require the necessary methods and properties in order to have a fully fucntioning crypt service.
    /// </summary>
    public interface ICryptService
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
        void ExecuteTextEncryption(TextEncryptionObject encryptTextObject);

        /// <summary>
        /// After all the necessary preparations are done, executes the decryption procedure.
        /// </summary>
        /// <param name="decryptTextObject">Object that contains the necessary data for text decryption.</param>
        void ExecuteTextDecryption(TextDecryptionObject decryptTextObject);

        /// <summary>
        /// After all necessary preparations are done, executes the encryption procedure.
        /// </summary>
        /// <param name="encryptFileObject">Object that contains the necessary data for file encryption.</param>
        void ExecuteFileEncryption(FileEncryptionObject encryptFileObject);

        /// <summary>
        /// After all necessary preparations are done, executes the decryption procedure.
        /// </summary>
        /// <param name="decryptFileObject">Object that contains the necessary data for file decryption.</param>
        void ExecuteFileDecryption(FileDecryptionObject decryptFileObject);
    }
}
