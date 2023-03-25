using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

namespace OpenAI
{
    public class DallE : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Button button;
        [SerializeField] private Image image;
        [SerializeField] private GameObject loadingLabel;

        private OpenAIApi openai = new OpenAIApi();

        public bool busy = false;

        private void Start()
        {
            button.onClick.AddListener(SendImageRequest);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return) && (inputField.text != "" || inputField != null) && busy == false)
            {
                busy = true;
                SendImageRequest();
            }
        }

        private async void SendImageRequest()
        {
            image.sprite = null;
            button.enabled = false;
            inputField.enabled = false;
            loadingLabel.SetActive(true);

            var response = await openai.CreateImage(new CreateImageRequest
            {
                Prompt = inputField.text,
                Size = ImageSize.Size256
            });

            inputField.text = "";

            if (response.Data != null && response.Data.Count > 0)
            {
                using(var request = new UnityWebRequest(response.Data[0].Url))
                {
                    request.downloadHandler = new DownloadHandlerBuffer();
                    request.SetRequestHeader("Access-Control-Allow-Origin", "*");
                    request.SendWebRequest();

                    while (!request.isDone) await Task.Yield();

                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(request.downloadHandler.data);
                    var sprite = Sprite.Create(texture, new Rect(0, 0, 256, 256), Vector2.zero, 1f);
                    image.sprite = sprite;
                }
            }
            else
            {
                Debug.LogWarning("No image was created from this prompt.");
            }

            busy = false;
            button.enabled = true;
            inputField.enabled = true;
            loadingLabel.SetActive(false);
        }

        public void BackToMenuButton()
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}