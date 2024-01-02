using System;
using V3.Models;
namespace V3.ViewModels
{
	public class NewCustomerViewModel
	{
		public IEnumerable<MembershipType>? MembershipTypes { get; set; }
		public Customer? Customer { get; set; }
	}
}

