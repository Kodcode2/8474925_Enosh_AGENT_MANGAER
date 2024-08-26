namespace AgentRest.Model
{
    public enum AgentStatus { Active , inactive }
    public class AgentModel
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Image { get; set; }
        public int XPostion {  get; set; } 
        public int YPostion {  get; set; } 
        public AgentStatus Status { get; set; }
        public ICollection<MissionModel> Missions { get; set; } = [];


    }
}
