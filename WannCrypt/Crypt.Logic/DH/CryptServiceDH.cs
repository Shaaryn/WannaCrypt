// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="CryptServiceDH.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Logic.DH
{
    using System;
    using Crypt.Logic.Misc;
    using Crypt.Model.DH;
    using Crypt.Model.Interfaces;

    /// <summary>
    /// Service provider responsible for the execution and flow control of the encryption and decryption procedures regarding DH.
    /// </summary>
    public class CryptServiceDH : ICryptServiceDH
    {
        /// <summary>
        /// <inheritdoc/>.
        /// </summary>
        public FormatHelper HelpMeFormat { get; set; }

        /// <summary>
        /// <inheritdoc/>.
        /// </summary>
        public KeyHelper HelpMeKey { get; set; }

        /// <summary>
        /// <inheritdoc/>.
        /// </summary>
        public PublicSpaceObject PublicSpace { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CryptServiceDH"/> class.
        /// </summary>
        /// <param name="publicSpace">Object that contains data regarding the public space.</param>
        public CryptServiceDH(PublicSpaceObject publicSpace)
        {
            HelpMeFormat = new FormatHelper();
            HelpMeKey = new KeyHelper();

            this.PublicSpace = publicSpace;
        }

        /// <summary>
        /// Calculates the DH public key from the private key and with the help of the public variables(p, q).
        /// </summary>
        /// <param name="party">Object that holds data regarding DH.</param>
        public void GeneratePublicKeyForParty(IDHObject party)
        {
            switch (party.PartnerID)
            {
                case Model.Enum.PartnerSide.Right:

                    this.PublicSpace.RightPartyPublicKey = new byte[(int)party.Size];
                    for (int i = 0; i < (int)party.Size; i++)
                    {
                        this.PublicSpace.RightPartyPublicKey[i] = Convert.ToByte(PublicSpace.SmallInteger ^ party.PrivateKey[i] % PublicSpace.LargePrime);
                    }

                    this.PublicSpace.RightPartyPublicKeyString = HelpMeFormat.ConvertByteArrayToHex(PublicSpace.RightPartyPublicKey);

                    break;
                case Model.Enum.PartnerSide.Left:

                    this.PublicSpace.LeftPartyPublicKey = new byte[(int)party.Size];
                    for (int i = 0; i < (int)party.Size; i++)
                    {
                        this.PublicSpace.LeftPartyPublicKey[i] = Convert.ToByte(PublicSpace.SmallInteger ^ party.PrivateKey[i] % PublicSpace.LargePrime);
                    }

                    this.PublicSpace.LeftPartyPublicKeyString = HelpMeFormat.ConvertByteArrayToHex(PublicSpace.LeftPartyPublicKey);

                    break;
            }
        }

        /// <summary>
        /// Calculates the DH full key from the publicly available other key, the private key and from the large prime.
        /// </summary>
        /// <param name="party">Object that holds data regarding DH.</param>
        public void CombineFullKeyForParty(IDHObject party)
        {
            switch (party.PartnerID)
            {
                case Model.Enum.PartnerSide.Right:

                    party.Key = new byte[(int)party.Size];
                    for (int i = 0; i < (int)party.Size; i++)
                    {
                        party.Key[i] = Convert.ToByte(this.PublicSpace.LeftPartyPublicKey[i] ^ party.PrivateKey[i] % PublicSpace.LargePrime);
                    }

                    party.KeyString = HelpMeFormat.ConvertByteArrayToHex(party.Key);

                    break;
                case Model.Enum.PartnerSide.Left:

                    party.Key = new byte[(int)party.Size];
                    for (int i = 0; i < (int)party.Size; i++)
                    {
                        party.Key[i] = Convert.ToByte(this.PublicSpace.RightPartyPublicKey[i] ^ party.PrivateKey[i] % PublicSpace.LargePrime);
                    }

                    party.KeyString = HelpMeFormat.ConvertByteArrayToHex(party.Key);

                    break;
            }
        }
    }
}
