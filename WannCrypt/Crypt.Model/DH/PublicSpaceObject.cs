// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="PublicSpaceObject.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Model.DH
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using AzureControl;
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

        public void SerilizePublicSpace(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            StreamWriter fs = new StreamWriter(path);

            fs.WriteLine("############################################################");
            fs.WriteLine("############################################################");
            fs.WriteLine("############################################################");
            fs.WriteLine("############################################################");
            fs.WriteLine("############################################################");
            fs.WriteLine("##### Used large prime: " + this.LargePrime);
            fs.WriteLine("##### Small integer: " + this.SmallInteger);
            fs.WriteLine("############################################################");
            fs.WriteLine("########## Public Left");
            fs.WriteLine("############################################################");
            fs.WriteLine("##### 1 # Public left key (string): ");
            fs.WriteLine("#####" + this.LeftPartyPublicKeyString);
            fs.WriteLine("##### 2 # Public left key (byte[]): ");
            fs.WriteLine("##### " + Encoding.Default.GetString(this.LeftPartyPublicKey));
            fs.WriteLine("##### 3 # Public left message (string): ");
            fs.WriteLine("#####" + this.LeftPartyPublicMessage);
            fs.WriteLine("##### 4 # Public left message (byte[]): ");
            fs.WriteLine("##### " + Encoding.Default.GetString(this.LeftPartyPublicMessageByte));
            fs.WriteLine("############################################################");
            fs.WriteLine("########## Public Right");
            fs.WriteLine("############################################################");
            fs.WriteLine("##### 1 # Public right key (string): ");
            fs.WriteLine("#####" + this.RightPartyPublicKeyString);
            fs.WriteLine("##### 2 # Public right key (byte[]): ");
            fs.WriteLine("##### " + Encoding.Default.GetString(this.RightPartyPublicKey));
            fs.WriteLine("##### 3 # Public right message (string): ");
            fs.WriteLine("#####" + this.RightPartyPublicMessage);
            fs.WriteLine("##### 4 # Public right message (byte[]): ");
            fs.WriteLine("##### " + Encoding.Default.GetString(this.RightPartyPublicMessageByte));
            fs.WriteLine("############################################################");
            fs.WriteLine("############################################################");
            fs.WriteLine("############################################################");
            fs.WriteLine("############################################################");
            fs.WriteLine("############################################################");

            fs.Close();
        }

        public void SerilizeAndPush()
        {
            string path = Directory.GetCurrentDirectory() + $"{this.GetType().Name}.txt";

            this.SerilizePublicSpace(path);
            AzureController.UploadFile(this.GetType().Name, path, "publicspace");
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
