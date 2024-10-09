namespace ToDoList.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string FileDescription { get; set; }
        public DateTime DeadLine { get; set; } = new DateTime();
        public int UserId { get; set; }
        public User User  { get; set; }
    }
}
