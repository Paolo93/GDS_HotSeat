using UnityEngine;

public class CursorFollow : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
    }
    private void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;
    }
}
