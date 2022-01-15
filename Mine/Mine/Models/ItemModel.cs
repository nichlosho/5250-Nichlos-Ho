﻿using System;

namespace Mine.Models
{
    /// <summary>
    /// Items for characters and monsters to use
    /// </summary>
    public class ItemModel
    {
        //The Id for the item
        public string Id { get; set; }

        //The display text for the item
        public string Text { get; set; }

        //The Description for the item
        public string Description { get; set; }

        //The value of the item +9 damage
        public int Value { get; set; } = 0;
    }
}