// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="PublicSpaceObject.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Model.DH
{
    using System.ComponentModel;
    using Crypt.Model.Interfaces;

    /// <summary>
    /// Model that is responsible of storing data regarding a message transmission between two communicating parties.
    /// </summary>
    public class PublicSpaceObject : IPublicSpaceObject, INotifyPropertyChanged
    {
        private int largePrime = 2147483423;
        private int smallInteger = 3;

        private string leftPartyPublicKeyString;
        private byte[] leftPartyPublicKey;
        private string rightPartyPublicKeyString;
        private byte[] rightPartyPublicKey;

        private string leftPartyPublicMessage;
        private byte[] leftPartyPublicMessageByte;
        private string rightPartyPublicMessage;
        private byte[] rightPartyPublicMessageByte;

        /// <summary>
        /// <inheritdoc/>.
        /// </summary>
        public int LargePrime
        {
            get { return this.largePrime; }
        }

        /// <summary>
        /// <inheritdoc/>.
        /// </summary>
        public int SmallInteger
        {
            get { return this.smallInteger; }
        }

        /// <summary>
        /// <inheritdoc/>.
        /// </summary>
        public string LeftPartyPublicKeyString
        {
            get
            {
                return this.leftPartyPublicKeyString;
            }

            set
            {
                this.leftPartyPublicKeyString = value;
                this.OnPropertyChanged(nameof(this.LeftPartyPublicKeyString));
            }
        }

        /// <summary>
        /// <inheritdoc/>.
        /// </summary>
        public byte[] LeftPartyPublicKey
        {
            get
            {
                return this.leftPartyPublicKey;
            }

            set
            {
                this.leftPartyPublicKey = value;
                this.OnPropertyChanged(nameof(this.LeftPartyPublicKey));
            }
        }

        /// <summary>
        /// <inheritdoc/>.
        /// </summary>
        public string RightPartyPublicKeyString
        {
            get
            {
                return this.rightPartyPublicKeyString;
            }

            set
            {
                this.rightPartyPublicKeyString = value;
                this.OnPropertyChanged(nameof(this.RightPartyPublicKeyString));
            }
        }

        /// <summary>
        /// <inheritdoc/>.
        /// </summary>
        public byte[] RightPartyPublicKey
        {
            get
            {
                return this.rightPartyPublicKey;
            }

            set
            {
                this.rightPartyPublicKey = value;
                this.OnPropertyChanged(nameof(this.RightPartyPublicKey));
            }
        }

        /// <summary>
        /// <inheritdoc/>.
        /// </summary>
        public string LeftPartyPublicMessage
        {
            get
            {
                return this.leftPartyPublicMessage;
            }

            set
            {
                this.leftPartyPublicMessage = value;
                this.OnPropertyChanged(nameof(this.LeftPartyPublicMessage));
            }
        }

        /// <summary>
        /// <inheritdoc/>.
        /// </summary>
        public byte[] LeftPartyPublicMessageByte
        {
            get
            {
                return this.leftPartyPublicMessageByte;
            }

            set
            {
                this.leftPartyPublicMessageByte = value;
                this.OnPropertyChanged(nameof(this.LeftPartyPublicMessageByte));
            }
        }

        /// <summary>
        /// <inheritdoc/>.
        /// </summary>
        public string RightPartyPublicMessage
        {
            get
            {
                return this.rightPartyPublicMessage;
            }

            set
            {
                this.rightPartyPublicMessage = value;
                this.OnPropertyChanged(nameof(this.RightPartyPublicMessage));
            }
        }

        /// <summary>
        /// <inheritdoc/>.
        /// </summary>
        public byte[] RightPartyPublicMessageByte
        {
            get
            {
                return this.rightPartyPublicMessageByte;
            }

            set
            {
                this.rightPartyPublicMessageByte = value;
                this.OnPropertyChanged(nameof(this.RightPartyPublicMessageByte));
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="propertyName">Name of the property that is changing.</param>
        public void OnPropertyChanged(string propertyName)
        {
            if (propertyName != null)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
