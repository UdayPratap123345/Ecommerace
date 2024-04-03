using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookRental.Model
{
   public  class Role
    {
        [Key]
        public int RoleId { get; set; }

        public string RoleName { get; set; } = string.Empty;

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public ICollection<User>? Users { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    }
}
