using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CanvasManager : MonoBehaviour
{

    public Button SwitchTurn;
    public Button Play;
    public Button MenuBtn;
    public Button SwitchGameMode;
    public Button Pause;
    private GameManager gameManager;
    public Animator transition;
    public float transitionTime = 1f;
    public bool isPause = false;
    [SerializeField] private GameObject pausePanel;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if(SwitchTurn) SwitchTurn.onClick.AddListener(gameManager.Switch);
        if(MenuBtn) MenuBtn.onClick.AddListener(GoToMenu);
        if (SwitchGameMode) SwitchGameMode.onClick.AddListener(gameManager.ChangeDesignMode);
        if (Play) Play.onClick.AddListener(LoadGame);
        if (Pause) Pause.onClick.AddListener(PauseGame);
    }

    public void LoadGame()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        FindObjectOfType<AudioManager>().Play("click_1");
    }
    public void PauseGame()
    {
        FindObjectOfType<AudioManager>().Play("click_1");
        if(isPause)
        {
            isPause = false;
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
        } else if(!isPause)
        {
            isPause = true;
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
    }

    IEnumerator LoadScene(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

}
