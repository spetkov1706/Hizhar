namespace Hizhar.Web.ViewModels.Photos
{
    public class PhotoViewModel
    {
        public int Id { get; set; }

        public int HutId { get; set; }

        public string HutName { get; set; } = string.Empty;

        public string FilePath { get; set; } = null!;

        public string? Caption { get; set; }

        public string AuthorId { get; set; } = null!;

        public string AuthorName { get; set; } = null!;

        public DateTime UploadedOn { get; set; }
    }
}
