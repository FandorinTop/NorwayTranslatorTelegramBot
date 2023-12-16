using System.ComponentModel.DataAnnotations;

namespace TelegramTranslator.Dictionary.Models
{
    public class PartOfSpeechResponce
    {
        [Required]
        [MaxLength(64)]
        public string PartOfSpeech { get; set; } = default!;

        public List<string> Interpretations { get; set; } = new List<string>();
    }
}
