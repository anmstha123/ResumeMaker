namespace Resume.Models
{
    public class ResumeData
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public List<Experience> Experiences { get; set; }
        public List<Education> Education { get; set; }
    }
}
