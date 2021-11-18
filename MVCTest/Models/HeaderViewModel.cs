namespace MVCTest.Models
{
    public class HeaderViewModel
    {
        public UserViewModel UserViewModel { get; set; }
    }

    public class UserViewModel
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Title { get; set; }

        public byte[] ThumbnailPhoto { get; set; }

        public string UserName { get; set; }

        public int? UserId { get; set; }
    }
}