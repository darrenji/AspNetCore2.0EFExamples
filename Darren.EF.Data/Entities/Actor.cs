using System;
using System.Collections.Generic;
using System.Text;

namespace Darren.EF.Data.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public byte[] Timestamp { get; set; }
    }
}
