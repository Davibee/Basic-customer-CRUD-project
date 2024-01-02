using System;
using System.ComponentModel.DataAnnotations;
using V3.Models;

namespace V3.Dtos
{
	public class CustomerDto
    {
			
		[Required]
        [StringLength(255)]
        public string? name { get; set; }
        public int Id { get; set; }
        public bool IsSubcribedTo { get; set; }
      

        [Required(ErrorMessage = "MembershipType is required.")]
        public int MembershipTypeId { get; set; }

        [MinAgeForMembershipType]
        public DateTime? Birthdate { get; set; }
    
}
}

