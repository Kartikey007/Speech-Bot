using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;

namespace Speech.Recognition
{
    class Program
    {

        static async Task Main()
        {
            string x;
            string textone = "";
            Console.WriteLine("1 - English", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.WriteLine("2 - Francais", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.WriteLine("3 - Espanol", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.WriteLine("4 - Deutsch", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.WriteLine("5 - Romana", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.WriteLine("Please choose the language that you will use:", Console.ForegroundColor = ConsoleColor.Yellow);
            x = Console.ReadLine();
            Console.WriteLine();

            await RecognizeSpeechAsync(x);

            Console.WriteLine();

            Console.WriteLine("Please press any key to continue closing this window...");
            Console.ReadKey();
        }

        static async Task<string> RecognizeSpeechAsync(string x)
        {
            var config =
                SpeechConfig.FromSubscription(
                    "YOUR-AZURE-SPEECH-KEY",
                    "YOUR-AZURE-SPEECH-LOCATION");

            string language = "";
            if (x == "1") { language = "en-US"; };
            if (x == "2") { language = "fr-FR"; };
            if (x == "3") { language = "es-ES"; };
            if (x == "4") { language = "de-DE"; };
            if (x == "5") { language = "ro-RO"; };

            using var recognizer = new SpeechRecognizer(config, language);

            var result = await recognizer.RecognizeOnceAsync();
            switch (result.Reason)
            {
                case ResultReason.RecognizedSpeech:
                    Console.WriteLine("This is the speech-to-text transformation:", Console.ForegroundColor = ConsoleColor.Blue);
                    Console.WriteLine($"{result.Text}", Console.ForegroundColor = ConsoleColor.White);
                    break;
                case ResultReason.NoMatch:
                    Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                    break;
                case ResultReason.Canceled:
                    var cancellation = CancellationDetails.FromResult(result);
                    Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                        Console.WriteLine($"CANCELED: Did you update the subscription info?");
                    }
                    break;
            }
            return result.Text;
        }
        
    }
}
