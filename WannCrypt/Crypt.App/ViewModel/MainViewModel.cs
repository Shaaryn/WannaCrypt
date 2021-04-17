// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="MainViewModel.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.App.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using Crypt.Logic;
    using Crypt.Model;
    using Crypt.Model.Enum;
    using GalaSoft.MvvmLight;

    /// <summary>
    /// ViewModel representing the data in the window.
    /// </summary>
    internal class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public new event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Method which will be used to refresh the UI.
        /// </summary>
        /// <param name="propertyName">Name of the proeprty that is changing.</param>
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
