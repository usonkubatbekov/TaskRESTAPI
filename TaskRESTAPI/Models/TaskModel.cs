namespace TaskRESTAPI.Models
{
    public class TaskModel
    {
        public int Id { get; set; }

        public DateTime TaskDate { get; set; }

        public string TaskName { get; set; }

        public string TaskStatus { get; set; }

        public string FilePath { get; set; }
    }
}
