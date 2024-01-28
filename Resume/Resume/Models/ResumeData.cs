namespace Resume.Models
{
    public class ResumeData
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public Experience[] Experiences { get; set; }
        public Education[] Education { get; set; }
    }
}
