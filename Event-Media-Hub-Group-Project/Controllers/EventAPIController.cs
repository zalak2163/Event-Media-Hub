﻿using Event_Media_Hub_Group_Project.Interface;
using Event_Media_Hub_Group_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Event_Media_Hub_Group_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventAPIController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventAPIController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("List")]
        public async Task<IEnumerable<EventDto>> ListEvents()
        {
            return await _eventService.ListEvents();
        }
        [HttpGet("{id}")]
        public async Task<EventDto> GetEvent(int id)
        {
            return await _eventService.GetEvent(id);
        }
        [HttpPost("Add")]
        [Authorize]
        public async Task<ServiceResponse> CreateEvent(EventDto eventDto)
        {
            return await _eventService.CreateEvent(eventDto);
        }
        [HttpPut("Update/{id}")]
        [Authorize]
        public async Task<ServiceResponse> UpdateEventDetails(int id, EventDto eventDto)
        {
            return await _eventService.UpdateEventDetails(id, eventDto);
        }
        [HttpDelete("Delete/{id}")]
        [Authorize]
        public async Task<ServiceResponse> Deleteevent(int id)
        {
            return await _eventService.Deleteevent(id);
        }
    }
}