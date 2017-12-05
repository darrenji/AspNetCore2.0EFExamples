using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Darren.EF.Client.Models.Actors
{
    public class ActorUpdateInputModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Timestamp { get; set; }
    }
}
