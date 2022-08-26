// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaggedWord.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.DataTransferObjects
{
    #region

    using System.Linq;

    #endregion

    /// <summary>
    /// </summary>
    public class TaggedWord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaggedWord"/> class. 
        /// </summary>
        public TaggedWord()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaggedWord"/> class. 
        /// </summary>
        /// <param name="stringTaggedWord">
        /// </param>
        /// <param name="indexInGroup">
        /// </param>
        public TaggedWord(string stringTaggedWord, int indexInGroup)
        {
            if (stringTaggedWord.Contains("/"))
            {
                this.Word = stringTaggedWord.Split('/').First();
                this.Tag = stringTaggedWord.Split('/').Last();
                this.Index = indexInGroup;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaggedWord"/> class. 
        /// </summary>
        /// <param name="word">
        /// </param>
        /// <param name="tag">
        /// </param>
        /// <param name="index">
        /// </param>
        public TaggedWord(string word, string tag, int index)
        {
            this.Word = word;
            this.Tag = tag;
            this.Index = index;
        }

        /// <summary>
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0}/{1}", this.Word, this.Tag);
        }
    }
}