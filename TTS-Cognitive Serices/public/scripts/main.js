(function () {

  document.getElementById('tts-content__get').addEventListener('click', function () {
    document.getElementById('tts-content__get').setAttribute("disabled", "disabled");
    makeRequest();
  });

  function makeRequest() {
    const url = `https://e5bb8e71d4ab.ngrok.io/api/tts`;
    const xhttp = new XMLHttpRequest();

    xhttp.onreadystatechange = async () => {
      await parseAPIResponse(xhttp);
    };

    xhttp.onreadystatechange = async () => {
      await parseAPIResponse(xhttp);
    };
    
    xhttp.open("POST", url);
    xhttp.setRequestHeader("Content-Type", "application/json");
    xhttp.responseType = 'blob';

    xhttp.send(JSON.stringify(
      {
        "voice": document.getElementById('tts-voice__language').value,
        "prosody": {
          "pitch": document.getElementById('tts-prosody__pitch').value,
          "rate": document.getElementById('tts-prosody__rate').value,
          "volume": document.getElementById('tts-prosody__volume').value
        },
        "content": document.getElementById('tts-content__input').value
      }
    ));

  }

  async function parseAPIResponse(xhttp) {

    if (xhttp.readyState === XMLHttpRequest.DONE) {

      document.getElementById('tts-content__get').removeAttribute("disabled");

      if (xhttp.status >= 200 && xhttp.status < 300) {

        const response = xhttp.response;
        var blob = new Blob([response], { type: 'audio/ogg' });

        var url = window.URL.createObjectURL(blob);
        var audio = document.getElementById('tts-audio__player');

        window.audio = new Audio();
        window.audio.src = url;
        window.audio.crossOrigin = 'anonymous';
        window.audio.play();
      }
    }
  }

}());