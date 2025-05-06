using Gym_Api.Data.Models;
using Gym_Api.DTO;
using Gym_Api.Repo;

namespace Gym_Api.Survices
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;
		private readonly IFileService _fileService;

		public ProductService(IProductRepository productRepository, IFileService fileService)
		{
			_productRepository = productRepository;
			_fileService = fileService;
		}

		public async Task<List<Product>> GetAllProductsAsyncS()
		{
			return await _productRepository.GetAllProductsAsync();
		}

		public async Task<Product?> GetProductByIdAsyncS(int id)
		{
			return await _productRepository.GetProductByIdAsync(id);
		}

		public async Task<int> Getproductscountasync()
		{
			return await _productRepository.GetProductsCount();
		}

		public async Task<Product> AddProductAsyncS(CreateProductDto dto)
		{
			var product = new Product
			{
				Product_Name = dto.Product_Name,
				Description = dto.Description,
				Price = dto.Price,
				Discount = dto.Discount,
				Stock_Quantity = dto.Stock_Quantity
			};

			if (dto.ProductImage != null)
			{
				product.Image_URL = await _fileService.SaveFileAsync(dto.ProductImage, "products");
			}

			await _productRepository.AddProductAsync(product);
			return product;
		}

		public async Task<bool> UpdateProductAsyncS(int id, UpdateProductDto dto)
		{
			var product = await _productRepository.GetProductByIdAsync(id);
			if (product == null)
				return false;

			product.Product_Name = dto.Product_Name;
			product.Description = dto.Description;
			product.Price = dto.Price;
			product.Discount = dto.Discount;
			product.Stock_Quantity = dto.Stock_Quantity;

			if (dto.ProductImage != null)
			{
				product.Image_URL = await _fileService.SaveFileAsync(dto.ProductImage, "products");
			}

			await _productRepository.UpdateProductAsync(product);
			return true;
		}

		public async Task<bool> DeleteProductAsyncS(int id)
		{
			var product = await _productRepository.GetProductByIdAsync(id);
			if (product == null)
				return false;

			await _productRepository.DeleteProductAsync(product);
			return true;
		}
	}
}
