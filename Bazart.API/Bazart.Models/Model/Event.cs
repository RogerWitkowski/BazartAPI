namespace Bazart.Models.Model
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EventDetail EventDetails { get; set; }
        public string? CreatedById { get; set; }
        public User CreatedBy { get; set; }
    }
}