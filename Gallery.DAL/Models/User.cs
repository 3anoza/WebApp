using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gallery.DAL.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int _userId { get; set;}
        public string _email { get; set; }
        public string _password { get; set; }
    }
}