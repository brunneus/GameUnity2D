using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    public string levelToLoad;
    public Button newGameButton;
	public Button quitGameButton;
	public Button helpButton;
    private static Button newGameButtons;
	private static Button quitGameButtons;
	private static Button helpButtons;

    private static AudioSource soundWithVoice;
    private static AudioSource singleSound;
    private static bool started;

    private static MainMenuManager instance = null;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            soundWithVoice.enabled = true;
            singleSound.enabled = false;
            newGameButtons.onClick.AddListener(NewGame);
			quitGameButtons.onClick.AddListener(QuitGame);
			helpButtons.onClick.AddListener(CallHelpScene);

            Destroy(this.gameObject);
            return;
        }
        else
        {
            newGameButtons = newGameButton;
            quitGameButtons = quitGameButton;
			helpButtons = helpButton;
            newGameButtons.onClick.AddListener(NewGame);
			quitGameButtons.onClick.AddListener(QuitGame);
			helpButtons.onClick.AddListener(CallHelpScene);

            AudioSource[] sounds = GetComponents<AudioSource>();
            soundWithVoice = sounds[0];
            singleSound = sounds[1];
            DontDestroyOnLoad(this.gameObject);

            soundWithVoice.enabled = true;
            singleSound.enabled = false;
            
            instance = this;
        }
    }

    public void NewGame() {
		GameManagerScript.lastSceneName = levelToLoad;
        SceneManager.LoadScene(levelToLoad);

        soundWithVoice.enabled = false;
        StartCoroutine(PlayGameSound());
    }

    public void QuitGame() {
        Application.Quit(); 
    }

	public void CallHelpScene() {
		SceneManager.LoadScene ("AlertScene");
	}

    private IEnumerator PlayGameSound()
    {
        yield return new WaitForSeconds(5);
        singleSound.enabled = true;
    }
}