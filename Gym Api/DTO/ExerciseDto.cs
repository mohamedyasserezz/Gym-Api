namespace Gym_Api.DTO
{
	public class ExerciseDto
	{
		public int Exercise_ID { get; set; }
		public string Exercise_Name { get; set; }
		public string Description { get; set; }
		public string Image_url { get; set; }
		public string Image_gif { get; set; }
		public int Duration { get; set; }
		public string Target_Muscle { get; set; }
		public int Difficulty_Level { get; set; }
		public int Category_ID { get; set; }

		public int Calories_Burned { get; set; }
	}
}
