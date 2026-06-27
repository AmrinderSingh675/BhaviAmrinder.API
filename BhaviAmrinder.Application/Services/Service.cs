using AutoMapper;
using BhaviAmrinder.Application.IRepositories;
using BhaviAmrinder.Application.IServices;
using BhaviAmrinder.Domain.Entities;

namespace BhaviAmrinder.Application.Services;

using AutoMapper;

public class GenericService<TDto, TEntity> : IGenericService<TDto, TEntity>
    where TEntity : class
{
    private readonly IRepository<TEntity> _repository;
    private readonly IMapper _mapper;

    public GenericService(IRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TDto> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<TDto>(entity);
    }

    public async Task<List<TDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<List<TDto>>(entities);
    }

    public async Task<TDto> CreateAsync(TDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);   // DTO → Entity
        await _repository.AddAsync(entity);

        return _mapper.Map<TDto>(entity);        // Entity → DTO
    }

    public async Task<TDto?> UpdateAsync(int id, TDto dto)
    {
        var existingEntity = await _repository.GetByIdAsync(id);

        if (existingEntity == null)
        {
            return default;
        }

        // Copy values from DTO to the existing entity
        _mapper.Map(dto, existingEntity);

        await _repository.UpdateAsync(existingEntity);

        return _mapper.Map<TDto>(existingEntity);
    }


}