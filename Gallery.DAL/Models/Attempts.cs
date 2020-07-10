using System;

namespace Gallery.DAL.Models
{
    public class Attempts
    {
        public long Id { get;  set; }
        
        public DateTime TimeStamp { get;  set; }
        
        public bool Success { get;  set; }
        
        public string IpAddress { get;  set; }
        
        public int UserId { get;  set; }
        
        public virtual User User { get;  set; }
    }
}