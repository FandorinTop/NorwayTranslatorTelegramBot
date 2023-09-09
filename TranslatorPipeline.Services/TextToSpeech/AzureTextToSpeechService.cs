using Microsoft.CognitiveServices.Speech;
using NorwayTranslatorTelegramBot.TextToSpeech.Azure;
using System.Runtime.InteropServices;
using TelegramTranslator.Common;

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

        private static byte[] ExtractBytesIfSuccess(SpeechSynthesisResult speechSynthesisResult)
        {
            switch (speechSynthesisResult.Reason)
            {
                case ResultReason.SynthesizingAudioCompleted:
                    return speechSynthesisResult.AudioData;
                case ResultReason.Canceled:
                    var cancellation = SpeechSynthesisCancellationDetails.FromResult(speechSynthesisResult);

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        throw new AzureSpeechSynthesisException(
                            "Azure Speech Synthesis cancelation error",
                            cancellation.ErrorCode.ToString(),
                            cancellation.ErrorDetails);
                    }

                    throw new AzureSpeechSynthesisException(
                        "Azure Speech Synthesis cancelate", 
                        cancellation.ErrorCode.ToString(),
                        cancellation.ErrorDetails,
                        speechSynthesisResult.Reason.ToString());
                default:
                    throw new NotImplementedException($"Current ResultReason is not implemented: {speechSynthesisResult.Reason}");
            }
        }

        private async static Task<SpeechSynthesisResult> TextToSpeechAsync(string text, SpeechConfig speechConfig)
        {
            using var speechSynthesizer = new SpeechSynthesizer(speechConfig);
            return await speechSynthesizer.SpeakTextAsync(text);
        }
    }
}