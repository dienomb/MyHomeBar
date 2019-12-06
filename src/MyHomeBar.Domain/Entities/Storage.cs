using System.Collections.Generic;

namespace MyHomeBar.Domain.Entities
{
    public class Storage
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Drink> Drinks { get; set; }
    }
}