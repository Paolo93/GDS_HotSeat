using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{

    public Button CreditsBtn;
    public Button CreditsBackBtn;

    public RectTransform menuPanel;
    public RectTransform creditsPanel;

    void Start()
    {
        if (CreditsBtn) CreditsBtn.onClick.AddListener(Credits);
        if (CreditsBackBtn) CreditsBackBtn.onClick.AddListener(CreditsBack);
    } 

    public void Credits()
    {
        menuPanel.DOAnchorPos(new Vector2(-1400, 0), 1, false);
        creditsPanel.DOAnchorPos(new Vector2(0, 0), 1, false);
    }

    public void CreditsBack()
    {
        creditsPanel.DOAnchorPos(new Vector2(1400, 0), 1, false);
        menuPanel.DOAnchorPos(new Vector2(0, 0), 1, false);
    }
}
