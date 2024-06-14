using NodaTime;

namespace Pks4Core.Models
{
    public class MessageModel
    {
		public int Id { get; set; }

		public string From { get; set; } = null!;

		public string To { get; set; } = null!;

		public string MessageTitle { get; set; } = null!;

		public string MessageText { get; set; } = null!;

		public LocalDateTime SendingTime { get; set; }

		public bool Status { get; set; }
	}
}
