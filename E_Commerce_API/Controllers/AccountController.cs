namespace E_Commerce_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController(
        IUnitOfWork unitOfWork
        ) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetAllAsync(){
            try{
                if(await _unitOfWork.UserRepository.GetAllAsync()
                    is not List<User> users)
                    return NoContent();
                
                var userDTOs = users.Select(user => new UserDTO{
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Username = user.Username
                }).ToList();

                return Ok(new ApiResponse{
                    Message = null,
                    Data = userDTOs,
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK
                });
            }
            catch(Exception e)
            {
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ApiResponse{
                        Message = e.Message,
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.InternalServerError,
                        Data = null
                    }
                );
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetByIdAsync
            (Guid userId)
        {
            try{
                if(await _unitOfWork.UserRepository.GetByIdAsync(userId)
                    is not User user)
                    return NotFound();
                
                var userDTO = new UserDTO{
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Username = user.Username
                };

                return Ok(new ApiResponse{
                    Message = null,
                    Data = userDTO,
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK
                });
            }
            catch(Exception e)
            {
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ApiResponse{
                        Message = e.Message,
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.InternalServerError,
                        Data = null
                    }
                );
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> RegisterAsync
            (UserRegistrationDTO userDTO)
        {
            try{

                if(!ModelState.IsValid)
                    return BadRequest(new ApiResponse{
                        Message = ExceptionMessages.InvalidParameterData,
                        Data = null,
                        StatusCode = HttpStatusCode.BadRequest,
                        IsSuccess = false
                    });

                if(await _unitOfWork.UserRepository.ValidateEmail
                        (userDTO.Email) is true)
                        return BadRequest(new ApiResponse{
                                Message = ExceptionMessages.User_Email_Invalid,
                                Data = null,
                                StatusCode = HttpStatusCode.BadRequest,
                                IsSuccess = false
                            });

                if(await _unitOfWork.UserRepository.ValidateUsername
                        (userDTO.Email) is true)
                        return BadRequest(new ApiResponse{
                                Message = ExceptionMessages.User_Username_Invalid,
                                Data = null,
                                StatusCode = HttpStatusCode.BadRequest,
                                IsSuccess = false
                            });
                
                if(!_unitOfWork.ValidationRepsitory.IsPhoneNumberValid(userDTO.PhoneNumber))
                    return BadRequest(new ApiResponse{
                        Message = ExceptionMessages.User_Phone_Invalid,
                        Data = null,
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.BadRequest
                    });
                
                if(!_unitOfWork.ValidationRepsitory.IsPasswordValid(
                    userDTO.Password, userDTO.ConfirmPassword
                ))
                    return BadRequest(new ApiResponse{
                        Message = ExceptionMessages.User_PasswordsDontMatch,
                        Data = null,
                        StatusCode = HttpStatusCode.BadRequest,
                        IsSuccess = false
                    });

                User newUser = new(){
                    Id = Guid.NewGuid(),
                    Email = userDTO.Email,
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    Password = userDTO.Password,
                    PhoneNumber = userDTO.PhoneNumber,
                    Username = userDTO.Username
                };

                await _unitOfWork.UserRepository.Insert(newUser);
                await _unitOfWork.SaveChangesAsync();

                return StatusCode(
                    statusCode: (int)HttpStatusCode.Created,
                    value: new ApiResponse(){
                        IsSuccess = true,
                        StatusCode = HttpStatusCode.Created,
                        Message = null,
                        Data = newUser
                    }
                );

            }
            catch(Exception e){
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ApiResponse(){
                        Message = e.Message,
                        StatusCode = HttpStatusCode.InternalServerError,
                        Data = null,
                        IsSuccess = false
                    }
                );
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> LoginAsync
            (UserLoginDTO loginDTO)
        {
            try{
                if(await _unitOfWork.UserRepository.LoginAsync(
                    loginDTO.Username, loginDTO.Password
                ) is not true)
                    return NotFound();

                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ApiResponse{
                        Message = e.Message,
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.InternalServerError,
                        Data = null
                    }
                );
            }
        }
    }
}