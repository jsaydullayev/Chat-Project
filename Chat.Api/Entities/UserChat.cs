﻿using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Entities
{
    public class UserChat
    {
        public Guid Id { get; set; }
        public Guid ToUserId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public User? User { get; set; } 
        public Guid ChatId { get; set; }
        public Chat? Chat {  get; set; } 
    }
}
