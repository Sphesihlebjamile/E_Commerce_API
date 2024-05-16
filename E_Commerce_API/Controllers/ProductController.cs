using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using E_Commerce_API.DTOs;
using E_Commerce_API.Helpers;
using E_Commerce_API.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController(
        IUnitOfWork unitOfWork
        )
                : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetAllAsync(){

            try{
                if(await _unitOfWork.ProductRepository.GetAllAsync()
                is not List<Product> products){
                    return NoContent();
                }

                List<ProductsRequestDTO> productDTOs = products.Select
                    (prod => new ProductsRequestDTO{
                        CategoryId = prod.CategoryId,
                        Id = prod.Id,
                        Description = prod.Description,
                        Name = prod.Name,
                        Price = prod.Price
                    })
                    .ToList();
                
                return Ok(new ApiResponse{
                    Message = null,
                    Data = productDTOs,
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK
                });
            }
            catch(Exception e){
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ApiResponse{
                        Message = e.Message,
                        StatusCode = HttpStatusCode.InternalServerError,
                        Data = null,
                        IsSuccess = false
                    });
            }

        }

        [HttpGet("id:guid")]
        public async Task<ActionResult<ApiResponse>> GetByIdAsync(Guid id)
        {
            try{
                if(await _unitOfWork.ProductRepository.GetById(id)
                    is not Product product){
                        return NoContent();
                    }
                
                var productDTO = new ProductsRequestDTO{
                    Id = product.Id,
                    Name = product.Name,
                    CategoryId = product.CategoryId,
                    Price = product.Price,
                    Description = product.Description
                };

                return Ok(new ApiResponse{
                    Message = null,
                    Data = productDTO,
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK
                });
            }
            catch(Exception e){
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ApiResponse{
                        Message = e.Message,
                        StatusCode = HttpStatusCode.InternalServerError,
                        Data = null,
                        IsSuccess = false
                    });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> AddProduct(ProductInsertDTO productInsertDTO)
        {
            if(!ModelState.IsValid){
                return BadRequest(new ApiResponse{
                    Message = ExceptionMessages.InvalidParameterData,
                    Data = null,
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            try{

                var newProduct = new Product{
                    Id = Guid.NewGuid(),
                    Name = productInsertDTO.Name,
                    CategoryId = productInsertDTO.CategoryId,
                    Description = productInsertDTO.Description,
                    Price = productInsertDTO.Price,
                };

                await _unitOfWork.ProductRepository.Insert(newProduct);
                await _unitOfWork.SaveChangesAsync();

                return StatusCode(
                    statusCode: (int)HttpStatusCode.Created,
                    value: new ApiResponse{
                        Message = null,
                        Data = newProduct,
                        StatusCode = HttpStatusCode.Created,
                        IsSuccess = true
                    }
                );

            }
            catch(Exception e)
            {
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ApiResponse{
                        Message = e.Message,
                        StatusCode = HttpStatusCode.InternalServerError,
                        Data = null,
                        IsSuccess = false
                    });
            }
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse>> Edit(Guid id, ProductsRequestDTO updatedProduct)
        {
            try{

                if(id != updatedProduct.Id)
                    return BadRequest(new ApiResponse{
                        Message = ExceptionMessages.Product_Edit_IdsDontMatch,
                        Data = null,
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.BadRequest
                    });

                if(await _unitOfWork.ProductRepository.GetById(id)
                    is not Product oldProduct)
                    {
                        return NotFound();
                    }
                
                oldProduct.Name = updatedProduct.Name;
                oldProduct.CategoryId = updatedProduct.CategoryId;
                oldProduct.Description = updatedProduct.Description;
                oldProduct.Price = updatedProduct.Price;

                _unitOfWork.ProductRepository.Update(oldProduct);
                await _unitOfWork.SaveChangesAsync();

                return Ok(new ApiResponse{
                    Message = null,
                    IsSuccess = true,
                    Data = oldProduct,
                    StatusCode = HttpStatusCode.OK
                });
            }
            catch(Exception e){
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ApiResponse{
                        Message = e.Message,
                        StatusCode = HttpStatusCode.InternalServerError,
                        Data = null,
                        IsSuccess = false
                    });
            }
        }

        [HttpDelete("id:guid")]
        public async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            try{

                if(await _unitOfWork.ProductRepository.GetById(id)
                    is not Product)
                    {
                        return NotFound();
                    }
                
                await _unitOfWork.ProductRepository.Delete(id);
                await _unitOfWork.SaveChangesAsync();

                return Ok(new ApiResponse{
                    StatusCode = HttpStatusCode.OK,
                    Data = null,
                    Message = null,
                    IsSuccess = true
                });

            }
            catch(Exception e)
            {
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ApiResponse{
                        Message = e.Message,
                        StatusCode = HttpStatusCode.InternalServerError,
                        Data = null,
                        IsSuccess = false
                    });
            }
        }
    }
}