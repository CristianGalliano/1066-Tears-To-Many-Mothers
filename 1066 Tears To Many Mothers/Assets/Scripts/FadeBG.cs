using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeBG : MonoBehaviour
{
    public Sprite[] backgroundImages;
    public Image thisImage;
    public Animator animator;
    private int num = 0;
    private int imageNum = 0;
    public AnimationClip[] animations;
    public float imageDisplayTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fade());
    }

    private void animParameter()
    {
        num++;
        if (num > 1)
        {
            num = 0;
        }
        animator.SetInteger("SwitchFade",num);
    }

    private void changeImage()
    {
        imageNum++;
        if (imageNum > backgroundImages.Length - 1)
        {
            imageNum = 0;
        }
        thisImage.sprite = backgroundImages[imageNum];
    }

    private IEnumerator fade()
    {
        yield return new WaitForSeconds(animations[num].length);
        if (num == 0)
        {
            yield return new WaitForSeconds(imageDisplayTime);
        }
        animParameter();
        if (num == 0)
        {
            changeImage();
        }
        StartCoroutine(fade());
    }
}
