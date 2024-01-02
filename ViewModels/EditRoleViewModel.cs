using System;
using System.ComponentModel.DataAnnotations;

namespace V3.ViewModels
{
	public class EditRoleViewModel
	{
		public EditRoleViewModel()
		{
			Users = new List<string>();
		}
		public string Id { get; set; }
		public List <string> Users { get; set; }

		[Required]
		public string RoleName { get; set; }
	}
}

