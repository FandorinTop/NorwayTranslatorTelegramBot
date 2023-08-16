using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Configuration;

namespace NorwayTranslatorTelegramBot.TextToSpeech.Azure
{
    public class AzureNorwaySpeechSetting : IAzureSpeechSetting
    {
        private const string SectionName = "AzureSpeech";

        private readonly string Key = "";
        private readonly string Region = "";
        private readonly string VoiceName = "";
        private readonly SpeechSynthesisOutputFormat OutputFormat;

        public AzureNorwaySpeechSetting(IConfiguration configuration)
        {
            var speechSection = configuration.GetSection(SectionName)
                ?? throw new ArgumentNullException(nameof(SectionName)); ;
            Key = speechSection[nameof(Key)]
                ?? throw new ArgumentNullException($"${nameof(Key)} is null");
            Region = speechSection[nameof(Region)]
                ?? throw new ArgumentNullException($"${nameof(Region)} is null");
            VoiceName = speechSection[nameof(VoiceName)]
                ?? throw new ArgumentNullException($"${nameof(VoiceName)} is null");
            OutputFormat = Enum.Parse<SpeechSynthesisOutputFormat>(speechSection[nameof(OutputFormat)]
                ?? throw new ArgumentNullException($"${nameof(OutputFormat)} is null"));
        }

        public SpeechConfig GetConfuguration()
        {
            var speechConfig = SpeechConfig.FromSubscription(Key, Region);
            speechConfig.SetSpeechSynthesisOutputFormat(OutputFormat);
            speechConfig.SpeechSynthesisVoiceName = VoiceName;

            return speechConfig;
        }
    }
}