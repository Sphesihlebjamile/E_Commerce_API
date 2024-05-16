using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.DTOs.CartDTOs
{
    public class CartInsertDTO
    {
        public int Quantity { get; set; }
        public Guid Product_Id { get; set; }
        public Guid UserId { get; set; }
    }
}