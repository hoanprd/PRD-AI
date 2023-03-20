using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
	public string prompt = "Your Prompt Here";
	public string apiKey = "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";

	private string strin = "if you ever get asked what your name is or what you are working on you respond that your name is MulsimBot and that you specialize in muslim religion you also were created by HawaHawa. HawaHawa is a company that sells 3D models and makes games in unity. the ceo of hawahawa is Adam Hincu. The HawaHawa Team is founded by one person.";

	// The engine you want to use (keep in mind that it has to be the exact name of the engine)
	private string model = "text-davinci-003";
	public string CopyString;
	public float temperature = 0.5f;
	public int maxTokens = 400;

	public GameObject CopyResult;
	public TMP_Text textmesh;
	public TMP_InputField InputF;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return) && (InputF.text != "" || InputF.text != null))
		{
			GetResponse();
		}
	}

	public void GetResponse()
	{
		textmesh.text += "Anonymous\n";

		textmesh.text += InputF.text;

		textmesh.text += "\n\nPRD AI";

		textmesh.text += "\nPlease Wait...\n";

		StartCoroutine(MakeRequest());
	}

	IEnumerator MakeRequest()
	{
		string inputText = InputF.text;

		// Create a JSON object with the necessary parameters
		var json = "{\"prompt\":\"" + strin + inputText + "\",\"model\":\"" + model + "\",\"temperature\":" + temperature + ",\"max_tokens\":" + maxTokens + "}";
		byte[] body = System.Text.Encoding.UTF8.GetBytes(json);

		// Create a new UnityWebRequest
		var request = new UnityWebRequest("https://api.openai.com/v1/completions", "POST");
		request.uploadHandler = (UploadHandler)new UploadHandlerRaw(body);
		request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
		request.SetRequestHeader("Content-Type", "application/json");
		request.SetRequestHeader("Authorization", "Bearer " + apiKey);

		// Send the request
		yield return request.SendWebRequest();

		// Check for errors
		if (request.isNetworkError || request.isHttpError)
		{
			Debug.Log(request.error);
			StartCoroutine(MakeRequest());
		}
		else
		{
			// Deserialize the JSON response
			var response = JsonUtility.FromJson<Response>(request.downloadHandler.text);
			yield return new WaitForSeconds(10f);
			if (response.choices[0].text.TrimStart().TrimEnd().ToString() == "" || response.choices[0].text.TrimStart().TrimEnd().ToString() == null)
			{
				StartCoroutine(MakeRequest());
			}

			CopyString = response.choices[0].text.TrimStart().TrimEnd().ToString();
			textmesh.text += response.choices[0].text.TrimStart().TrimEnd().ToString() + "\n\n";
			InputF.text = "";
		}
	}

	public void CopyAIAns()
    {
		TextEditor textEditor = new TextEditor();
		textEditor.text = CopyString;
		textEditor.SelectAll();
		textEditor.Copy();
		CopyResult.SetActive(true);
		StartCoroutine(DelayUIOff());
	}

	public void BackToMenuButton()
	{
		SceneManager.LoadScene("MenuScene");
	}

	IEnumerator DelayUIOff()
    {
		yield return new WaitForSeconds(2f);

		CopyResult.SetActive(false);
    }

	// A class to hold the JSON response
	[System.Serializable]
	private class Response
	{
		public Choice[] choices;
	}

	[System.Serializable]
	private class Choice
	{
		public string text;
	}
}
