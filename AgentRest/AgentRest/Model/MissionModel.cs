namespace AgentRest.Model
{
    public enum MissionStatus { Done , NotDone }
    public class MissionModel
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public int TargetId { get; set; }
        public AgentModel Agent { get; set; }
        public TargetModel Target { get; set; }
        public MissionStatus Status { get; set; }
        public double ActionTime { get; set; }
    }
}
