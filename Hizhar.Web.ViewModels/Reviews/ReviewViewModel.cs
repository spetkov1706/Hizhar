namespace Hizhar.Web.ViewModels.Reviews
{
    public class ReviewViewModel
    {
        public int Id { get; set; }

        public int HutId { get; set; }

        public string HutName { get; set; } = string.Empty;

        public string AuthorId { get; set; } = null!;

        public string AuthorName { get; set; } = null!;

        public int Rating { get; set; }

        public string Content { get; set; } = null!;

        public DateTime CreatedOn { get; set; }
    }
}
