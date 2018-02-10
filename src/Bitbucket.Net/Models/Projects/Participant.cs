namespace Bitbucket.Net.Models.Projects
{
    public class Participant
    {
        public User User { get; set; }
        public string Role { get; set; }
        public bool Approved { get; set; }
        public string Status { get; set; }

        public override string ToString() => User.DisplayName;
    }
}
