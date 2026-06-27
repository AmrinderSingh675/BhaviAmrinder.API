using BhaviAmrinder.Domain.Entities;

namespace BhaviAmrinder.Application.IServices;

public interface IGenericService<TDto, TEntity>
    where TEntity : class
{
    Task<TDto> GetByIdAsync(int id);
    Task<List<TDto>> GetAllAsync();
    Task<TDto> CreateAsync(TDto dto);
    Task<TDto> UpdateAsync(int id, TDto dto);
}