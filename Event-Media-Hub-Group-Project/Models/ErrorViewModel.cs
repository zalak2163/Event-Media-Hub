namespace Event_Media_Hub_Group_Project.Models
{
    public class ErrorViewModel
    {
        internal List<string> Errors;
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
