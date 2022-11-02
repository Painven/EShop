using AutoMapper;
using EShopAPI.DataAccess;
using EShopAPI.Models;
using System.Text.RegularExpressions;

namespace EShopAPI.Mapper;
public class ProductAutomapperProfile : Profile
{
    static Dictionary<CacheImageType, string> cacheSizes = new ()
    {
        [CacheImageType.Category] = "150x150",
        [CacheImageType.Product] = "228x228",
    };

    public ProductAutomapperProfile()
    {
        CreateMap<ProductDto, Product>()
            .ForMember(c => c.Created, c => c.MapFrom(x => DateTime.UtcNow));

        CreateMap<Product, ProductDto>()
            .ForMember(c => c.ImageSmall, c => c.MapFrom(x => GetImageCacheFromOriginal(x.Image, CacheImageType.Product)));

        CreateMap<CategoryDto, Category>()
            .ForMember(c => c.Created, c => c.MapFrom(x => DateTime.UtcNow));
        CreateMap<Category, CategoryDto>();
        CreateMap<Category, CategoryDetailsDto>()
            .ForMember(c => c.ImageSmall, c => c.MapFrom(x => GetImageCacheFromOriginal(x.Image, CacheImageType.Category)))
            .ForMember(c => c.ProductsInCategory, c => c.MapFrom(x => x.Products.Count));
        
    }

    private static string GetImageCacheFromOriginal(string src, CacheImageType type)
    {
        string extension = Path.GetExtension(src);
        if (!string.IsNullOrWhiteSpace(extension) && src.StartsWith("https://etk-komplekt.ru/"))
        {
            string cacheSrc = src.Replace("/image/", "/image/cache/").Replace(extension, $"-{cacheSizes[type]}{extension}");
            return cacheSrc;
        }
        return src;
    }

    private enum CacheImageType
    {
        Category,
        Product
    }
}
