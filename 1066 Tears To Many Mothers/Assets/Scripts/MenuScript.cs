using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private GameObject mainPanel;//set variables for UI panels.
    private GameObject optionsPanel;

	void Start ()// Use this for initialization
    {
        mainPanel = GameObject.Find("MainPanel");//find panels in the scene hierachy by name.
        optionsPanel = GameObject.Find("OptionsPanel");
        optionsPanel.gameObject.SetActive(false);//set this panel to be inactive at start.
	}
	
	void Update ()// Update is called once per frame
    {
		
	}

    public void openOptions()//runs when options button is clicked.
    {
        mainPanel.gameObject.SetActive(false);
        optionsPanel.gameObject.SetActive(true);
    }

    public void back()//runs when the back button in the options menu is clicked.
    {
        optionsPanel.gameObject.SetActive(false);
        mainPanel.gameObject.SetActive(true);
    }

    public void start()//runs when the start button is clicked.
    {
        SceneManager.LoadScene("PlayScene");//load scene by calling the scenes name.
    }

    public void exit()//runs when exit button is clicked.
    {
        Application.Quit();//quit the application.
    }
}
