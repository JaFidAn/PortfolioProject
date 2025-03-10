using Application.DTOs.Technologies;
using Application.Repositories;
using Application.Services.Abstracts;
using AutoMapper;
using Domain.Entities;

namespace Application.Services.Concretes;

public class TechnologyService : ITechnologyService
{
    private readonly ITechnologyRepository _technologyRepository;
    private readonly IMapper _mapper;

    public TechnologyService(ITechnologyRepository technologyRepository, IMapper mapper)
    {
        _technologyRepository = technologyRepository;
        _mapper = mapper;
    }

    public async Task<Guid> CreateAsync(CreateTechnologyDto dto)
    {
        var technology = _mapper.Map<Technology>(dto);
        technology.Id = Guid.NewGuid();

        await _technologyRepository.AddAsync(technology);
        return technology.Id;
    }

    public async Task<TechnologyResponseDto?> GetByIdAsync(Guid id)
    {
        var technology = await _technologyRepository.GetByIdAsync(id);
        if (technology == null || technology.IsDeleted) return null;

        return _mapper.Map<TechnologyResponseDto>(technology);
    }

    public async Task<IEnumerable<TechnologyResponseDto>> GetAllAsync()
    {
        var technologies = await _technologyRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<TechnologyResponseDto>>(technologies.Where(t => !t.IsDeleted));
    }

    public async Task<Technology> UpdateAsync(Technology model)
    {
        await _technologyRepository.UpdateAsync(model);
        return await _technologyRepository.GetByIdAsync(model.Id);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _technologyRepository.DeleteAsync(id);
    }
}
