using System;
using System.Collections.Generic;
using System.Text;

namespace MyHomeBar.Domain.Entities
{
    public class Country
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public ICollection<Drink> Drinks { get; set; }
    }
}
