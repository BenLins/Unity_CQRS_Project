using Project.Domain.Entities;
using Project.Application.Dto;

namespace Project.Application.Mappings
{
    public static class CounterDtoMapper
    {
        public static CounterDto MapToDto(Counter counter)
        {
            return new CounterDto
            {
                Id = counter.Id,
                Name = counter.Name,
                Value = counter.Value,
                Step = counter.Step
            };
        }
        
        public static Counter MapToDomain(this CounterDto counterDto)
        {
            return new Counter(counterDto.Id, counterDto.Name, counterDto.Value, counterDto.Step);
        }
    }
}