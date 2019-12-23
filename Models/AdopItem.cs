using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptionApi.Models
{
    public class AdopItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Desc { get; set; }
        public int Age { get; set; }
        public string Category { get; set; }
        public string Gender { get; set; }
        public string Location { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Image> ItemImage { get; set; }
        public int UserId { get; set; }

    }
}
