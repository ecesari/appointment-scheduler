﻿using System.ComponentModel.DataAnnotations;

namespace iPractice.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}