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
    public Button PauseBtn;
    private GameManager gameManager;
    public Animator transition;
    public float transitionTime = 1f;
    public bool isPause = false;
    [SerializeField] private GameObject pausePanel;

    public GameObject greenTurnLight, blueturnLight;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if(SwitchTurn) SwitchTurn.onClick.AddListener(gameManager.Switch);
        if(MenuBtn) MenuBtn.onClick.AddListener(GoToMenu);
        if (SwitchGameMode) SwitchGameMode.onClick.AddListener(gameManager.ChangeDesignMode);
        if (Play) Play.onClick.AddListener(LoadGame);
        if (PauseBtn) PauseBtn.onClick.AddListener(PauseGame);
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
            Resume();
        } 
        else if(!isPause)
        {
            Pause();
        }
    }

    private void Pause()
    {
        isPause = true;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    private void Resume()
    {
        isPause = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void SwitchLight(int turn)
    {
        turn = gameManager.playerTurn;
        if(turn == 2)
        {
            blueturnLight.SetActive(false);
            greenTurnLight.SetActive(true);
        } else if(turn == 1)
        {
            blueturnLight.SetActive(true);
            greenTurnLight.SetActive(false);
        }
    }

    IEnumerator LoadScene(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

}
