// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseTest.cs" company="NPComplet">
//   FR-SAS
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Tests.TestsHelper
{
    #region

    using System;

    using Xunit.Abstractions;

    #endregion

    /// <summary>
    /// The base test.
    /// </summary>
    public class BaseTest : IWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTest"/> class.
        /// </summary>
        /// <param name="output">
        /// The output.
        /// </param>
        public BaseTest(ITestOutputHelper output)
        {
            this.Output = output;
        }

        /// <summary>
        /// Gets the output.
        /// </summary>
        public ITestOutputHelper Output { get; }

        /// <summary>
        /// The write line.
        /// </summary>
        /// <param name="str">
        /// The str.
        /// </param>
        public void WriteLine(string str)
        {
            this.Output.WriteLine(str ?? Environment.NewLine);
        }
    }
}