#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using NPComplet.YourDeals.Domain.Shared.Communication;
using NPComplet.YourDeals.Domain.Shared.Shared;
using NPComplet.YourDeals.Domain.Shared.Users;
using NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces;
using NPComplet.YourDeals.Server.Infrastructure.Services.Interfaces;

#endregion

namespace NPComplet.YourDeals.Server.Infrastructure.Services.Classes.Communication
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IHostingEnvironment _environment;


        public MessageService(IUnitOfWork unitOfWork, IHostingEnvironment environment)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _environment = environment;
        }

        /// <inheritdoc />
        public async Task<IList<Message>> GetUserChatHistory(Guid currentUserId)
        {
            var briefMessages = new List<Message>();

            var messages = await _unitOfWork.GetRepository<Message>().GetAllAsync(
                p => (p.ToUserId == currentUserId || p.OwnerId == currentUserId) && p.IsOpen,
                null,
                "MessageCore",
                "ToUser",
                "Owner");
            var enumerable = messages as Message[] ?? messages.ToArray();
            foreach (var message in enumerable)
            {
                await SetTheProfileForTheUser(message.Owner);
                await SetTheProfileForTheUser(message.ToUser);
            }

            if (!enumerable.Any()) return briefMessages;

            var groupedAndOrderedMessages = GetGroupedAndOrderedMessages(enumerable);

            foreach (var msg in groupedAndOrderedMessages)
                if (msg.ToUserId == currentUserId)
                {
                    var foundLatestMsg = briefMessages.SingleOrDefault(p =>
                        p.ToUserId == msg.OwnerId
                        && p.DealMessagesPostId == msg.DealMessagesPostId
                        && p.PropositionMessagesPostId == msg.PropositionMessagesPostId);

                    ManageBriefMessage(foundLatestMsg, msg, briefMessages);
                }
                else if (msg.OwnerId == currentUserId)
                {
                    var foundLatestMsg = briefMessages.SingleOrDefault(p =>
                        p.OwnerId == msg.ToUserId
                        && p.DealMessagesPostId == msg.DealMessagesPostId
                        && p.PropositionMessagesPostId == msg.PropositionMessagesPostId);

                    ManageBriefMessage(foundLatestMsg, msg, briefMessages);
                }

            return briefMessages;
        }

        private async Task SetTheProfileForTheUser(User user)
        {
            user.Profile = await _unitOfWork.GetRepository<Profile>().GetByIdAsync(user.ProfileId);
            user.Profile.ProfileImage =
                await _unitOfWork.GetRepository<ProfileImage>().GetByIdAsync(user.Profile.ProfileImageId);
            try
            {
                user.Profile.ProfileImage.Data = await convertLocalFileToArrOfByte(
                _environment.WebRootPath +
                user.Profile.ProfileImage.LocalPath +
                user.Profile.ProfileImage.FileName +
                "." + user.Profile.ProfileImage.FileExtension);
            }
            catch
            {

                user.Profile.ProfileImage.Data = await convertLocalFileToArrOfByte(
                _environment.WebRootPath +
                "/AppData/Users/ProfilePictures/" +
                "DefaultProfileImg" +
                "." + "jpg");
            }
            if (user.Profile.AddressId != null)
            {
                user.Profile.Address = await _unitOfWork.GetRepository<Address>().GetByIdAsync(user.Profile.AddressId);
                user.Profile.Address.Location = await _unitOfWork.GetRepository<Location>()
                    .GetByIdAsync(user.Profile.Address.LocationId);
            }
        }

        private Task<byte[]> convertLocalFileToArrOfByte(string localPath)
        {
            
            using var fs = new FileStream(localPath, FileMode.Open, FileAccess.Read);
            // Create a byte array of file stream length
            var bytes = File.ReadAllBytes(localPath);
            //Read block of bytes from stream into the byte array
            fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
            //Close the File Stream
            fs.Close();
            return Task.FromResult(bytes); //return the byte data
        }

        private static IEnumerable<Message> GetGroupedAndOrderedMessages(IEnumerable<Message> messages)
        {
            var previewDealsMessages = messages
                .Where(m => m.DealMessagesPostId != null)
                .GroupBy(m => new { m.OwnerId, m.ToUserId, m.DealMessagesPostId })
                .Select(gm => gm.OrderByDescending(m => m.SendingTimeUtc).First());

            var previewPropositionsMessages = messages
                .Where(m => m.PropositionMessagesPostId != null)
                .GroupBy(m => new { m.OwnerId, m.ToUserId, m.PropositionMessagesPostId })
                .Select(gm => gm.OrderByDescending(m => m.SendingTimeUtc).First());

            return previewDealsMessages.Concat(previewPropositionsMessages).OrderBy(m => m.SendingTimeUtc);
        }

        private static void ManageBriefMessage(Message foundLatestMsg, Message msg, ICollection<Message> briefMessages)
        {
            if (foundLatestMsg != null)
            {
                if (foundLatestMsg.SendingTimeUtc > msg.SendingTimeUtc) return;

                briefMessages.Remove(foundLatestMsg);
                briefMessages.Add(msg);
            }
            else
            {
                briefMessages.Add(msg);
            }
        }
    }
}