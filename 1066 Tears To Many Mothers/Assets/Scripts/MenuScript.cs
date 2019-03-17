using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject mainPanel, optionsPanel, rulesPanel;//set variables for UI panels.
    public Image rulesImage;
    int pageNum = 0;
    public Text pageNumberText;
    private bool rulesOpen = false;
    public Sprite[] rulesSprites;
    public Scrollbar vertBar;

	void Start ()// Use this for initialization
    {
        optionsPanel.gameObject.SetActive(false);//set this panel to be inactive at start.
        rulesPanel.gameObject.SetActive(false);
    }
	
	void Update ()// Update is called once per frame
    {
        if (rulesOpen == true)
        {
            pageNumberText.text = (pageNum + 1) + "/" + rulesSprites.Length;
        }
	}

    public void openOptions()//runs when options button is clicked.
    {
        mainPanel.gameObject.SetActive(false);
        rulesPanel.gameObject.SetActive(false);
        optionsPanel.gameObject.SetActive(true);

        rulesOpen = false;
    }

    public void openRules()//runs when options button is clicked.
    {
        mainPanel.gameObject.SetActive(false);
        rulesPanel.gameObject.SetActive(true);
        optionsPanel.gameObject.SetActive(false);
        pageNum = 0;
        rulesImage.GetComponent<Image>().sprite = rulesSprites[pageNum];
        vertBar.value = 1;

        rulesOpen = true;
    }

    public void back()//runs when the back button in the options menu is clicked.
    {
        optionsPanel.gameObject.SetActive(false);
        rulesPanel.gameObject.SetActive(false);
        mainPanel.gameObject.SetActive(true);

        rulesOpen = false;
    }

    public void start()//runs when the start button is clicked.
    {
        SceneManager.LoadScene("SelectionScene");//load scene by calling the scenes name.
    }

    public void exit()//runs when exit button is clicked.
    {
        Application.Quit();//quit the application.
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
}
