namespace NorwayTranslatorTelegramBot
{

    internal partial class Program
    {
        public static async Task DownloadFileTaskAsync(HttpClient client, Uri uri, string FileName)
        {
            using var s = await client.GetStreamAsync(uri);
            using var fs = new FileStream(FileName, FileMode.CreateNew);
            await s.CopyToAsync(fs);
        }

        static Task Main()
        {
            return Task.CompletedTask;
        }
    }
}