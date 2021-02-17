using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

    public Button SwitchTurn;
    // Start is called before the first frame update

    private GameManager gameManager;

   
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        SwitchTurn.onClick.AddListener(gameManager.Switch);
    }


}
