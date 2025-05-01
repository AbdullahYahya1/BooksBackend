using BooksBackend.BusinessLayer.IService;
using BooksBackend.DataLayer.Dto.Books;
using BooksBackend.DataLayer.Dto.General;
using BooksBackend.DataLayer.Dto.Users;
using BooksBackend.DataLayer.Entities;


namespace BooksBackend.BusinessLayer.Services
{
    public class FollowService : IFollowService
    {

        private readonly IUnitOfWork _unitOfWork;

        public FollowService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseModel> FollowUserAsync(int userId)
        {
            var currentUserId = _unitOfWork.GetCurrentUserId();
            if (currentUserId == null)
            {
                return new ResponseModel { IsSuccess = false, Message = "Unauthorized user." };
            }

            if (currentUserId == userId)
            {
                return new ResponseModel { IsSuccess = false, Message = "You cannot follow yourself." };
            }

            var existingFollow = await _unitOfWork.UserFollows.FindAsync(f =>
                f.FollowerId == currentUserId && f.FollowingId == userId);

            if (existingFollow != null)
            {
                return new ResponseModel { IsSuccess = false, Message = "You are already following this user." };
            }

            var currentUser = await _unitOfWork.Users.GetByIdAsync((int)currentUserId);
            var followingUser = await _unitOfWork.Users.GetByIdAsync(userId);

            if (currentUser == null || followingUser == null)
            {
                return new ResponseModel { IsSuccess = false, Message = "User not found." };
            }

            // Create new follow record
            var follow = new UserFollow
            {
                FollowerId = (int)currentUserId,
                FollowingId = userId
            };

            currentUser.following += 1;
            followingUser.followers += 1;

            await _unitOfWork.UserFollows.AddAsync(follow);
            await _unitOfWork.SaveChangesAsync();

            return new ResponseModel
            {
                IsSuccess = true,
                Message = "Followed successfully."
            };
        }


        public async Task<ResponseModel> UnfollowUserAsync(int userId)
        {
            var currentUserId = _unitOfWork.GetCurrentUserId();
            if (currentUserId == null)
            {
                return new ResponseModel { IsSuccess = false, Message = "Unauthorized user." };
            }

            if (currentUserId == userId)
            {
                return new ResponseModel { IsSuccess = false, Message = "You cannot unfollow yourself." };
            }

            var follow = await _unitOfWork.UserFollows.GetByFollowerIdAndFollowingId((int)currentUserId, userId);
            if (follow == null)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = "You are not following this user."
                };
            }

            var currentUser = await _unitOfWork.Users.GetByIdAsync((int)currentUserId);
            var followedUser = await _unitOfWork.Users.GetByIdAsync(userId);

            if (currentUser == null || followedUser == null)
            {
                return new ResponseModel { IsSuccess = false, Message = "User not found." };
            }

            if (currentUser.following > 0)
                currentUser.following -= 1;

            if (followedUser.followers > 0)
                followedUser.followers -= 1;

            await _unitOfWork.UserFollows.DeleteAsync(follow.Id);
            await _unitOfWork.SaveChangesAsync();

            return new ResponseModel
            {
                IsSuccess = true,
                Message = "Unfollowed successfully."
            };
        }




        public async Task<ResponseModel<List<GetUserDto>>> GetFollowersAsync(int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
            {
                return new ResponseModel<List<GetUserDto>>
                {
                    IsSuccess = false,
                    Message = "User not found.",
                };
            }

            var followers = await _unitOfWork.UserFollows.GetFollowersAsync(userId);


            return new ResponseModel<List<GetUserDto>>
            {
                IsSuccess = true,
                Result = _unitOfWork.Mapper.Map<List<GetUserDto>>(followers)
            };
        }



        public async Task<ResponseModel<List<GetUserDto>>> GetFollowingAsync(int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
            {
                return new ResponseModel<List<GetUserDto>>
                {
                    IsSuccess = false,
                    Message = "User not found.",
                };
            }

            var following = await _unitOfWork.UserFollows.GetFollowingAsync(userId);

            return new ResponseModel<List<GetUserDto>>
            {
                IsSuccess = true,
                Result = _unitOfWork.Mapper.Map<List<GetUserDto>>(following)
            };
        }


        public async Task<ResponseModel<List<ActivityDto>>> GetFollowingActivityAsync()
        {
            var currentUserId = _unitOfWork.GetCurrentUserId();
            if (currentUserId == null)
            {
                return new ResponseModel<List<ActivityDto>>
                {   
                    IsSuccess = false,
                    Message = "Unauthorized."
                };
            }

            var followingUsers = await _unitOfWork.UserFollows.GetFollowingAsync((int)currentUserId);

            if (!followingUsers.Any())
            {
                return new ResponseModel<List<ActivityDto>>
                {
                    IsSuccess = true,
                    Result = new List<ActivityDto>()
                };
            }

            var followingUserIds = followingUsers.Select(u => u.UserID).ToList();

            var readActivities = await _unitOfWork.UserReadBooks.GetReadActivitiesByUserIdsAsync(followingUserIds);
            var ratingActivities = await _unitOfWork.Reviews.GetRatingActivitiesByUserIdsAsync(followingUserIds);
            var likeActivities = await _unitOfWork.UserBookFavorits.GetFavoriteActivitiesByUserIdsAsync(followingUserIds);
            var allActivities = readActivities
                .Concat(ratingActivities)
                .Concat(likeActivities)
                .OrderByDescending(a => a.ActivityDate)
                .ToList(); 

            return new ResponseModel<List<ActivityDto>>
            {
                IsSuccess = true,
                Result = allActivities
            };
        }
    }
}
