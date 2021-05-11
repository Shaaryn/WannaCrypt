// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="ICryptServiceDH.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

using Crypt.Model.Interfaces;

namespace Crypt.Logic.DH
{
    public interface ICryptServiceDH
    {
        public void GeneratePublicKeyForParty(IDHObject party);

        public void CombineFullKeyForParty(IDHObject party);
    }
}
