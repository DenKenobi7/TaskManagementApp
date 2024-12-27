using AutoMapper;
using TaskManagementApp.Application.DTOs;
using TaskManagementApp.Domain;

namespace TaskManagementApp.Application.MappingProfiles;

public class TaskMappingProfile : Profile
{
    public TaskMappingProfile()
    {
        CreateMap<TaskEntity, TaskEntityDto>();
    }
}