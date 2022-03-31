using BootstrapBlazor.Components;
using System.ComponentModel.DataAnnotations;

namespace BugInv.Shared.Core
{
    public class VideoMp4Validator : IValidator
    {
        public string? ErrorMessage { get; set; }

        public async Task ValidateAsync(object? propertyValue, ValidationContext context, List<ValidationResult> results)
        {
            var url = propertyValue?.ToString();
            if (string.IsNullOrWhiteSpace(url))
            {
                System.Console.WriteLine($"string.IsNullOrWhiteSpace(url) - '{context.MemberName}'");
                results.Add(new ValidationResult("Неудалось загрузить видео", new[] { context.MemberName! }));
                return;
            }

            using var httpClient = new HttpClient();
            var result = await httpClient.GetAsync(url, HttpCompletionOption.ResponseContentRead);

            if (!result.IsSuccessStatusCode)
            {
                System.Console.WriteLine($"result.IsSuccessStatusCode - '{context.MemberName}'");
                results.Add(new ValidationResult($"Неудалось загрузить видео, код ошибки '{result.StatusCode}'", new[] { context.MemberName! }));
                return;
            }

            var headers = result.Content.Headers;
            if (!string.Equals(headers.ContentType?.MediaType, "video/mp4", StringComparison.OrdinalIgnoreCase))
            {
                System.Console.WriteLine($"video/mp4 - '{context.MemberName}'");
                results.Add(new ValidationResult("Тип содержимого должен быть 'video/mp4'", new[] { context.MemberName! }));
                return;
            }

            if (headers.ContentLength is null || headers.ContentLength <= 0)
            {
                System.Console.WriteLine($"ContentLength - '{context.MemberName}'");
                results.Add(new ValidationResult("Размер видео не может быть меньше нуля", new[] { context.MemberName! }));
                return;
            }

            System.Console.WriteLine($"SUC! results - '{results.Count}'");
        }
    }
}
