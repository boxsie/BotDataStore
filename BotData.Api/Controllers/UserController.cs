﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BotData.Data;
using BotData.Data.Context;
using BotData.Data.Entity.User;
using BotData.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BotData.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly BotDataContext _context;

        public UserController(BotDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await _context
                .Users
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("{discordId:long}")]
        public async Task<ActionResult<User>> GetById(long discordId)
        {
            var user = await _context
                .Users
                .FirstOrDefaultAsync(x => x.DiscordId == discordId);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<User>> GetByName(string userName)
        {
            var user = await _context
                .Users
                .FirstOrDefaultAsync(x => x.Name == userName);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<User>> Post(UserModel model)
        {
            if (await _context.Users.AnyAsync(x => x.DiscordId == model.DiscordId))
                return BadRequest("User already exists");

            var user = new User
            {
                DiscordId = model.DiscordId,
                Name = model.Name
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPut]
        [ValidateModel]
        public async Task<ActionResult<User>> Put(UserModel model)
        {
            var user = await _context
                .Users
                .FirstOrDefaultAsync(x => x.DiscordId == model.DiscordId);

            if (user == null)
                return BadRequest("User does not exist");

            if (!string.IsNullOrWhiteSpace(model.Name))
                user.Name = model.Name;
            
            if (!string.IsNullOrWhiteSpace(model.EntranceSound))
                user.EntranceSound = model.EntranceSound;

            await _context.SaveChangesAsync();

            return Ok(user);
        }
    }
}

