namespace AgentRest.Model
{
    public enum TargetStatus { Active, inactive }
    public class TargetModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public int XPostion { get; set; }
        public int YPostion { get; set; }
        public TargetStatus Status { get; set; }

    }
}
