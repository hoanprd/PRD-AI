using System;
using UnityEngine;
using UnityEngine.UI;

namespace OpenAI
{
    public class Whisper : MonoBehaviour
    {
        [SerializeField] private Button recordButton;
        [SerializeField] private Image progressBar;
        //[SerializeField] private Text message;
        [SerializeField] private InputField inputField;
        [SerializeField] private Dropdown dropdown;

        public GameObject PB, SB;

        private readonly string fileName = "output.wav";
        private readonly int duration = 5;
        
        private AudioClip clip;
        private bool isRecording;
        private float time;
        private OpenAIApi openai = new OpenAIApi();

        private void Start()
        {
            foreach (var device in Microphone.devices)
            {
                dropdown.options.Add(new Dropdown.OptionData(device));
            }
            recordButton.onClick.AddListener(StartRecording);
        }

        private void StartRecording()
        {
            isRecording = true;
            PB.SetActive(true);
            SB.SetActive(false);
            recordButton.enabled = false;
            
            clip = Microphone.Start(dropdown.options[dropdown.value].text, false, duration, 44100);
        }

        private async void EndRecording()
        {
            //message.text = "Transcripting...";
            inputField.text = "Transcripting...";

            PB.SetActive(false);
            SB.SetActive(true);
            isRecording = false;
            time = 0;
            Microphone.End(null);
            byte[] data = SaveWav.Save(fileName, clip);
            
            var req = new CreateAudioTranscriptionsRequest
            {
                FileData = new FileData() {Data = data, Name = "audio.wav"},
                // File = Application.persistentDataPath + "/" + fileName,
                Model = "whisper-1",
                Language = "en"
            };
            var res = await openai.CreateAudioTranscription(req);

            //message.text = res.Text;
            inputField.text = res.Text;
            recordButton.enabled = true;
        }

        private void Update()
        {
            if (isRecording)
            {
                time += Time.deltaTime;
                progressBar.fillAmount = time / duration;

                if (time >= duration)
                {
                    isRecording = false;
                    EndRecording();
                }
            }
        }
    }
}
