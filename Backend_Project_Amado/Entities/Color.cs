﻿namespace Backend_Project_Amado.Entities
{
    public class Color
    {
        public int Id { get; set; }
        public string ColorName { get; set; }
        public List<ProductColor> ProductColors { get; set; }
    }
}
