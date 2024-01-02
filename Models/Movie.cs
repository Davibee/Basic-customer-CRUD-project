using System;
namespace V3.Models
{
	public class Movie
	{
		public string? Name { get; set; }
		public int ID { get; set; }
		public string? Genre { get; set; }
		public DateTime ReleaseDate {get; set;}
		public DateTime DateAdded { get; set; }
		public int NumberInStock { get; set; }
	}
}

