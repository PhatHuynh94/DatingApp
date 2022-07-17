using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class AppUsersController : BaseAPIController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AppUsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
        {
            return Ok(await _userRepository.GetMembersAsync()); ;
        }

        //Ex: api/AppUsers/3
        //[HttpGet("{id}")]
        //public async Task<ActionResult<AppUser>> GetUser(int id)
        //{
        //    return await _userRepository.GetUserByIdAsync(id);
        //}

        //Ex: api/AppUsers/myra
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDTO>> GetUser(string username)
        {
            return await _userRepository.GetMemberByUsernameAsync(username);
        }
    }
}
