using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CI.Business.Models
{
    public partial class User
    {
        public User()
        {
            this.Cars = new List<Car>();
        }

        public System.Guid UserId { get; set; }
        [DisplayName("Email Address")]
        [Required]
        public string Email { get; set; }
         [Required]
        public string Password { get; set; }
        [DisplayName("First Name")]
        [Required]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [Required]
        public string LastName { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime DateCreated { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}
