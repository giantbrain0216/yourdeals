// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationException.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.DataTransferObjects.Exceptions
{
    #region

    using System.Runtime.Serialization;

    #endregion

    /// <summary>
    ///     ErrorMesage
    /// </summary>
    public enum ErrorMesage
    {
        /// <summary>
        ///     couldNotFindEntity
        /// </summary>
        [EnumMember(Value = "couldNotFindEntity")]
        couldNotFindEntity,

        /// <summary>
        ///     userDoesNotHavePermissionToUpdateEntity
        /// </summary>
        [EnumMember(Value = "userDoesNotHavePermissionToUpdateEntity")]
        userDoesNotHavePermissionToUpdateEntity,

        /// <summary>
        ///     AccountIsLockedOut
        /// </summary>
        [EnumMember(Value = "AccountIsLockedOut")]
        AccountIsLockedOut,

        /// <summary>
        ///     UnabletoloaduserwithID
        /// </summary>
        [EnumMember(Value = "UnabletoloaduserwithID")]
        UnabletoloaduserwithID,

        /// <summary>
        ///     UnabletoloadDealwithID
        /// </summary>
        [EnumMember(Value = "UnabletoloadDealwithID")]
        UnabletoloadDealwithID,

        /// <summary>
        ///     UnexpectederroroccurredremovingloginforuserwithID
        /// </summary>
        [EnumMember(Value = "UnexpectederroroccurredremovingloginforuserwithID")]
        UnexpectederroroccurredremovingloginforuserwithID
    }
}