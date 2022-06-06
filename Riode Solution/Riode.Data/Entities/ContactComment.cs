using System;
using System.ComponentModel.DataAnnotations;

namespace Riode.Data.Entities
{
	public class ContactComment : BaseEntity
	{
		[EmailAddress(ErrorMessage = "Your email is not correct!")]
		[Required(ErrorMessage = "Your email cannot be empty")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Cannot be empty")]
		public string Comment { get; set; }

		[Required(ErrorMessage = "Cannot be empty")]
		public string User { get; set; }
		public DateTime? AnswerDate { get; set; }
		public string AnswerMessage { get; set; }
		public int? AnsweredById { get; set; }
		
	}
}
