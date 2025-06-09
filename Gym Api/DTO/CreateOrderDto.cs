namespace Gym_Api.DTO
{
	public class CreateOrderDto
	{
		public string User_ID { get; set; }
		public List<OrderItemRequest> Items { get; set; }
		public string RecipientName { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public int PhoneNumber { get; set; }
		public IFormFile PaymentProof { get; set; }
	}
}
