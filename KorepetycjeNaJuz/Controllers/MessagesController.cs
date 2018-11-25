using KorepetycjeNaJuz.Core.DTO.Message;
using KorepetycjeNaJuz.Core.Helpers;
using KorepetycjeNaJuz.Core.Models;
using KorepetycjeNaJuz.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using KorepetycjeNaJuz.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly Logger _logger;
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;

        public MessagesController(IMessageService messageService, IUserService userService)
        {
            _logger = LogManager.GetLogger("apiLogger");
            _messageService = messageService;
            _userService = userService;
        }

        /// <summary>
        /// Pobiera konwersację z użytkownikiem
        /// </summary>
        /// <param name="id">Id rozmówcy</param>
        /// <returns></returns>
        [Authorize("Bearer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConversationWithUser([FromRoute] int id)
        {
            try
            {
                var currentUserId = User.GetUserId().Value;
                return Ok((await _messageService.GetConversationWithUserAsync(currentUserId, id))
                    .Select(m => new MessageDTO(m)));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during Message download");
                return NotFound();
            }
        }
        /// <summary>
        /// Pobiera konwersacje użytkownika
        /// </summary>
        /// <returns></returns>
        [Authorize("Bearer")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ConversationDTO>), 200), ProducesResponseType(404)]
        public async Task<IActionResult> GetConversations()
        {
            try
            {
                var currentUserId = User.GetUserId().Value;
                var users = await _messageService.GetInterlocutorsAsync(currentUserId);//_context.Users.AsNoTracking().Where(u => usersId.Contains(u.Id)).ToDictionaryAsync(u => u.Id);


                return Ok((await _messageService.GetConversation(currentUserId)).Select(g => ConversationDTO.Create(g, users)));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during conversation download");
                return NotFound();
            }
        }
        /// <summary>
        /// Wysyła wiadomość
        /// </summary>
        /// <response code="400">Niepoprawne argumenty</response>
        /// <response code="201">Poprawnie wysłano wiadomość</response>
        /// <param name="message"></param>
        /// <returns></returns>
        // POST: api/Messages
        [HttpPost]
        [ProducesResponseType(201), ProducesResponseType(400)]
        [Authorize("Bearer")]
        public async Task<IActionResult> SendMessage([FromBody] MessageDTO message)
        {
            try
            {
                var now = DateTime.Now.ToUniversalTime();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (! await _userService.IsUserExistsAsync(message.RecipientId))
                {
                    ModelState.AddModelError("RecipientId", "Użytkownik z takim Id nie istnieje.");
                    return BadRequest(ModelState);
                }

                var currentUserId = User.GetUserId().Value;

                await _messageService.AddMessageAsync(new Message
                {
                    DateOfSending = now,
                    OwnerId = currentUserId,
                    RecipientId = message.RecipientId,
                    Content = message.Content
                });

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Error during Message creation");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        /// <summary>
        /// Usuwa wiadomość o podanym id
        /// </summary>
        /// <param name="id">Id wiadomości</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200), ProducesResponseType(400)]
        [Authorize("Bearer")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            try
            {
                var currentUserId = User.GetUserId().Value;
                var message = await _messageService.GetMessageAsync(id);
                if (message==null)
                {
                    ModelState.AddModelError("id", "Wiadomość z podanym id nie istnieje.");
                    return BadRequest(ModelState);
                }
                if (message.RecipientId != currentUserId && message.OwnerId != currentUserId)
                {
                    ModelState.AddModelError("id", "Użytkownik nie może usuwać cudze wiadomości.");
                    return BadRequest(ModelState);
                }
                await _messageService.RemoveAsync(message.Id);
                return StatusCode((int)HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Error during Message removal");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}