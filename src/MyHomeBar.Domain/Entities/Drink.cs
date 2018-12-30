using System;
using System.Collections.Generic;
using System.Text;

namespace MyHomeBar.Domain.Entities
{
    public class Drink
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Scale Scale { get; set; }
    }

    public enum Scale { normal, good, great, special };
}
