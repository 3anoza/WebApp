using System;

namespace App.Core
{
    public class Logging
    {
        Logging(string message)
        {
            using (System.IO.FileStream stream = new System.IO.FileStream("C:/Users/%User%/.debug/debug.msg",System.IO.FileMode.CreateNew,System.IO.FileAccess.Write))
            {
                stream.Write(System.Text.Encoding.UTF8.GetBytes(message), 0, message.Length);
            }
        }
    }
}
