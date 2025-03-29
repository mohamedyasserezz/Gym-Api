namespace Gym_Api.Contract
{
	public class CreateNewExerciseDto
	{
		public string Exercise_Name { get; set; }
		public string Description { get; set; }
		public string Video_URL { get; set; }
		public int Duration { get; set; }
		public string Target_Muscle { get; set; }
		public int Difficulty_Level { get; set; }
		public int Calories_Burned { get; set; }
		public int Category_ID { get; set; }
	}
}
