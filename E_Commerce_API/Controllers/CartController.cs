using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_API.DTOs.CartDTOs;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public CartController(
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetAllAsync(){
            try{
                
                if(await _unitOfWork.CartRepository.GetAllAsync()
                    is not List<Cart> carts)
                    return NoContent();

                var cartDTOs = carts.Select(cart => new CartDTO{
                    Id = cart.Id,
                    CustomerEmail = cart.User!.Email,
                    CustomerName = $"{cart.User!.FirstName} {cart.User!.LastName}",
                    ProductId = cart.ProductId,
                    ProductName = cart.Product!.Name,
                    Quantity = cart.Quantity
                });

                return Ok(new ApiResponse{
                    Message = null,
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Data = cartDTOs
                });

            }
            catch(Exception e){
                return StatusCode(
                    statusCode: (int)HttpStatusCode.InternalServerError,
                    value: new ApiResponse{
                        Message = e.Message,
                        StatusCode = HttpStatusCode.InternalServerError,
                        Data = null,
                        IsSuccess = false
                    }
                );
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> AddToCart(CartInsertDTO cartInsertDTO){
            try{
                if(!ModelState.IsValid)
                    return BadRequest(new ApiResponse{
                        Message = ExceptionMessages.InvalidParameterData,
                        StatusCode = HttpStatusCode.BadRequest,
                        Data = null,
                         IsSuccess = false
                    });

                Cart cart = new(){
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    ProductId = cartInsertDTO.Product_Id,
                    Quantity = cartInsertDTO.Quantity,
                    UserId = cartInsertDTO.UserId,
                };

                await _unitOfWork.CartRepository.Insert(cart);
                await _unitOfWork.SaveChangesAsync();

                return Ok(new ApiResponse{
                    StatusCode = HttpStatusCode.OK,
                    Message = null,
                    IsSuccess = true,
                    Data = cart
                });
            }
            catch(Exception e){
                return StatusCode(
                    statusCode: (int)HttpStatusCode.InternalServerError,
                    value: new ApiResponse{
                        Message = e.Message,
                        StatusCode = HttpStatusCode.InternalServerError,
                        Data = null,
                        IsSuccess = false
                    }
                );
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Edit(Guid cartId, CartUpdateDTO cartUpdateDTO)
        {
            try{
                if(!ModelState.IsValid)
                    return BadRequest(new ApiResponse{
                        Message = ExceptionMessages.InvalidParameterData,
                        StatusCode = HttpStatusCode.BadRequest,
                        Data = null,
                        IsSuccess = false
                    });

                if(await _unitOfWork.CartRepository.GetByIdAsync(cartId)
                    is not Cart oldCart)
                    return NotFound();
                
                oldCart.Quantity = cartUpdateDTO.Quantity;
                oldCart.ProductId = cartUpdateDTO.ProductId;

                _unitOfWork.CartRepository.Update(oldCart);
                await _unitOfWork.SaveChangesAsync();

                return Ok(new ApiResponse{
                    Message = null,
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Data = oldCart
                });
            }
            catch(Exception e){
                return StatusCode(
                    statusCode: (int)HttpStatusCode.InternalServerError,
                    value: new ApiResponse{
                        Message = e.Message,
                        StatusCode = HttpStatusCode.InternalServerError,
                        Data = null,
                        IsSuccess = false
                    }
                );
            }
        }

        [HttpDelete("cartId:guid")]
        public async Task<ActionResult<ApiResponse>> RemoveCart(Guid cartId)
        {
            try{
                await _unitOfWork.CartRepository.Delete(cartId);
                await _unitOfWork.SaveChangesAsync();

                return Ok(new ApiResponse{
                    Message = null,
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Data = null
                });
            }
            catch(Exception e){
                return StatusCode(
                    statusCode: (int)HttpStatusCode.InternalServerError,
                    value: new ApiResponse{
                        Message = e.Message,
                        StatusCode = HttpStatusCode.InternalServerError,
                        Data = null,
                        IsSuccess = false
                    }
                );
            }
        }

    }
}