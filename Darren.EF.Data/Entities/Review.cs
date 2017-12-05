using System;
using System.Collections.Generic;
using System.Text;

namespace Darren.EF.Data.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public DateTime Dated { get; set; }
        public string Summary { get; set; }
        public int MovieId { get; set; }
    }
}
