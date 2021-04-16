// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="Encryption.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Logic.EncryptionLogic
{
    using Crypt.Model;
    using Crypt.Model.Interfaces;

    /// <summary>
    /// Class responsible for the implementation of the encryption process.
    /// </summary>
    public sealed class Encryption : IEncryption
    {
        private const int BlockSize = 16;
        private byte[] state;

        /// <summary>
        /// Initializes a new instance of the <see cref="Encryption"/> class.
        /// </summary>
        public Encryption()
        {
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="encryptObject"> Object that contains the necessary data for encryption.</param>
        /// <param name="offset">Offset varaible determining the current block that is being encrypted.</param>
        /// <returns>Array of bytes containing the currently encrypted block.</returns>
        public byte[] Encrypt(IEncryptionObject encryptObject, int offset)
        {
            // Copy 16 bits of the message to the state.
            this.state = new byte[BlockSize];
            for (int i = 0; i < BlockSize; i++)
            {
                state[i] = encryptObject.PaddedMessage[i + offset];
            }

            // -1 because in this case : 16 * 0 = 0 so the currentKey index is not incremented in the function.
            AddRoundKey(encryptObject.Key, -1);

            for (int i = 0; i < (int)encryptObject.Round; i++)
            {
                SubBytes();
                ShiftRows();
                MixColumns();
                AddRoundKey(encryptObject.ExpandedKey, i);
            }

            // Final round
            SubBytes();
            ShiftRows();
            AddRoundKey(encryptObject.ExpandedKey, (int)encryptObject.Round);

            return this.state;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void SubBytes()
        {
            for (int i = 0; i < BlockSize; i++)
            {
                this.state[i] = Tables.SBox[this.state[i]];
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void ShiftRows()
        {
            byte[] tmp = new byte[BlockSize];

            tmp[0] = this.state[0];
            tmp[1] = state[5];
            tmp[2] = state[10];
            tmp[3] = state[15];

            tmp[4] = state[4];
            tmp[5] = state[9];
            tmp[6] = state[14];
            tmp[7] = state[3];

            tmp[8] = state[8];
            tmp[9] = state[13];
            tmp[10] = state[2];
            tmp[11] = state[7];

            tmp[12] = state[12];
            tmp[13] = state[1];
            tmp[14] = state[6];
            tmp[15] = state[11];

            for (int i = 0; i < BlockSize; i++)
            {
                this.state[i] = tmp[i];
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void MixColumns()
        {
            byte[] tmp = new byte[16];

            tmp[0] = (byte)(Tables.GMul2[this.state[0]] ^ Tables.GMul3[state[1]] ^ state[2] ^ state[3]);
            tmp[1] = (byte)(state[0] ^ Tables.GMul2[state[1]] ^ Tables.GMul3[state[2]] ^ state[3]);
            tmp[2] = (byte)(state[0] ^ state[1] ^ Tables.GMul2[state[2]] ^ Tables.GMul3[state[3]]);
            tmp[3] = (byte)(Tables.GMul3[state[0]] ^ state[1] ^ state[2] ^ Tables.GMul2[state[3]]);

            tmp[4] = (byte)(Tables.GMul2[state[4]] ^ Tables.GMul3[state[5]] ^ state[6] ^ state[7]);
            tmp[5] = (byte)(state[4] ^ Tables.GMul2[state[5]] ^ Tables.GMul3[state[6]] ^ state[7]);
            tmp[6] = (byte)(state[4] ^ state[5] ^ Tables.GMul2[state[6]] ^ Tables.GMul3[state[7]]);
            tmp[7] = (byte)(Tables.GMul3[state[4]] ^ state[5] ^ state[6] ^ Tables.GMul2[state[7]]);

            tmp[8] = (byte)(Tables.GMul2[state[8]] ^ Tables.GMul3[state[9]] ^ state[10] ^ state[11]);
            tmp[9] = (byte)(state[8] ^ Tables.GMul2[state[9]] ^ Tables.GMul3[state[10]] ^ state[11]);
            tmp[10] = (byte)(state[8] ^ state[9] ^ Tables.GMul2[state[10]] ^ Tables.GMul3[state[11]]);
            tmp[11] = (byte)(Tables.GMul3[state[8]] ^ state[9] ^ state[10] ^ Tables.GMul2[state[11]]);

            tmp[12] = (byte)(Tables.GMul2[state[12]] ^ Tables.GMul3[state[13]] ^ state[14] ^ state[15]);
            tmp[13] = (byte)(state[12] ^ Tables.GMul2[state[13]] ^ Tables.GMul3[state[14]] ^ state[15]);
            tmp[14] = (byte)(state[12] ^ state[13] ^ Tables.GMul2[state[14]] ^ Tables.GMul3[state[15]]);
            tmp[15] = (byte)(Tables.GMul3[state[12]] ^ state[13] ^ state[14] ^ Tables.GMul2[state[15]]);

            for (int i = 0; i < 16; i++)
            {
                this.state[i] = tmp[i];
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="currentKey">Defines the key used for the current round key.</param>
        /// <param name="roundIndex">Defines the offset of the key (block) which will be used.</param>
        public void AddRoundKey(byte[] currentKey, int roundIndex)
        {
            for (int i = 0; i < BlockSize; i++)
            {
                this.state[i] ^= currentKey[i + (16 * (roundIndex + 1))];
            }
        }
    }
}
