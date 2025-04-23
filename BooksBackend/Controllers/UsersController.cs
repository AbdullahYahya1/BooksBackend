using BooksBackend.BusinessLayer.IService;
using BooksBackend.BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IFollowService _followService;
        private readonly IUserService _userService;
        public UsersController(IFollowService followService , IUserService userService)
        {
            _followService = followService;
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetProfile() {
            var user =await _userService.GetCurrentUser();
            return Ok(user); 
        }

        [HttpPost("follow/{UserId}")]
        public async Task<IActionResult> FollowUser(int UserId)
        {
            var response = await _followService.FollowUserAsync(UserId);
            return Ok(response);
        }

        [HttpPost("unfollow/{UserId}")]
        public async Task<IActionResult> UnfollowUser(int UserId)
        {
            var response = await _followService.UnfollowUserAsync(UserId);
            return Ok(response);
        }

        [HttpGet("{userId}/followers")]
        public async Task<IActionResult> GetFollowers(int userId)
        {
            var response = await _followService.GetFollowersAsync(userId);
            return Ok(response);
        }

        [HttpGet("{userId}/following")]
        public async Task<IActionResult> GetFollowing(int userId)
        {
            var response = await _followService.GetFollowingAsync(userId);
            return Ok(response);
        }

        [HttpGet("following/activity")]
        public async Task<IActionResult> FollowingActivity()
        {
            var response = await _followService.GetFollowingActivityAsync();
            return Ok(response);
        }
    }
}
