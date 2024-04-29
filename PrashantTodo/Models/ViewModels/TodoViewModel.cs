namespace PrashantTodo.Models.ViewModels
{
	public class TodoViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

        public DateTime dateTime { get; set; }

        public string status { get; set; }

        public string? description { get; set; }

        public List<TodoItem> TodoList { get; set; }

		public TodoItem Todo { get; set; }
	}
}
