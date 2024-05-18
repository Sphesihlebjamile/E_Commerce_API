using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_API.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetAllCategories(){
            try{
                if(await _unitOfWork.CategoryRepository.GetAllAsync()
                    is not List<Category> categories)
                    return NoContent();
                
                List<CategoryDTO> categoriesWithProduct = 
                    categories.Select(category => new CategoryDTO{
                            CategoryName = category.Name,
                            ProductNames = category.Products?.
                                    Select(prod => prod.Name).ToList() ?? [],
                            Id = category.Id
                    }).ToList();
                
                return Ok(new ApiResponse{
                    Message = null,
                    StatusCode = HttpStatusCode.OK,
                    Data = categoriesWithProduct,
                    IsSuccess = true
                });
            }
            catch(Exception e){
                return StatusCode(
                    statusCode: (int)HttpStatusCode.InternalServerError,
                    value: new ApiResponse(){
                        Message = e.Message,
                        Data = null,
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.InternalServerError
                    }
                );
            }
        }

        [HttpGet("categoryId:guid")]
        public async Task<ActionResult<ApiResponse>> GetById(Guid categoryId){
            try{
                if(await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId)
                    is not Category category)
                    return NotFound();
                
                var categoryWithProducts = new CategoryDTO{
                    Id = category.Id,
                    CategoryName = category.Name,
                    ProductNames = category.Products?.Select(prod => prod.Name).ToList() 
                        ?? []
                };

                return Ok(new ApiResponse{
                    Message = null,
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Data = categoryWithProducts
                });
            }
            catch(Exception e){
                return StatusCode(
                    statusCode: (int)HttpStatusCode.InternalServerError,
                    value: new ApiResponse(){
                        Message = e.Message,
                        Data = null,
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.InternalServerError
                    }
                );
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> AddCategory(CategoryInsertDTO categoryDTO){
            try{
                if(!ModelState.IsValid)
                    return BadRequest(new ApiResponse{
                        Message = ExceptionMessages.InvalidParameterData,
                        Data = null,
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.BadRequest
                    });

                Category newCategory = new(){
                    Id = Guid.NewGuid(),
                    Name = categoryDTO.Name
                };
                
                await _unitOfWork.CategoryRepository.Insert(newCategory);
                await _unitOfWork.SaveChangesAsync();

                return StatusCode(
                    statusCode: (int)HttpStatusCode.Created,
                    value: new ApiResponse{
                        Data = new CategoryDTO{
                            Id = newCategory.Id,
                            CategoryName = newCategory.Name,
                            ProductNames = []
                        },
                        Message = null,
                        StatusCode = HttpStatusCode.Created,
                        IsSuccess = true
                });
            }
            catch(Exception e){
                return StatusCode(
                    statusCode: (int)HttpStatusCode.InternalServerError,
                    value: new ApiResponse(){
                        Message = e.Message,
                        Data = null,
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.InternalServerError
                    }
                );
            }
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse>> Edit(Guid id, CategoryUpdateDTO updatedCategory){
            try{
                if(!ModelState.IsValid)
                    return BadRequest(new ApiResponse{
                        Message = ExceptionMessages.InvalidParameterData,
                        Data = null,
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.BadRequest
                    });
                
                if(id != updatedCategory.Id)
                    return BadRequest(new ApiResponse{
                        Message = ExceptionMessages.Edit_IdsDontMatch,
                        Data = null,
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.BadRequest
                    });
                
                if(await _unitOfWork.CategoryRepository.GetByIdAsync(id)
                    is not Category oldCategory)
                    return NotFound();
                
                oldCategory.Name = updatedCategory.Name;

                _unitOfWork.CategoryRepository.Update(oldCategory);
                await _unitOfWork.SaveChangesAsync();

                return Ok(new ApiResponse{
                    Data = new CategoryDTO{
                        CategoryName = oldCategory.Name,
                        Id = oldCategory.Id,
                        ProductNames = oldCategory.Products?
                            .Select(prod => prod.Name).ToList() 
                            ?? [],
                    },
                    Message = null,
                    StatusCode = HttpStatusCode.OK,
                });
            }
            catch(Exception e){
                return StatusCode(
                    statusCode: (int)HttpStatusCode.InternalServerError,
                    value: new ApiResponse(){
                        Message = e.Message,
                        Data = null,
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.InternalServerError
                    }
                );
            }
        }

        [HttpDelete("categoryId:guid")]
        public async Task<ActionResult<ApiResponse>> Remove(Guid categoryId){
            try{
                await _unitOfWork.CategoryRepository.Delete(categoryId);
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
                    value: new ApiResponse(){
                        Message = e.Message,
                        Data = null,
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.InternalServerError
                    }
                );
            }
        }
    }
}