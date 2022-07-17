﻿using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //Using Projection
        public async Task<MemberDTO> GetMemberByUsernameAsync(string username)
        {
            return await _context.AppUsers.Where(x => x.UserName == username)
                .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)    
                .SingleOrDefaultAsync();
        }
        //Using Projection
        public async Task<IEnumerable<MemberDTO>> GetMembersAsync()
        {
            return await _context.AppUsers
                .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.AppUsers.FindAsync(id);
        }

        //Not using projection
        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.AppUsers
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == username);
        }
        //Not using projection
        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.AppUsers
                .Include(p => p.Photos)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}
