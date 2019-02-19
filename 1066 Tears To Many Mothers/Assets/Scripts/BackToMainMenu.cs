using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    public KeyCode backMenu;
    public string exitMenu;
    public GameObject EscMenu;
    public GameObject OptMenu;
    public Button ResumeButton;
    public Button OptionButton;
    public Button MainButton;
    public Button ExitButton;
    public Button SoundButton;
    public Button BackButton;





    void Start()
    {
        EscMenu.gameObject.SetActive(false);
        OptMenu.gameObject.SetActive(false);
      
    }

    void Update()
    {
        if (Input.GetKeyDown(backMenu))
        {
            EscMenu.gameObject.SetActive(true);
            ResumeButton.onClick.AddListener(ResumeOnClick);
            OptionButton.onClick.AddListener(OptionOnClick);
            MainButton.onClick.AddListener(MainOnClick);
            ExitButton.onClick.AddListener(ExitOnClick);
            //SoundButton.onClick.AddListener();
            BackButton.onClick.AddListener(BackOnClick);
        }
        
    }

    void ResumeOnClick()
    {
        EscMenu.gameObject.SetActive(false);
    }

    void OptionOnClick()
    {
        EscMenu.gameObject.SetActive(false);
        OptMenu.gameObject.SetActive(true);
    }
    
    void MainOnClick()
    {
        SceneManager.LoadScene(exitMenu);
    }

    void ExitOnClick()
    {
        Application.Quit();
        Debug.Log("Exit");
    }

    void BackOnClick()
    {
        EscMenu.gameObject.SetActive(true);
        OptMenu.gameObject.SetActive(false);
    }

}