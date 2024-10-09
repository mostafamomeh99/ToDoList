namespace ToDoList.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TodoItem> TodoItem { get; set; }=new List<TodoItem>();
    }
}
