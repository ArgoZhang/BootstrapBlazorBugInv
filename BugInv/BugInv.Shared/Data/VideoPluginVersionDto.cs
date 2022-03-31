using System.ComponentModel.DataAnnotations;

namespace BugInv.Shared.Data
{
    public class VideoPluginVersionDto
    {
        [Required]
        public VideoPluginSettingsDto VideoPluginSettings { get; init; } = new();
    }
}
