﻿using MyHomeBar.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MyHomeBar.Api.TestRepository
{

    public class InMemoryProductsRepository : IDrinksRepository
    {
        private readonly List<Drink> _products = new List<Drink>
        {
            new Drink
            {
                Id = 1,
                Name = "Zuica",
                Scale = Scale.special
            },
            new Drink
            {
                Id = 2,
                Name = "Vodka",
                Scale = Scale.normal
            },
        };

        public Drink Get(string drinkName)
        {
            return _products.FirstOrDefault(p => p.Name == drinkName);
        }
    }

    public interface IDrinksRepository
    {
        Drink Get(string productNumber);
    }

    
}