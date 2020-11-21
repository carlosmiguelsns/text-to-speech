using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.CognitiveServices.Speech;
using Microsoft.Net.Http.Headers;
using System.Xml.Linq;
using System;

namespace FunctionApp_TTS
{
    public static class Function1
    {
        // COGNITVE SERVICE SETTINGS
        public static string COGNITIVE_SERVICE_KEY      = Environment.GetEnvironmentVariable("COGNITIVE_SERVICE_KEY");
        public static string COGNITIVE_SERVICE_REGION   = Environment.GetEnvironmentVariable("COGNITIVE_SERVICE_REGION");

        // SSML Object customization 
        public class SpeakSSML
        {
            public string voice { get; set; }
            public Prosody prosody { get; set; }
            public string content { get; set; }
        }

        public class Prosody
        {
            public string pitch { get; set; }
            public string rate { get; set; }
            public string volume { get; set; }
        }


        [FunctionName("tts")]
        public static async Task<FileContentResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            // Creates an instance of a speech config with specified subscription key and service region.
            // Replace with your own subscription key and service region (e.g., "westus").
            // The default language is "en-us".
            var config = SpeechConfig.FromSubscription(COGNITIVE_SERVICE_KEY, COGNITIVE_SERVICE_REGION);

            // Customize audio format
            config.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Audio16Khz32KBitRateMonoMp3);
            
            using (var synthesizer = new SpeechSynthesizer(config, null))
            {
                string requestBody  = await new StreamReader(req.Body).ReadToEndAsync();
                SpeakSSML data      = JsonConvert.DeserializeObject<SpeakSSML>(requestBody);

                // Values from the JSON Body request
                //{
                // "voice": "en-US-AriaRUS",
                // "prosody": {
                //  "pitch": "x-low",
                //  "rate": "low",
                //  "volume": "medium"
                // }
                // "content": "ola"
                //}

                // Fine-tune the pitch, pronunciation, speaking rate, volume, and more of the text-to-speech output by submitting:
                string ssml = Generate_SSML(data?.content, data?.voice, data?.prosody?.pitch, data?.prosody?.rate, data?.prosody?.volume);

                //var file = await synthesizer.SpeakTextAsync(data.content);
                var file = await synthesizer.SpeakSsmlAsync(ssml);
                return new FileContentResult(file.AudioData, new MediaTypeHeaderValue("application/octet-stream"));
            }
        }


        #region private Generate SSML

        /// <summary>
        /// Speech Synthesis Markup Language (SSML) allows fine-tune the pitch, pronunciation, speaking rate, volume, and more of the text-to-speech output by submitting your requests from an XML schema. 
        /// </summary>
        private static string Generate_SSML(string content, string voice, string pitch, string speakingrate, string volume)
        {
            content         = !string.IsNullOrEmpty(content) ? content : "nothing to read";
            voice           = !string.IsNullOrEmpty(voice) ? voice : "en-US-AriaRUS";
            pitch           = !string.IsNullOrEmpty(pitch) ? pitch : "default";
            speakingrate    = !string.IsNullOrEmpty(speakingrate) ? speakingrate: "default";
            volume          = !string.IsNullOrEmpty(volume) ? volume: "default";

            //string prosodyString = "<speak version='1.0' xmlns='https://www.w3.org/2001/10/synthesis' xmlns:mstts='https://www.w3.org/2001/mstts' xml:lang='en-US'>" +
            //"<voice name='" + voice + "'>" +
            //                            "<mstts:express-as style='cheerful'>" +
            //                              content +
            //                            "</mstts:express-as>" +
            //                         "</voice>" +
            //                        "</speak>";

            string prosodyString = string.Empty;
            prosodyString = "<speak version='1.0' xmlns='https://www.w3.org/2001/10/synthesis' xml:lang='en-US'>" +
                              "<voice name='" + voice + "'>" +
                                "<prosody pitch='" + pitch + "' rate='" + speakingrate + "' volume='" + volume + "'>" +
                                  content +
                                "</prosody>" +
                             "</voice>" +
                            "</speak>";

            var xml = XDocument.Parse(prosodyString);
            return xml.ToString();
        }

        #endregion
    }
}
