﻿namespace Gym_Api.Survices
{
	public interface IFileService
	{
		public Task<string> SaveFileAsync(IFormFile imageFile, string subfolder);
		public void DeleteFile(string file, string subfolder);
	}
}
