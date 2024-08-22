﻿using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Entities
{
    public class Content
    {
        public int Id { get; set; }
        public string? Caption { get; set; }
        [Required]
        public string FileUrl { get; set; }
        [Required]
        public string? Type { get; set; }
        public int MessageId { get; set; }
        public Message? Message { get; set; }
    }
}
