using AutoMapper;
using Domain.ProcessAggregate;

namespace Application.Queries.GetProcesses
{
    // ReSharper disable once UnusedType.Global
    public class ProcessToProcessDtoMap : Profile
    {
        public ProcessToProcessDtoMap()
        {
            CreateMap<Process, ProcessDTO>();
        }
    }
}