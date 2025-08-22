using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

/// <summary>
/// AutoMapper profile for Sale entities and DTOs.
/// </summary>
public class SaleMappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the SaleMappingProfile class.
    /// </summary>
    public SaleMappingProfile()
    {
        // Map from Sale entity to SaleResponse
        CreateMap<Sale, SaleResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        // Map from Sale entity to SaleSummaryResponse
        CreateMap<Sale, SaleSummaryResponse>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        // Map from SaleItem entity to SaleItemResponse
        CreateMap<SaleItem, SaleItemResponse>();

        // Map from Customer value object to CustomerResponse
        CreateMap<Customer, CustomerResponse>();

        // Map from Branch value object to BranchResponse
        CreateMap<Branch, BranchResponse>();

        // Map from Product value object to ProductResponse
        CreateMap<Product, ProductResponse>();

        // Map from CustomerDto to Customer value object
        CreateMap<CustomerDto, Customer>();

        // Map from BranchDto to Branch value object
        CreateMap<BranchDto, Branch>();

        // Map from ProductDto to Product value object
        CreateMap<ProductDto, Product>();
    }
}
