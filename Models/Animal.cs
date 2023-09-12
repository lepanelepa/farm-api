using System;
using System.ComponentModel.DataAnnotations;

namespace farm_api.Models
{
    public class Animal
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }      
    }
}
