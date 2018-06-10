using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    public string levelToLoad;
    public Button newGameButton;
    public Button quitGameButton;
    private static Button newGameButtons;
    private static Button quitGameButtons;

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

            Destroy(this.gameObject);
            return;
        }
        else
        {
            newGameButtons = newGameButton;
            quitGameButtons = quitGameButton;
            newGameButtons.onClick.AddListener(NewGame);
            quitGameButtons.onClick.AddListener(QuitGame);

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
        SceneManager.LoadScene(levelToLoad);

        soundWithVoice.enabled = false;
        StartCoroutine(PlayGameSound());
    }

    public void QuitGame() {
        Application.Quit(); 
    }

    private IEnumerator PlayGameSound()
    {
        yield return new WaitForSeconds(5);
        singleSound.enabled = true;
    }
}