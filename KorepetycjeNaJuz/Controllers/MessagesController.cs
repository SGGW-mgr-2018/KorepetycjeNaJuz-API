using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KorepetycjeNaJuz.Core.Models;
using KorepetycjeNaJuz.Infrastructure;
using KorepetycjeNaJuz.Core.DTO.Message;
using Microsoft.AspNetCore.Authorization;
using KorepetycjeNaJuz.Core.Helpers;
using System.Net;
using NLog;

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

        // GET: api/Messages
        [HttpGet]
        public IEnumerable<Message> GetMessages()
        {
            return _context.Messages;
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var message = await _context.Messages.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        // PUT: api/Messages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage([FromRoute] int id, [FromBody] Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != message.Id)
            {
                return BadRequest();
            }

            _context.Entry(message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        /// <summary>
        /// Wysyła wiadomość
        /// </summary>
        /// <response code="201">Poprawnie wysłano wiadomość</response>
        /// <param name="message"></param>
        /// <returns></returns>
        // POST: api/Messages
        [HttpPost]
        [ProducesResponseType(201)]
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

                if (!_context.Users.Any(u=>u.Id==message.RecipientId))
                {
                    ModelState.AddModelError("RecipientId", "Użytkownik z takim Id nie istnieje.");
                    return BadRequest(ModelState);
                }

                var currentUserId = 1;// User.GetUserId().Value;

                _context.Messages.Add(new Message
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

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return Ok(message);
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }
    }
}