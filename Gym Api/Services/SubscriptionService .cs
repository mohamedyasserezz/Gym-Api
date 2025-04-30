using Gym_Api.Contract;
using Gym_Api.Data;
using Gym_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Survices
{
	public class SubscriptionService  : ISubscriptionService
	{
		private readonly ApplicationDbContext _context;
		public SubscriptionService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<string> AddSubscriptionAsync(CreateSubscriptionDto createSubscriptionDto)
		{
			// التحقق مما إذا كان المستخدم مشترك بالفعل مع نفس الكوتش
			var existingSubscription = await _context.Subscriptions
				.FirstOrDefaultAsync(s => s.User_ID == createSubscriptionDto.UserId &&
										  s.Coach_ID == createSubscriptionDto.CoachId);

			if (existingSubscription != null)
			{
				// لو الاشتراك موجود وكان الدفع تم لكن في انتظار الموافقة
				if (existingSubscription.IsPaid && !existingSubscription.IsApproved)
				{
					return "تم الدفع بنجاح، في انتظار الموافقة من الإدارة.";
				}

				// لو الدفع والموافقة تمت بالفعل
				if (existingSubscription.IsPaid && existingSubscription.IsApproved)
				{
					return "أنت مشترك بالفعل مع هذا المدرب.";
				}
				if (existingSubscription.EndDate > DateTime.UtcNow)
				{
					return $"لديك اشتراك ساري بالفعل حتى {existingSubscription.EndDate:yyyy-MM-dd}. لا يمكن الاشتراك مرة أخرى قبل انتهاء الاشتراك الحالي.";
				}

			}

			// إنشاء اشتراك جديد
			var newSubscription = new Subscribe
			{
				User_ID = createSubscriptionDto.UserId,
				Coach_ID = createSubscriptionDto.CoachId,
				IsPaid = false,  // يبدأ الاشتراك على إنه غير مدفوع
				IsApproved = false, // لم تتم الموافقة عليه بعد
				StartDate = DateTime.UtcNow,
				EndDate = DateTime.UtcNow.AddMonths(1) // اشتراك لمدة شهر
			};

			_context.Subscriptions.Add(newSubscription);
			await _context.SaveChangesAsync();

			return "تم الاشتراك بنجاح، يرجى إتمام عملية الدفع.";
		}


		public async Task<string> ConfirmPaymentAsync(int SubscriptionId)
		{
			// البحث عن الاشتراك في قاعدة البيانات
			var subscription = await _context.Subscriptions.FindAsync(SubscriptionId);

			if (subscription == null)
			{
				return "الاشتراك غير موجود.";
			}

			// لو الدفع تم بالفعل
			if (subscription.IsPaid)
			{
				return "تم الدفع بالفعل لهذا الاشتراك.";
			}

			// تحديث حالة الدفع
			subscription.IsPaid = true;
			await _context.SaveChangesAsync();

			return "تم الدفع بنجاح، في انتظار الموافقة من الإدارة.";

		}

		public async Task<List<Subscribe>> GetAllPaidSubscriptionsAsync()
		{
			// جلب كل الاشتراكات اللي تم دفعها ولكن لم تتم الموافقة عليها بعد
			var paidSubscriptions = await _context.Subscriptions
				.Where(s => s.IsPaid && !s.IsApproved) // مدفوع ولكن لم تتم الموافقة عليه بعد
				.Include(s => s.User) // تحميل بيانات المستخدم المشترك
				.Include(s => s.Coach) // تحميل بيانات الكوتش
				.ToListAsync();

			return paidSubscriptions;
		}

		public async Task<string> ApproveSubscriptionPaymentAsync(int subscriptionId)
		{
			var subscription = await _context.Subscriptions.FindAsync(subscriptionId);

			if (subscription == null)
			{
				return "هذا الاشتراك غير موجود.";
			}

			if (!subscription.IsPaid)
			{
				return "لا يمكن الموافقة على اشتراك لم يتم دفعه بعد.";
			}

			if (subscription.IsApproved)
			{
				return "تمت الموافقة على هذا الاشتراك بالفعل.";
			}

			// تحديث حالة الموافقة
			subscription.IsApproved = true;
			await _context.SaveChangesAsync();

			return "تمت الموافقة على الدفع بنجاح.";
		}


		public async Task<string> RejectSubscriptionAsync(int subscriptionId)
		{
			var subscription = await _context.Subscriptions.FindAsync(subscriptionId);

			if (subscription == null)
			{
				return "الاشتراك غير موجود.";
			}

			if (!subscription.IsPaid)
			{
				return "لا يمكن رفض الاشتراك لأنه لم يتم الدفع بعد.";
			}

			if (subscription.IsApproved)
			{
				return "لا يمكن رفض الاشتراك بعد الموافقة عليه.";
			}

			// رفض الاشتراك
			_context.Subscriptions.Remove(subscription);
			await _context.SaveChangesAsync();

			return "تم رفض الاشتراك وإزالته من النظام.";
		}

		public async Task<List<Coach>> GetUserSubscriptionsAsync(int userid)
		{
			var userExists = await _context.Users.AnyAsync(u => u.User_ID == userid);
			if (!userExists)
			{
				return null;
			}
			var subscribe = await _context.Subscriptions
		.Where(s => s.User_ID == userid && s.IsPaid) 
		.Include(s => s.Coach) 
		.ToListAsync();
			var coaches = subscribe.Select(s => s.Coach).ToList();
			return coaches;

		}

		public async Task<List<User>> GetCoachSubscribersAsync(int coachid) 
		{
			var coachExists = await _context.Coaches.AnyAsync(c => c.Coach_ID == coachid);
			if (!coachExists)
			{
				return null;
			}

			var subscriptions = await _context.Subscriptions
				.Where(s => s.Coach_ID == coachid && s.IsPaid) 
				.Include(s => s.User) 
				.ToListAsync();

			var users = subscriptions.Select(s => s.User).ToList(); 
			return users;
		}

		public async Task<bool> CancelSubscriptionAsync(int subscriptionId, int userId)
		{
			var subscription = await _context.Subscriptions
				.FirstOrDefaultAsync(s => s.Subscribe_ID == subscriptionId && s.User_ID == userId);

			if (subscription == null)
				return false; 

			_context.Subscriptions.Remove(subscription);
			await _context.SaveChangesAsync();
			return true;
		}


	}
}
