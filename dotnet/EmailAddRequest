namespace Immersed.Models.Requests
{
    public class EmailsAddRequest
    {
        [Required]
        public string EmailAddress { get; set; }
       
        public string Subject { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string From { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string To { get; set; }

    }
}
