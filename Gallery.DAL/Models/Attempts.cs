using System;

namespace Gallery.DAL.Models
{
    public class Attempts
    {
        public long Id { get; private set; }
        
        public DateTime TimeStamp { get; private set; }
        
        public bool Success { get; private set; }
        
        public string IpAddress { get; private set; }
        
        public int UserId { get; private set; }
        
        public virtual User User { get; private set; }
    }
}