// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationConstants.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Constant.Application
{
    /// <summary>
    ///     Shared App Coonstant
    /// </summary>
    public static class ApplicationConstants
    {



        public const string ApiUrl = "https://uat.api.pamdeals.com/";

        /// <summary>
        ///     SignalR Constant
        /// </summary>
        public static class SignalR
        {
          
            /// <summary>
            ///     /
            /// </summary>
            public const string ConnectUser = "ConnectUser";

            /// <summary>
            /// </summary>
            public const string DealHubUrl = "dealHub";

            /// <summary>
            /// </summary>
            public const string DisconnectUser = "DisconnectUser";

            /// <summary>
            ///     HubUrl
            /// </summary>
            public const string HubUrl = "signalRHub";
            /// <summary>
            /// 
            /// </summary>
            public const string HubUrlApi = "/signalRHub";
            /// <summary>
            /// </summary>
            public const string LogoutUsersByRole = "LogoutUsersByRole";

            /// <summary>
            /// </summary>
            public const string OnChangeRolePermissions = "OnChangeRolePermissions";

            /// <summary>
            /// </summary>
            public const string OnConnect = "OnConnectAsync";

            /// <summary>
            /// </summary>
            public const string OnDisconnect = "OnDisconnectAsync";

            /// <summary>
            /// </summary>
            public const string PingRequest = "PingRequestAsync";

            /// <summary>
            /// </summary>
            public const string PingResponse = "PingResponseAsync";

            /// <summary>
            /// </summary>
            public const string RealTimeOffer = "SendOfferAsync";

            /// <summary>
            /// </summary>
            public const string RealTimeRequest = "SendRequestAsync";

            /// <summary>
            /// </summary>
            public const string ReceiveChatNotification = "ReceiveChatNotification";

            /// <summary>
            /// </summary>
            public const string ReceiveMessage = "ReceiveMessage";

            /// <summary>
            /// </summary>
            public const string ReceiveRegenerateTokens = "RegenerateTokens";

            /// <summary>
            /// </summary>
            public const string ReceiveUpdateDashboard = "UpdateDashboard";

            /// <summary>
            /// </summary>
            public const string SendChatNotification = "ChatNotificationAsync";

            /// <summary>
            /// </summary>
            public const string SendMessage = "SendMessageAsync";

            /// <summary>
            /// </summary>
            public const string SendRegenerateTokens = "RegenerateTokensAsync";

            /// <summary>
            /// </summary>
            public const string SendUpdateDashboard = "UpdateDashboardAsync";
        }
        /// <summary>
        /// 
        /// </summary>
        public static class MimeTypes
        {
            public const string OpenXml = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        }
    }
}