using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gallery.DAL.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set;}
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
        public virtual ICollection<Media> Media { get; set; } = new List<Media>();
        public virtual ICollection<Attempts> Attempts { get; set; } = new List<Attempts>();
    }
}