using System;
using System.Collections.Generic;
using System.Linq;

namespace MyHomeBar.Domain.Entities
{
    //public enum Scale { normal, good, great, special };

    public sealed class Level
    {
        public static readonly Level Normal = new Level(1, nameof(Normal));
        public static readonly Level Good = new Level(2, nameof(Good));
        public static readonly Level Great = new Level(2, nameof(Great));
        public static readonly Level Special = new Level(2, nameof(Special));


        public readonly int Id;
        public readonly string Name;

        private Level(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static IReadOnlyCollection<Level> GetLevels()
        {
            return new[] { Normal, Good, Great, Special };
        }

        public static Level FindBy(int id)
        {
            var Level = GetLevels().SingleOrDefault(s => s.Id == id);

            if (Level == null)
            {
                throw new ArgumentOutOfRangeException($"Invalid id {id}");
            }

            return Level;
        }
    }

}
