using BooksBackend.BusinessLayer.IService;
using BooksBackend.DataLayer.Dto.Auth;
using BooksBackend.DataLayer.Dto.General;
using BooksBackend.DataLayer.Dto.Users;
using BooksBackend.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksBackend.BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IAuthService authService, IUnitOfWork unitOfWork)
        {
            _authService = authService;
            _unitOfWork = unitOfWork;

        }

        public async Task<ResponseModel<ICollection<GetUserDto2>>> GetUsersByEmailOrName(string userNameEmail)
        {
            var users = await _unitOfWork.Users.GetUsersByEmailOrName(userNameEmail);
            if (users == null || !users.Any())
            {
                return new ResponseModel<ICollection<GetUserDto2>>
                {
                    IsSuccess = false,
                    Message = "No users found."
                };
            }
            var userDtos = _unitOfWork.Mapper.Map<ICollection<GetUserDto2>>(users);
            return new ResponseModel<ICollection<GetUserDto2>>
            {
                IsSuccess = true,
                Result = userDtos
            };
        }

        public async Task<ResponseModel<GetUserDto>> RegisterAsync(string username, string email, string password)
        {
            var existingUser = await _unitOfWork.Users.FindAsync(u => u.Email == email);
            if (existingUser != null)
            {
                return new ResponseModel<GetUserDto>
                {
                    IsSuccess = false,
                    Message = "User already exists."
                };
            }

            var hashedPassword = _authService.HashPassword(password);
            var newUser = new User
            {
                Username = username,
                Email = email,
                PasswordHash = hashedPassword,
                UserType = UserType.Client
            };

            await _unitOfWork.Users.AddAsync(newUser);
            await _unitOfWork.SaveChangesAsync();

            var userDto = _unitOfWork.Mapper.Map<GetUserDto>(newUser);
            return new ResponseModel<GetUserDto>
            {
                IsSuccess = true,
                Result = userDto
            };
        }

        public async Task<ResponseModel<TokenResponse>> LoginAsync(string email, string password)
        {
            var user = await _authService.AuthenticateAsync(email, password);
            if (user == null || !_authService.VerifyPassword(password, user.PasswordHash))
            {
                return new ResponseModel<TokenResponse>
                {
                    IsSuccess = false,
                    Message = "Invalid credentials."
                };
            }

            var accessToken = _authService.GenerateJwtToken(user);
            var refreshToken = _authService.GenerateRefreshToken();

            user.RefreshToken = _authService.HashPassword(refreshToken);
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return new ResponseModel<TokenResponse>
            {
                IsSuccess = true,
                Result = new TokenResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                }
            };
        }

        public async Task<ResponseModel<TokenResponse>> RefreshTokenAsync(string refreshToken)
        {
            var hashedToken = _authService.HashPassword(refreshToken);

            var user = await _unitOfWork.Users.FindByRefreshTokenAsync(hashedToken);

            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return new ResponseModel<TokenResponse>
                {
                    IsSuccess = false,
                    Message = "Invalid or expired refresh token."
                };
            }

            var newAccessToken = _authService.GenerateJwtToken(user);
            var newRefreshToken = _authService.GenerateRefreshToken();

            user.RefreshToken = _authService.HashPassword(newRefreshToken);
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return new ResponseModel<TokenResponse>
            {
                IsSuccess = true,
                Result = new TokenResponse
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                }
            };
        }

        public async Task<int> GetCurrentUserID()
        {
            var httpContext = _unitOfWork.HttpContextAccessor.HttpContext;
            var user = httpContext.User;
            var userIdClaim = user.FindFirst("UserID")?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return 0;
            }

            return userId;
        }

        public async Task<ResponseModel<GetUserDto>> GetCurrentUser(int? UserId)
        {
            var id = await GetCurrentUserID();
            if (UserId.HasValue)
            {
                id = UserId.Value;
            }

            if (id == 0)
            {
                return new ResponseModel<GetUserDto>
                {
                    Message = "User ID not found",
                    IsSuccess = false
                };
            }

            var user = await _unitOfWork.Users.FindAsync(u => u.UserID == id);
            if (user == null)
            {
                return new ResponseModel<GetUserDto>
                {
                    Message = "User not found",
                    IsSuccess = false
                };
            }

            if (_unitOfWork.Mapper == null)
            {
                return new ResponseModel<GetUserDto>
                {
                    Message = "Mapper not initialized",
                    IsSuccess = false
                };
            }

            var userDto = _unitOfWork.Mapper.Map<GetUserDto>(user);
            return new ResponseModel<GetUserDto>
            {
                IsSuccess = true,
                Result = userDto
            };
        }


    }

}
