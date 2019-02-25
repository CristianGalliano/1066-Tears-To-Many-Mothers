using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
    public Camera Camera1;
    public Camera Camera2;
    public Camera Camera3;
    int cameraNum = 1;

    public GameController controller;


    // Use this for initialization
    void Start ()
    {
        if (PlayerPrefs.GetInt("side") == 1)
        {
            Camera1.gameObject.SetActive(true);
            Camera2.gameObject.SetActive(false);
            Camera3.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("side") == 0)
        {
            Camera1.gameObject.SetActive(false);
            Camera2.gameObject.SetActive(true);
            Camera3.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void changeImage(int i)
    {
        if (i == 0)
        {
            Camera1.gameObject.SetActive(true);
            Camera2.gameObject.SetActive(false);
            Camera3.gameObject.SetActive(false);
        }
        else if (i == 1)
        {
            Camera1.gameObject.SetActive(false);
            Camera2.gameObject.SetActive(true);
            Camera3.gameObject.SetActive(false);
        }
    }

    public void switchTopDown()
    {
        if (controller.camCount == 0)
        {
            Camera1.gameObject.SetActive(false);
            Camera2.gameObject.SetActive(false);
            Camera3.gameObject.SetActive(true);
            controller.camCount++;
        }
        else if (controller.camCount == 1)
        {
            if (controller.turn == 0)
            {
                Camera1.gameObject.SetActive(true);
                Camera2.gameObject.SetActive(false);
                Camera3.gameObject.SetActive(false);
                controller.camCount = 0;
            }
            else if (controller.turn == 1)
            {
                Camera1.gameObject.SetActive(false);
                Camera2.gameObject.SetActive(true);
                Camera3.gameObject.SetActive(false);
                controller.camCount = 0;
            }
        }
    }
}
