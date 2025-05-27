using Application.Common.Models;
using Application.Interfaces;
using Domain.Entities.SU;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Persistense.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<User> _userManager;
    public IdentityService(UserManager<User> userManager) => _userManager = userManager;

    public async Task<User> GetUser(long userId) => await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

    public async Task<User> GetUser(long userId, Expression<Func<User, IEnumerable<UserProfile>>> profileFilter) => await _userManager.Users.Include(profileFilter).FirstOrDefaultAsync(u => u.Id == userId);
    public async Task<(Result Result, long UserId)> CreateUserAsync(User user)
    {
        IdentityResult result = await _userManager.CreateAsync(user, user.Password);

        return (result.ToApplicationResult(), user.Id);
    }

    public async Task<Result> DeleteUserAsync(long userId)
    {
        User user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user != null) return await DeleteUserAsync(user);

        return Result.Success();
    }

    public async Task<Result> DeleteUserAsync(User user)
    {
        IdentityResult result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

    public async Task<Result> UpdateUserAsync(User user)
    {
        IdentityResult result = await _userManager.UpdateSecurityStampAsync(user);

        return result.ToApplicationResult();
    }

    public async Task<Result> ResetPassword(User user, string newPassword)
    {
        string code = await _userManager.GeneratePasswordResetTokenAsync(user);
        IdentityResult result = await _userManager.ResetPasswordAsync(user, code, newPassword);

        return result.ToApplicationResult();
    }

    public string GeneratePassword()
    {
        PasswordOptions options = _userManager.Options.Password;
        int length = options.RequiredLength;
        string randomPassword = string.Empty;
        int passwordLength = length;

        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        Random random = new Random();

        while (randomPassword.Length != passwordLength)
        {
            int randomNumber = random.Next(valid.Length);

            char c = valid[randomNumber];
            randomPassword += c;
        }

        return randomPassword;
    }
}
