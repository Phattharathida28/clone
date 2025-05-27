using Application.Behaviors;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities.SU;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using Domain.Entities;

namespace Application.Features.SU.SURT04;

public class Edit
{
    public class Command : User, ICommand<Response>
    {
        public List<UserProfile> UserProfileLists { get; set; }
    }

    public class Response
    {
        public long UserId { get; set; }
    }

    public class Handler : IRequestHandler<Command, Response>
    {
        private readonly ICleanDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ICurrentUserAccessor _user;
        public Handler(ICleanDbContext context, UserManager<User> userManager, ICurrentUserAccessor user)
        {
            _context = context;
            _userManager = userManager;
            _user = user;
        }
        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.UserProfileLists == null) request.UserProfileLists = new List<UserProfile>();
            else
            {
                List<UserProfile> usrPf = new List<UserProfile>();

                request.UserProfileLists.ToList().ForEach(f =>
                {
                    if (!usrPf.Any(a => a.ProfileCode == f.ProfileCode && a.ProfileCode != null)) usrPf.Add(f);
                });

                request.UserProfileLists = usrPf;
            }

            Validate(request);

            if (request.Id == 0)
            {
                User user = new User();
                user.UserName = request.UserName;
                user.Firstname = request.Firstname;
                user.Lastname = request.Lastname;
                user.FirstnameTh = request.FirstnameTh;
                user.LastnameTh = request.LastnameTh;
                user.Email = request.Email;
                user.PhoneNumber = request.PhoneNumber;
                user.Active = request.Active;
                user.StartEffectiveDate = DateTime.Now;

                if (_context.Set<User>().Any(a => a.Email == request.Email)) throw new RestException(HttpStatusCode.BadRequest, "message.STD00056", request.Email);

                IdentityResult result = await _userManager.CreateAsync(user, request.PasswordHash);

                if (!result.Succeeded) throw new RestException(HttpStatusCode.BadRequest, string.Join(",", result.Errors.Select(s => s.Description).ToList()));

                User usr = _context.Set<User>().Where(w => w.UserName == user.UserName).FirstOrDefault();

                request.Id = usr.Id;
                request.UserProfileLists.ForEach(f => f.UserId = user.Id);

                _context.Set<User>().Attach(usr);
                _context.Entry(user).State = EntityState.Modified;

            }
            else if (request.Id != 0)
            {
                User user = _context.Set<User>().Where(w => w.UserName == request.UserName).FirstOrDefault();

                if (request.PasswordHash.Length <= 20)
                {
                    IdentityResult updateResult = await _userManager.UpdateAsync(user);
                    IdentityResult removePasswordResult = await _userManager.RemovePasswordAsync(user);
                    IdentityResult addPasswordResult = await _userManager.AddPasswordAsync(user, request.PasswordHash);

                    if (!updateResult.Succeeded) throw new RestException(HttpStatusCode.BadRequest, string.Join(",", updateResult.Errors.Select(s => s.Description).ToList()));
                    else if (!removePasswordResult.Succeeded) throw new RestException(HttpStatusCode.BadRequest, string.Join(",", removePasswordResult.Errors.Select(s => s.Description).ToList()));
                    else if (!addPasswordResult.Succeeded) throw new RestException(HttpStatusCode.BadRequest, string.Join(",", addPasswordResult.Errors.Select(s => s.Description).ToList()));
                }

                if (_context.Set<User>().Where(w => w.Email == request.Email && w.UserName != request.UserName).Count() != 0) throw new RestException(HttpStatusCode.BadRequest, "message.STD00056", request.Email);

                user = _context.Set<User>().Where(w => w.UserName == request.UserName).FirstOrDefault();
                user.UserName = request.UserName;
                user.Password = request.Password;
                user.Firstname = request.Firstname;
                user.Lastname = request.Lastname;
                user.FirstnameTh = request.FirstnameTh;
                user.LastnameTh = request.LastnameTh;
                user.Email = request.Email;
                user.PhoneNumber = request.PhoneNumber;
                user.Active = request.Active;

                _context.Set<User>().Attach(user);
                _context.Entry(user).State = EntityState.Modified;

                request.UserProfileLists.ForEach(f => f.UserId = user.Id);
            }

            List<UserProfile> userProfiles = await _context.Set<UserProfile>().Where(w => w.UserId == request.Id).ToListAsync();

            _context.Set<UserProfile>().RemoveRange(userProfiles);
            await _context.SaveChangesAsync(cancellationToken);

            _context.Set<UserProfile>().AddRange(request.UserProfileLists.Where(w => w.RowState != RowState.Delete));
            await _context.SaveChangesAsync(cancellationToken);

            return new Response { UserId = request.Id };
        }

        private void Validate(Command user)
        {
            List<UserProfile> userProfiles = user.UserProfileLists.ToList();

            if (_context.Set<User>().Any(a => user.Id == 0 && a.UserName == user.UserName)) throw new RestException(HttpStatusCode.BadRequest, "message.STD00004", user.UserName);
        }
    }
}