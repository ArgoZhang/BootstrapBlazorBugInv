using System.ComponentModel.DataAnnotations;

namespace BugInv.Shared.Data
{
    public class VideoPluginSettingsDto
    {
        [Required, Url]
        public string? VideoUrl { get; set; }

    }
}
