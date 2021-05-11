// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="Decryption.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Logic.AES.DecryptionLogic
{
    using Crypt.Model;
    using Crypt.Model.Interfaces;

    /// <summary>
    /// Class responsible for the implementation of the encryption process.
    /// </summary>
    public sealed class Decryption : IDecryption
    {
        private const int BlockSize = 16;
        private byte[] state;

        /// <summary>
        /// Initializes a new instance of the <see cref="Decryption"/> class.
        /// </summary>
        public Decryption()
        {
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="decryptObject">Object that contains the necessary data for decryption.</param>
        /// <param name="offset">Offset variable determining the current block that is being decrypted.</param>
        /// <returns>Array of bytes containing the currently decrypted block.</returns>
        public byte[] Decrypt(IDecryptionObject decryptObject, int offset)
        {
            // Copy 16 bits of the encripted message to the state.
            this.state = new byte[BlockSize];
            for (int i = 0; i < BlockSize; i++)
            {
                state[i] = decryptObject.EncryptedPaddedMessage[offset][i];
            }

            // 8 because in this case : 16 * (9+1) = 144 so the key will be used from top to bottom
            AddRoundKey(decryptObject.ExpandedKey, (int)decryptObject.Round);

            for (int i = (int)decryptObject.Round - 1; i >= 0; i--)
            {
                InvShiftRows();
                InvSubBytes();
                AddRoundKey(decryptObject.ExpandedKey, i);
                InvMixColumns();
            }

            // Final round
            InvShiftRows();
            InvSubBytes();
            AddRoundKey(decryptObject.Key, -1);

            return this.state;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void InvSubBytes()
        {
            for (int i = 0; i < BlockSize; i++)
            {
                this.state[i] = Tables.InvSBox[this.state[i]];
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void InvShiftRows()
        {
            byte[] tmp = new byte[BlockSize];

            tmp[0] = this.state[0];
            tmp[1] = state[13];
            tmp[2] = state[10];
            tmp[3] = state[7];

            tmp[4] = state[4];
            tmp[5] = state[1];
            tmp[6] = state[14];
            tmp[7] = state[11];

            tmp[8] = state[8];
            tmp[9] = state[5];
            tmp[10] = state[2];
            tmp[11] = state[15];

            tmp[12] = state[12];
            tmp[13] = state[9];
            tmp[14] = state[6];
            tmp[15] = state[3];

            for (int i = 0; i < BlockSize; i++)
            {
                this.state[i] = tmp[i];
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void InvMixColumns()
        {
            byte[] tmp = new byte[BlockSize];

            /* r0 = 14(a0) + 9(a3) + 13(a2) + 11(a1)
             * r1 = 14(a1) + 9(a0) + 13(a3) + 11(a2)
             * r2 = 14(a2) + 9(a1) + 13(a0) + 11(a3)
             * r3 = 14(a3) + 9(a2) + 13(a1) + 11(a0) */

            tmp[0] = (byte)(Tables.GMul14[this.state[0]] ^ Tables.GMul9[state[3]] ^ Tables.GMul13[state[2]] ^ Tables.GMul11[state[1]]);
            tmp[1] = (byte)(Tables.GMul14[state[1]] ^ Tables.GMul9[state[0]] ^ Tables.GMul13[state[3]] ^ Tables.GMul11[state[2]]);
            tmp[2] = (byte)(Tables.GMul14[state[2]] ^ Tables.GMul9[state[1]] ^ Tables.GMul13[state[0]] ^ Tables.GMul11[state[3]]);
            tmp[3] = (byte)(Tables.GMul14[state[3]] ^ Tables.GMul9[state[2]] ^ Tables.GMul13[state[1]] ^ Tables.GMul11[state[0]]);

            tmp[4] = (byte)(Tables.GMul14[this.state[4]] ^ Tables.GMul9[state[7]] ^ Tables.GMul13[state[6]] ^ Tables.GMul11[state[5]]);
            tmp[5] = (byte)(Tables.GMul14[state[5]] ^ Tables.GMul9[state[4]] ^ Tables.GMul13[state[7]] ^ Tables.GMul11[state[6]]);
            tmp[6] = (byte)(Tables.GMul14[state[6]] ^ Tables.GMul9[state[5]] ^ Tables.GMul13[state[4]] ^ Tables.GMul11[state[7]]);
            tmp[7] = (byte)(Tables.GMul14[state[7]] ^ Tables.GMul9[state[6]] ^ Tables.GMul13[state[5]] ^ Tables.GMul11[state[4]]);

            tmp[8] = (byte)(Tables.GMul14[this.state[8]] ^ Tables.GMul9[state[11]] ^ Tables.GMul13[state[10]] ^ Tables.GMul11[state[9]]);
            tmp[9] = (byte)(Tables.GMul14[state[9]] ^ Tables.GMul9[state[8]] ^ Tables.GMul13[state[11]] ^ Tables.GMul11[state[10]]);
            tmp[10] = (byte)(Tables.GMul14[state[10]] ^ Tables.GMul9[state[9]] ^ Tables.GMul13[state[8]] ^ Tables.GMul11[state[11]]);
            tmp[11] = (byte)(Tables.GMul14[state[11]] ^ Tables.GMul9[state[10]] ^ Tables.GMul13[state[9]] ^ Tables.GMul11[state[8]]);

            tmp[12] = (byte)(Tables.GMul14[this.state[12]] ^ Tables.GMul9[state[15]] ^ Tables.GMul13[state[14]] ^ Tables.GMul11[state[13]]);
            tmp[13] = (byte)(Tables.GMul14[state[13]] ^ Tables.GMul9[state[12]] ^ Tables.GMul13[state[15]] ^ Tables.GMul11[state[14]]);
            tmp[14] = (byte)(Tables.GMul14[state[14]] ^ Tables.GMul9[state[13]] ^ Tables.GMul13[state[12]] ^ Tables.GMul11[state[15]]);
            tmp[15] = (byte)(Tables.GMul14[state[15]] ^ Tables.GMul9[state[14]] ^ Tables.GMul13[state[13]] ^ Tables.GMul11[state[12]]);

            for (int i = 0; i < BlockSize; i++)
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
