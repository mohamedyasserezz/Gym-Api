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


		public async Task<List<Order>> GetAllOrdersAsync()
		{
			return await _repository.GetAllOrders();
		}


		public async Task<List<Order>> GetAllUserOrdersAsync(string UserId)
		{
			return await _repository.GetAllUserOrders(UserId);
		}


		public async Task<object> GetOrderByIdAsync(int id)
		{
			var order = await _repository.GetOrderByIdR(id);
			if (order == null)
				return new { error = "Order not found." };

			return new
			{
				orderId = order.Order_id,
				orderDate = order.Order_Date,
				orderStatus = order.Order_Status,
				isPaid = order.IsPaid,
				totalPrice = order.TotalPrice,
				recipientName = order.RecipientName,
				address = order.Address,
				city = order.City,
				phoneNumber = order.PhoneNumber,
				paymentProof = order.PaymentProof,
				items = order.OrderItems.Select(i => new
				{
					orderItemId = i.OrderItem_ID,
					productId = i.Product_ID,
					productName = i.Product.Product_Name,
					description = i.Product.Description,
					price = i.Product.Price,
					discount = i.Product.Discount,
					quantity = i.Quantity,
					itemTotalPrice = i.ItemTotalPrice,
					imageUrl = i.Product.Image_URL
				}).ToList()
			};
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


			var order = new Order
			{
				User_ID = createOrderDto.User_ID,
				RecipientName = createOrderDto.RecipientName,
				Address = createOrderDto.Address,
				City = createOrderDto.City,
				PhoneNumber = createOrderDto.PhoneNumber,
				Order_Date = DateTime.UtcNow,
				TotalPrice = total, // المجموع الكلي
				IsPaid = false,
				Order_Status = "Pending",
				OrderItems = items
			};
			if (createOrderDto.PaymentProof is not null)
			{
				order.PaymentProof = await _fileService.SaveFileAsync(createOrderDto.PaymentProof, "PaymentProofs");
			}

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

		public async Task<List<Order>> GetAllPendingOrdersAsync()
		{
			return await _repository.GetAllPendingOrdersAsync();
		}

		public async Task<bool> AcceptOrderAsync(int Orderid)
		{
			var order = await _repository.GetOrderByIdR(Orderid);
			if (order == null)
			{
				Console.WriteLine($"Subscription with id {Orderid} not found.");
				return false;
			}

			if (order.IsPaid)
			{
				Console.WriteLine($"Subscription with id {Orderid} is already approved.");
				return false;
			}
			order.IsPaid = true;
			order.Order_Status = "Accepted";
			await _repository.AcceptOrderR(order);

			Console.WriteLine($"Subscription {Orderid} approved successfully.");
			return true;
		}


		public async Task<bool> RejectOrderAsync(int Orderid)
		{
			var order = await _repository.GetOrderByIdR(Orderid);
			if (order == null)
			{
				return false;
			}
			order.IsPaid = false;
			order.Order_Status = "Rejected";
			await _repository.RejectOrderR(order);
			return true;
		}
		public async Task<bool> UpdateOrderAsync(int Orderid, UpdateOrderDto updateOrderDto)
		{
			var order = await _repository.GetOrderByIdR(Orderid);
			if(order == null)
			{
				return false;
			}
			order.RecipientName = updateOrderDto.RecipientName;
			order.Address = updateOrderDto.Address;
			order.City = updateOrderDto.City;
			order.PhoneNumber = updateOrderDto.PhoneNumber;
			await _repository.UpdateOrder(order);
			return true;

		}


	}
}
