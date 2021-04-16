// |+|-------------------------YB7IQX-------------------------|+|
// <copyright file="ProgressModel.cs" company="ITSec midterm project">
// This code is part of my 'ITSec midterm project'. Please use it carefully.
// </copyright>
// |+|-------------------------YB7IQX-------------------------|+|

namespace Crypt.Model
{
    /// <summary>
    /// Represent a model that is able to track the progress of execution.
    /// </summary>
    public class ProgressModel
    {
        /// <summary>
        /// Current progress towards the finish (100%).
        /// </summary>
        public int CurrentProgress { get; set; }

        /// <summary>
        /// Number of 16 bits long blocks the message contains.
        /// </summary>
        public int MaximumNumberOfBlocks { get; set; }
    }
}
