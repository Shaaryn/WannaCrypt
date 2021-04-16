// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="RoundSize.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Model.Enum
{
    /// <summary>
    /// Enum type determining the number of rounds the encryption or decryption procedure takes.
    /// </summary>
    public enum RoundSize
    {
        /// <summary>
        /// It takes 9 rounds to encrypt a message with a 128 bit key. Not counting the initial and the final round.
        /// </summary>
        bits128 = 9,

        /// <summary>
        /// It takes 11 rounds to encrypt a message with a 128 bit key. Not counting the initial and the final round.
        /// </summary>
        bits192 = 11,

        /// <summary>
        /// It takes 13 rounds to encrypt a message with a 128 bit key. Not counting the initial and the final round.
        /// </summary>
        bits256 = 13,
    }
}
