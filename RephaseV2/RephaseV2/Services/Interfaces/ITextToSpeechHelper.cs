using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.TextToSpeech.Abstractions;

namespace RephaseV2.Services.Interfaces
{
    public interface ITextToSpeechHelper
    {
        /// <summary>
        /// Converts text to audio using system audio output.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        Task ConvertTextToSpeechAsync(string text);

        /// <summary>
        /// Retrieves a list of installed languages for use when converting text to speech.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CrossLocale>> GetInstalledLanguagesAsync();
    }
}