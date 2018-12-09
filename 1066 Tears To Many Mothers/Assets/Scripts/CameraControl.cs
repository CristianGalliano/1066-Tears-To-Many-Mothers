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


    // Use this for initialization
    void Start ()
    {
        Camera1.gameObject.SetActive(true);
        Camera2.gameObject.SetActive(false);
        Camera3.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void switchCamera()
    {

        if (cameraNum == 1)
        {
            Camera1.gameObject.SetActive(false);
            Camera2.gameObject.SetActive(true);
            Camera3.gameObject.SetActive(false);
            cameraNum++;
        }
        else if (cameraNum == 2)
        {
            Camera1.gameObject.SetActive(false);
            Camera2.gameObject.SetActive(false);
            Camera3.gameObject.SetActive(true);
            cameraNum++;
        }
        else
        {
            Camera1.gameObject.SetActive(true);
            Camera2.gameObject.SetActive(false);
            Camera3.gameObject.SetActive(false);
            cameraNum = 1;
        }
    }
}
