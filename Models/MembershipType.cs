using System;
namespace V3.Models
{
	public class MembershipType
	{
		public int? Id { get; set; }
		public string? name { get; set; }
		public int SignUpFee { get; set; }
		public int DurationInMonths { get; set; }
		public int DurationRates { get; set; }


		public static readonly int Unknown = 0;
		public static readonly int PayAsYouGo = 1;
	}
}

