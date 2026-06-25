using AutoMapper;
using BhaviAmrinder.Application.DTOs;
using BhaviAmrinder.Domain.Entities;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>();

        CreateMap<ProductDto, Product>();
    }
}