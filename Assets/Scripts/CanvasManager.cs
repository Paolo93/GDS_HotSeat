using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CanvasManager : MonoBehaviour
{

    public Button SwitchTurn;
    public Button Play;
    public Button MenuBtn;
    private GameManager gameManager;

    public Animator transition;
    public float transitionTime = 1f;


    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if(SwitchTurn) SwitchTurn.onClick.AddListener(gameManager.Switch);
        if(MenuBtn) MenuBtn.onClick.AddListener(GoToMenu);

        if (Play) Play.onClick.AddListener(LoadGame);

    }

    public void LoadGame()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void GoToMenu()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex - 1));
    }


    IEnumerator LoadScene(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

}
