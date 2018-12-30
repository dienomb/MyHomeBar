using System;
using System.Collections.Generic;
using System.Text;

namespace MyHomeBar.Api.IntegrationTest.Specs
{
    public static class MyHomeBarAPI
    {
        static string BASEURI = "api/v1/MyHomebar/ViewDrink";

        public static class View
        {
            public static string Drink(string name)
            {
                return $"{BASEURI }?drinkName={name}";
            }
        }

        public static class Post
        {
            public static string Bar()
            {
                return BASEURI;
            }
        }
    }
}
