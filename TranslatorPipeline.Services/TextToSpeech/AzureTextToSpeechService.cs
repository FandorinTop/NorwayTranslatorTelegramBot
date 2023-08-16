using Microsoft.CognitiveServices.Speech;
using NorwayTranslatorTelegramBot.TextToSpeech.Azure;

namespace NorwayTranslatorTelegramBot.TextToSpeech
{
    public class AzureTextToSpeechService : ITextToSpeechService
    {
        private readonly SpeechConfig speechConfig;

        public AzureTextToSpeechService(IAzureSpeechSetting configuration)
        {
            speechConfig = configuration.GetConfuguration();
        }

        public async Task<byte[]> GetSpeechBytesAsync(string text)
        {
            var speechSynthesisResult = await TextToSpeechAsync(text, speechConfig);

            return ExtractBytesIfSuccess(speechSynthesisResult);
        }

        private byte[] ExtractBytesIfSuccess(SpeechSynthesisResult speechSynthesisResult)
        {
            switch (speechSynthesisResult.Reason)
            {
                case ResultReason.SynthesizingAudioCompleted:
                    return speechSynthesisResult.AudioData;
                case ResultReason.Canceled:
                    var cancellation = SpeechSynthesisCancellationDetails.FromResult(speechSynthesisResult);
                    Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                        Console.WriteLine($"CANCELED: Did you set the speech resource key and region values?");
                    }
                    //TODO replace
                    throw new Exception("");
                default:
                    //TODO replace
                    throw new NotImplementedException();
            }
        }

        private async Task<SpeechSynthesisResult> TextToSpeechAsync(string text, SpeechConfig speechConfig)
        {
            using (var speechSynthesizer = new SpeechSynthesizer(speechConfig))
            {
                return await speechSynthesizer.SpeakTextAsync(text);
            }

        }
    }
}