# Text-to-Speech
> A Speech service feature that converts text to lifelike speech

People talk to, and listen to, other people. It's how we naturally communicate with each other. With the Text to Speech service (Cognitive Services) you can synthesize human speech and make interaction with an automated system more natural. Bringing more natural interactions to scalable and cost-effective automated systems delivers positive customer experiences and drives adoption.

Today, visually impaired people use computers and mobile devices that incorporate text-to-speech software, which highlights text as it is read aloud so people can see and/or hear the content at the same time. 

Web accessibility is essential for people with disabilities and useful for everyone. Learn more about the impact of accessibility and how Microsoft technologies, through Cognitive Services (AI), help in a variety of situations.

## Developing

### Built With
* Microsoft Cognitive Service
* Azure Functions
* .NET Core
* NodeJS
* JavaScript
* Ngrok, Localtunnel or any other debugging tool to tunnel intro your localhost.

### IDE
* Microsoft Visual Studio
* Microsoft Visual Studio Code


### Prerequisites
In order to take this working you need an Azure Subscription. You can get a Free trial at: https://azure.microsoft.com/en-us/free/

## Setting up Dev

Next are the steps that you need to perform in order to get the project up and running:


```shell
git clone https://github.com/carlosmiguelsns/text-to-speech.git
```

...navigate to './TTS-Cognitive Serices' folder and run :

```shell
 npm install
```

...open *.sln file inside './AZ fn' folder. On your Visual Studio open the file **local.settings.json**, and change the **COGNITIVE_SERVICE_KEY** as well as the **COGNITIVE_SERVICE_REGION** props.

```json
 {
    ...

    "COGNITIVE_SERVICE_KEY": "[YOUR-COGNITIVE-SERVICE-KEY]",
    "COGNITIVE_SERVICE_REGION": "[THE-REGION-OF-YOUR-SERVICE]"

    ...
}
```

...Now hit **Start Debuggin (F5)**, this will run the project and install all neccessary dependencies packages.


Once the code is compiled, the AZ function emulator will start.
Allow the proxy to your network if it prompts the window.

Now is time to convert your localhost url, from the Az Fn emulator window, by creating a localhost tunnel to a public url.
Open the Ngrok application and type:

```shell
ngrok http [PORT_NUMBER]
```

The next step is copy the the public url created by the Ngrok and past inside the main.js. Folder './TTS-Cognitive Serices/public/scripts/':

```js
const url = 'https://xxxxxxxxxxxx.ngrok.io/api/tts'
```

Once you've everything in place just navigate with your terminal to './TTS-Cognitive Serices' and type:

```shell
npm run serve
```

### Now it's time to play. Click the *Read text* button on the front-end client and listen to what **Cognite Service - Text-to-Speech service** has done for us.

----

## References
* [Quickstart: Synthesize speech in C# under .NET Framework for Windows](https://github.com/Azure-Samples/cognitive-services-speech-sdk/tree/master/quickstart/csharp/dotnet/text-to-speech)
* [Text-to-speech REST API](https://docs.microsoft.com/en-us/azure/cognitive-services/speech-service/rest-text-to-speech)
* [Get started with text-to-speech](https://docs.microsoft.com/en-us/azure/cognitive-services/speech-service/get-started-text-to-speech?pivots=programming-language-csharp&tabs=script%2Cwindowsinstall)

## References
* [Azure Cognitive Services](https://azure.microsoft.com/en-us/services/cognitive-services/)
* [Text to Speech - A Speech service feature](https://azure.microsoft.com/en-us/services/cognitive-services/text-to-speech/)
* [Audio Content Creation - Optimize text-to-speech voice outpu](https://speech.microsoft.com/audiocontentcreation)
* [Cognitive Services Speech SDK](https://github.com/Azure-Samples/cognitive-services-speech-sdk)
* [Speech Studio](https://speech.microsoft.com/)
