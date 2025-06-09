using Gym_Api.Data.Models;
using Gym_Api.DTO;
using Gym_Api.Repo;
using Gym_Api.Survices;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Services
{
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository _repository;
		private readonly IFileService _fileService;

		public OrderService(IOrderRepository repository, IFileService fileService)
		{
			_repository = repository;
			_fileService = fileService;
		}

		public async Task<Order?> GetOrderByIdAsync(int id)
		{
			return await _repository.GetOrderByIdR(id);
		}


		public async Task<object> AddOrderAsync(CreateOrderDto createOrderDto)
		{
			var total = 0.0;
			var items = new List<OrderItem>();

			foreach (var item in createOrderDto.Items)
			{
				var product = await _repository.GetProductByIdAsync(item.Product_ID);
				if (product == null || product.Stock_Quantity < item.Quantity)
					return new { error = $"Product {item.Product_ID} not available in sufficient quantity." };

				var discountedPrice = product.Price - (product.Price * product.Discount / 100);
				var itemTotalPrice = discountedPrice * item.Quantity; // حساب سعر المنتج الواحد مع الكمية
				total += itemTotalPrice; // إضافة للمجموع الكلي

				product.Stock_Quantity -= item.Quantity;
				await _repository.UpdateProductAsync(product); // تحديث الكمية

				items.Add(new OrderItem
				{
					Product_ID = item.Product_ID,
					Quantity = item.Quantity,
					ItemTotalPrice = itemTotalPrice // تخزين سعر المنتج الواحد
				});
			}

			var fileName = await _fileService.SaveFileAsync(createOrderDto.PaymentProof, "Orders");

			var order = new Order
			{
				User_ID = createOrderDto.User_ID,
				RecipientName = createOrderDto.RecipientName,
				Address = createOrderDto.Address,
				City = createOrderDto.City,
				PhoneNumber = createOrderDto.PhoneNumber,
				Order_Date = DateTime.UtcNow,
				TotalPrice = total, // المجموع الكلي
				PaymentProof = fileName,
				IsPaid = false,
				Order_Status = "Pending",
				OrderItems = items
			};

			await _repository.AddOrderR(order);
			// إرجاع تفاصيل الطلب
			return new
			{
				orderId = order.Order_id,
				userId = order.User_ID,
				orderDate = order.Order_Date,
				orderStatus = order.Order_Status,
				isPaid = order.IsPaid,
				totalPrice = order.TotalPrice,
				recipientName = order.RecipientName,
				address = order.Address,
				city = order.City,
				phoneNumber = order.PhoneNumber,
				paymentProof = order.PaymentProof,
				items = items.Select(i => new
				{
					productId = i.Product_ID,
					quantity = i.Quantity,
					itemTotalPrice = i.ItemTotalPrice // إرجاع سعر المنتج الواحد
				}).ToList()
			};

		}



	}
}
