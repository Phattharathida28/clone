using Application.Common.Models;
using Domain.Entities.SU;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IIdentityService
{
    Task<User> GetUser(long userId);
    Task<User> GetUser(long userId, Expression<Func<User, IEnumerable<UserProfile>>> profileFilter);
    Task<(Result Result, long UserId)> CreateUserAsync(User user);
    Task<Result> DeleteUserAsync(long userId);
    Task<Result> UpdateUserAsync(User user);
    Task<Result> ResetPassword(User user, string newPassword);
    string GeneratePassword();
}
