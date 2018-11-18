using KorepetycjeNaJuz.Core.DTO.Message;
using KorepetycjeNaJuz.Core.Models;
using KorepetycjeNaJuz.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly KorepetycjeContext _context;
        private readonly Logger _logger;

        public MessagesController(KorepetycjeContext context)
        {
            _context = context;
            _logger = LogManager.GetLogger("apiLogger");
        }

        /// <summary>
        /// Pobiera konwersację z użytkownikiem
        /// </summary>
        /// <param name="id">Id rozmówcy</param>
        /// <returns></returns>
        //[Authorize("Bearer")]
        [HttpGet("{id}")]
        public IActionResult GetConversationWithUser([FromRoute] int id)
        {
            try
            {
                var currentUserId = 1;// User.GetUserId().Value;
                return Ok(_context.Messages.Where(m => m.RecipientId == currentUserId && m.OwnerId == id || m.RecipientId == id && m.OwnerId == currentUserId)
                    .Select(m => new MessageDTO(m)));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during Message download");
                return NotFound();
            }
        }/// <summary>
         /// Pobiera konwersacje użytkownika
         /// </summary>
         /// <returns></returns>
        //[Authorize("Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetConversations()
        {
            try
            {
                var currentUserId = 1;// User.GetUserId().Value;
                var conversation = _context.Messages.AsNoTracking().Where(m => m.RecipientId == currentUserId || m.OwnerId == currentUserId)
                    .GroupBy(m => m.OwnerId == currentUserId ? m.RecipientId : m.OwnerId);
                var usersId = await conversation.Select(c => c.Key).ToListAsync();
                var users = await _context.Users.AsNoTracking().Where(u => usersId.Contains(u.Id)).ToDictionaryAsync(u => u.Id);
                
                return Ok(_context.Messages.AsNoTracking().Where(m => m.RecipientId == currentUserId || m.OwnerId == currentUserId)
                    .GroupBy(m=>m.OwnerId==currentUserId?m.RecipientId:m.OwnerId).Select(g => ConversationDTO.Create(g, users)));
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
        //[Authorize("Bearer")]
        public async Task<IActionResult> SendMessage([FromBody] MessageDTO message)
        {
            try
            {
                var now = DateTime.Now.ToUniversalTime();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (! await _context.Users.AnyAsync(u=>u.Id==message.RecipientId))
                {
                    ModelState.AddModelError("RecipientId", "Użytkownik z takim Id nie istnieje.");
                    return BadRequest(ModelState);
                }

                var currentUserId = 1;// User.GetUserId().Value;

                await _context.Messages.AddAsync(new Message
                {
                    DateOfSending = now,
                    OwnerId = currentUserId,
                    RecipientId = message.RecipientId,
                    Content = message.Content
                });
                await _context.SaveChangesAsync();

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
        //[Authorize("Bearer")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            try
            {
                var currentUserId = 1;// User.GetUserId().Value;
                var message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
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
                _context.Messages.Remove(message);
                await _context.SaveChangesAsync();
                return StatusCode((int)HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Error during Message creation");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}