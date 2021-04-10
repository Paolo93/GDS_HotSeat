using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorFollow : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Menu")
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
    }
    private void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;
    }
}
