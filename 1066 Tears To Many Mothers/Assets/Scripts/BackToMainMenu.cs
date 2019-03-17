using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    public Image rulesImage;
    private int pageNum = 0;
    public Text pageNumberText;
    private bool rulesOpen = false;
    public Sprite[] rulesSprites;
    public Scrollbar vertBar;

    public GameObject menuPanel, optionsPanel, rulesPanel, mainUI, colliders;

    private bool menuOpen = false;

    void Start()
    {
        menuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        rulesPanel.SetActive(false);
        mainUI.SetActive(true);
    }

    void Update()
    {
        if (rulesOpen == true)
        {
            pageNumberText.text = (pageNum + 1) + "/" + rulesSprites.Length;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuOpen == false)
            {
                menuOpen = true;
                menuPanel.SetActive(true);
                colliders.SetActive(true);
                mainUI.SetActive(false);
            }
            else if (menuOpen == true)
            {
                menuOpen = false;
                menuPanel.SetActive(false);
                optionsPanel.SetActive(false);
                rulesPanel.SetActive(false);
                colliders.SetActive(false);
                mainUI.SetActive(true);
            }
        }
    }


    public void nextPage()
    {
        pageNum++;
        if (pageNum > rulesSprites.Length - 1)
        {
            pageNum = 0;
        }
        rulesImage.GetComponent<Image>().sprite = rulesSprites[pageNum];
        vertBar.value = 1;
    }

    public void prevPage()
    {
        pageNum--;
        if (pageNum < 0)
        {
            pageNum = rulesSprites.Length - 1;
        }
        rulesImage.GetComponent<Image>().sprite = rulesSprites[pageNum];
        vertBar.value = 1;
    }

    public void resumeGame()
    {
        menuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        rulesPanel.SetActive(false);
        menuOpen = false;
        mainUI.SetActive(true);
        colliders.SetActive(false);
    }

    public void returnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void openOptions()
    {
        menuPanel.SetActive(false);
        optionsPanel.SetActive(true);
        rulesPanel.SetActive(false);

        rulesOpen = false;
    }

    public void openRules()
    {
        menuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        rulesPanel.SetActive(true);
        pageNum = 0;
        rulesImage.GetComponent<Image>().sprite = rulesSprites[pageNum];
        vertBar.value = 1;
        rulesOpen = true;
    }

    public void back()
    {
        menuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        rulesPanel.SetActive(false);

        rulesOpen = false;
    }
}