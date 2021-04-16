// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="IFileObject.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Model.Interfaces
{
    /// <summary>
    /// Represents an object that derives from a file.
    /// </summary>
    public interface IFileObject
    {
        /// <summary>
        /// Name of the file that was chosen.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Path of the file we want to encrypt.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Extension of the file we are encrypting.
        /// </summary>
        public string Extension { get; set; }
    }
}
