using System;
using System.ComponentModel.DataAnnotations;

namespace V3.Models
{
	public class Customer
	{
		[Required]
		[StringLength(255)]
		public string? name { get; set; }
		public int Id { get; set; }
		public bool IsSubcribedTo { get; set; }
		
        public MembershipType? MembershipType { get; set; }

		
		public int MembershipTypeId { get; set; }

		[MinAgeForMembershipType]
		public DateTime? Birthdate { get; set; }
	}
}

