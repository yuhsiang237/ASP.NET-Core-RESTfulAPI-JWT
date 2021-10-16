using System;
using System.Collections.Generic;

#nullable disable

namespace TodoAPI.Models
{
    public partial class Todo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? IsComplete { get; set; }
        public int? IsDeleted { get; set; }
    }
}
