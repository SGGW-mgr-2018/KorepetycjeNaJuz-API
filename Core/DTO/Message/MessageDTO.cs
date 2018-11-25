﻿using System;
using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Core.DTO.Message
{
    public class MessageDTO
    {
        public MessageDTO() { }
        public MessageDTO(Models.Message m)
        {
            Id = m.Id;
            RecipientId = m.RecipientId;
            Content = m.Content;
            OwnerId = m.OwnerId;
            DateOfSending = m.DateOfSending;
        }

        public int Id { get; }
        [Range(1, int.MaxValue, ErrorMessage = "Pole musi być liczbą całkowitą większą niż 0.")]
        public int RecipientId { get; set; }
        public string Content { get; set; }
        public int OwnerId { get; }
        /// <summary>
        /// UTC datetime
        /// </summary>
        public DateTime DateOfSending { get; }
    }
}