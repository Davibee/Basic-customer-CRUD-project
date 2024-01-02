using System;
using System.ComponentModel.DataAnnotations;

namespace V3.ViewModels
{
	public class CreateRoleViewModel
	{
		[Required]
		public string? RoleName { get; set; }
	}
}

