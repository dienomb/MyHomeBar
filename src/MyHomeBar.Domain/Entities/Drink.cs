using System;
using System.Collections.Generic;
using System.Text;

namespace MyHomeBar.Domain.Entities
{
    public class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUri { get; set; }
        public decimal Volume { get; set; }
        public decimal Alchool { get; set; } 
        public Storage Location { get; set; }
        public Country Origin { get; set; }
        public Price Price { get; set; }
        public Scale Scale { get; set; }
        public bool IsFinish { get; set; }
    }
}
