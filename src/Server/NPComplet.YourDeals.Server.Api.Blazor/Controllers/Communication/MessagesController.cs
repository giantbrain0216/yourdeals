// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessagesController.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Communication
{
    #region

    using Infrastructure.Repositories.Interfaces;
    using Infrastructure.Services.Interfaces;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using NPComplet.YourDeals.Domain.Shared.Communication;
    using NPComplet.YourDeals.Domain.Shared.Users;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    ///     The Messages Controller.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiVersion("1.0")]
    public class MessagesController : TypeBaseController<Message>
    {
        private readonly IMessageService _messageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagesController"/> class.
        ///     Message controller
        /// </summary>
        /// <param name="userManager">
        /// The user manager.
        /// </param>
        /// <param name="unitOfWork">
        /// The unit fo work.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="messageService">The message service.</param>
        public MessagesController(
            UserManager<User> userManager,
            IUnitOfWork unitOfWork,
            ILogger<MessagesController> logger, IMessageService messageService)
            : base(userManager, unitOfWork, logger)
        {
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
        }

        /// <summary>
        /// Get user wise chat history for a room.
        /// </summary>
        /// <param name="contactId">
        /// </param>
        /// <param name="roomId">The deal or proposition messages post identifier.</param>
        /// <returns>
        /// Status 200 OK
        /// </returns>
        [HttpGet("{contactId}/{roomId:guid}")]
        public async Task<IActionResult> GetChatHistoryAsync(string contactId, Guid roomId)
        {
            var user = await UserManager.GetUserAsync(HttpContext.User);
            var messages = await UnitOfWork.GetRepository<Message>().GetAllAsync(
                message => (message.PropositionMessagesPostId == roomId
                            || message.DealMessagesPostId == roomId)
                           && (message.OwnerId == user.Id && message.ToUserId == Guid.Parse(contactId)
                               || message.OwnerId == Guid.Parse(contactId) && message.ToUserId == user.Id),
                "SendingTimeUtc",
                "MessageCore");

            return Ok(messages);
        }

        /// <summary>
        ///     Get Messages For Main Messages screen List
        /// </summary>
        /// <returns>Messages.</returns>
        [HttpGet]
        public async Task<IActionResult> GetUserAllChatHistory()
        {
            var user = await UserManager.GetUserAsync(HttpContext.User);

            return Ok(await _messageService.GetUserChatHistory(user.Id));
        }

        /// <summary>
        /// Save Chat Message
        /// </summary>
        /// <param name="message">
        /// </param>
        /// <returns>
        /// Status 200 OK
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> SaveMessageAsync([FromBody] Message message)
        {
            var user = await this.UserManager.GetUserAsync(this.HttpContext.User);

            message.OwnerId = user.Id;
            message.Owner = user;

            await this.UnitOfWork.GetRepository<Message>().AddAsync(message);
            await this.UnitOfWork.CommitAsync(user.Id.ToString());

            return this.Ok(message);
        }

        /// <summary>
        /// Get room messages.
        /// </summary>
        /// <param name="roomId">The room identifier.</param>
        /// <returns>Messages for the room.</returns>
        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetRoomMessages(Guid roomId)
        {
            var user = await UserManager.GetUserAsync(HttpContext.User);

            var messagesToClose = await UnitOfWork.GetRepository<Message>().GetAllAsync(message =>
                message.DealMessagesPostId == roomId
                && (message.OwnerId == user.Id || message.ToUserId == user.Id));

            return Ok(messagesToClose.ToList());
        }

        /// <summary>
        /// Close message for a room.
        /// </summary>
        /// <param name="messagesToClose">The messages to close.</param>
        [HttpPut]
        public async Task<IActionResult> CloseMessages([FromBody] IEnumerable<Message> messagesToClose)
        {
            if (messagesToClose == null || !messagesToClose.Any())
            {
                return NoContent();
            }

            foreach (var message in messagesToClose)
            {
                message.IsOpen = false;
                message.CloseDate = DateTime.Now;
            }

            UnitOfWork.GetRepository<Message>().UpdateRange(messagesToClose);
            await UnitOfWork.CommitAsync((await UserManager.GetUserAsync(HttpContext.User)).Id.ToString());

            return NoContent();
        }

    }
}