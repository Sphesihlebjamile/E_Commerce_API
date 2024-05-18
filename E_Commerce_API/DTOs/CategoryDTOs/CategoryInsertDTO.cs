using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.DTOs.CategoryDTOs
{
    public class CategoryInsertDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}