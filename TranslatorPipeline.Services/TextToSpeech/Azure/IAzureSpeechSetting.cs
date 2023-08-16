using Microsoft.CognitiveServices.Speech;

namespace NorwayTranslatorTelegramBot.TextToSpeech.Azure
{
    public interface IAzureSpeechSetting
    {
        public SpeechConfig GetConfuguration();
    }
}