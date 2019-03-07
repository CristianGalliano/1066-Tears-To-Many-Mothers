using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeAlphaImagesMain : MonoBehaviour
{
    public Image imageOne;
   //public Image imageTwo;
    //public Image imageThree;
    //public Image imageFour;


    void Start()
    {
        imageOne = GetComponent<Image>();
        Color a = imageOne.color;
        a.a = 0;
        imageOne.color = a;

        /*imageTwo = GetComponent<Image>();
        Color b = imageTwo.color;
        b.a = 0;
        imageTwo.color = b;

        imageThree = GetComponent<Image>();
        Color c = imageThree.color;
        c.a = 0;
        imageThree.color = c;

        imageFour = GetComponent<Image>();
        Color d = imageFour.color;
        d.a = 0;
        imageFour.color = d; */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
