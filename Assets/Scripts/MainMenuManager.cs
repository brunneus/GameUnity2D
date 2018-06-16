using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    public string levelToLoad;
    public Button newGameButton;
	public Button quitGameButton;
	public Button helpButton;
	public GameObject canvas;
	public static GameObject canvass;

    private static AudioSource soundWithVoice;
    private static AudioSource singleSound;
    private static bool started;

    private static MainMenuManager instance = null;

    void Awake()
    {
        if (instance != null && instance != this)
        {	Destroy(this.gameObject);
			Destroy (canvas);
        }
        else
        {
			canvass = canvas;
            newGameButton.onClick.AddListener(NewGame);
			quitGameButton.onClick.AddListener(QuitGame);
			helpButton.onClick.AddListener(CallHelpScene);

            AudioSource[] sounds = GetComponents<AudioSource>();
            soundWithVoice = sounds[0];
            singleSound = sounds[1];
			DontDestroyOnLoad(this.gameObject);
			DontDestroyOnLoad(canvas);

            instance = this;
        }

		soundWithVoice.enabled = true;
		singleSound.enabled = false;
		canvass.SetActive (true);
    }

    public void NewGame() {
		canvass.SetActive (false);
		GameManagerScript.lastSceneName = levelToLoad;
        SceneManager.LoadScene(levelToLoad);

        soundWithVoice.enabled = false;
        StartCoroutine(PlayGameSound());
    }

    public void QuitGame() {
        Application.Quit(); 
    }

	public void CallHelpScene() {
		canvass.SetActive (false);
		SceneManager.LoadScene ("AlertScene");
	}

    private IEnumerator PlayGameSound()
    {
        yield return new WaitForSeconds(5);
        singleSound.enabled = true;
    }
}