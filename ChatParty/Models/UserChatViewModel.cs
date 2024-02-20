namespace ChatParty.Models
{
	public class UserChatViewModel
	{
		public string Receiver { get; set; } = "######";
		public IEnumerable<Message> Messages { get; set; } = new List<Message>();
	}
}
