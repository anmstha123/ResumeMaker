namespace Resume.Models
{
    public class ResumeData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string State {  get; set; }
        public string ZipCode { get; set; }

        public List<Experience> Experiences { get; set; }
        public List<Education> Education { get; set; }
    }
}
