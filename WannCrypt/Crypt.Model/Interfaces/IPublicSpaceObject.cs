// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="IPublicSpaceObject.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Model.Interfaces
{
    /// <summary>
    /// Represents a public space object.
    /// </summary>
    public interface IPublicSpaceObject
    {
        /// <summary>
        /// Large prime number according to the DH documentation.
        /// https://www.math.brown.edu/johsilve/MathCrypto/SampleSections.pdf (Page 66 - Table 2.2).
        /// </summary>
        public int LargePrime { get; }

        /// <summary>
        /// Small integer number according to the DH documentation.
        /// https://www.math.brown.edu/johsilve/MathCrypto/SampleSections.pdf (Page 66 - Table 2.2).
        /// </summary>
        public int SmallInteger { get; }

        /// <summary>
        /// String representation of the left party's public key.
        /// </summary>
        public string LeftPartyPublicKeyString { get; set; }

        /// <summary>
        /// Byte representation of the left party's public key.
        /// </summary>
        public byte[] LeftPartyPublicKey { get; set; }

        /// <summary>
        /// String representation of the left party's public key.
        /// </summary>
        public string RightPartyPublicKeyString { get; set; }

        /// <summary>
        /// Byte representation of the left party's public key.
        /// </summary>
        public byte[] RightPartyPublicKey { get; set; }

        /// <summary>
        /// String representation of the left party's encrypted message.
        /// </summary>
        public string LeftPartyPublicMessage { get; set; }

        /// <summary>
        /// Byte representation of the left party's encrypted message.
        /// </summary>
        public byte[] LeftPartyPublicMessageByte { get; set; }

        /// <summary>
        /// String representation of the right party's public key.
        /// </summary>
        public string RightPartyPublicMessage { get; set; }

        /// <summary>
        /// Byte representation of the left party's public key.
        /// </summary>
        public byte[] RightPartyPublicMessageByte { get; set; }
    }
}
