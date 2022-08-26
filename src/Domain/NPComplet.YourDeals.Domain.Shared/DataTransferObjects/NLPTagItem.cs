// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NLPTagItem.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.DataTransferObjects
{
    #region

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// </summary>
    public class NlpTagItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NlpTagItem"/> class. 
        ///     Initialize new instance of <see cref="NlpTagItem"/>.
        /// </summary>
        public NlpTagItem()
        {
            this.TaggedWords = new List<TaggedWord>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NlpTagItem"/> class. 
        /// Initialize new instance of <see cref="NlpTagItem"/>.
        /// </summary>
        /// <param name="tag">
        /// The tag
        /// </param>
        public NlpTagItem(string tag)
            : this()
        {
            this.Tag = tag;
        }

        /// <summary>
        /// </summary>
        public int IndexInSentence { get; set; }

        /// <summary>
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// </summary>
        public List<TaggedWord> TaggedWords { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"[{(!string.IsNullOrEmpty(this.Tag) ? this.Tag + " " : string.Empty)}{string.Join(" ", this.TaggedWords)}]";
        }
    }
}