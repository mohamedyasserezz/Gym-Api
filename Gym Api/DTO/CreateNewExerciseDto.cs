namespace Gym_Api.Contract
{
	public class CreateNewExerciseDto
	{
		public string Exercise_Name { get; set; }
		public string Description { get; set; }
		public IFormFile Image_url { get; set; }
		public IFormFile Image_gif { get; set; }
		public int Duration { get; set; }
		public string Target_Muscle { get; set; }
		public int Difficulty_Level { get; set; }
		public int Calories_Burned { get; set; }
		public int Category_ID { get; set; }
	}
}
