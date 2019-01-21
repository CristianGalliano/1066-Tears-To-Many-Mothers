using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectSide : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void selectSaxon()
    {
        PlayerPrefs.SetInt("side", 0);
        SceneManager.LoadScene("PlayScene");
    }

    public void selectNorman()
    {
        PlayerPrefs.SetInt("side", 1);
        SceneManager.LoadScene("PlayScene");
    }
}
