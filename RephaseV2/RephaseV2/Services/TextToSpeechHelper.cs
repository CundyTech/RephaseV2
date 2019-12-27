using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.TextToSpeech;
using Plugin.TextToSpeech.Abstractions;
using RephaseV2.Services.Interfaces;

namespace RephaseV2.Services
{
    class TextToSpeechHelper : ITextToSpeechHelper
    {
        /// <summary>
        /// Converts text to audio using system audio output.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
		public async Task ConvertTextToSpeechAsync(string text)
		{
			await CrossTextToSpeech.Current.Speak(text);
		}

        /// <summary>
        /// Retrieves a list of installed languages for use when converting text to speech.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CrossLocale>> GetInstalledLanguagesAsync()
	    {
	       return await CrossTextToSpeech.Current.GetInstalledLanguages();
        }
    }
}