﻿using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Contact : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}