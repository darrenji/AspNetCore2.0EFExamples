using System;
using System.Collections.Generic;
using System.Text;

namespace Darren.EF.Data.Entities
{
    public class MovieActor
    {
        public int Id { get; set; }
        public int MoveId { get; set; }
        public int ActorId { get; set; }
        public string Role { get; set; }
    }
}
