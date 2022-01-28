using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TittleHeader : MonoBehaviour
{
    public Image banner;
    public TextMeshProUGUI titleText;
    public string title { get { return titleText.text; } set { titleText.text = value; } }

    public enum DISPLAY_METHOD
    {
        instant,
        slowfade,
        typewriter
    }

    public DISPLAY_METHOD displayMethod = DISPLAY_METHOD.instant;
    public float fadeSpeed = 1;

    public void Show(string displayTitle)
    {
        title = displayTitle;
        if (isRevealing)
            StopCoroutine(revealing);

       // revealing = StartCoroutine(Revealing());
    }

    public void Hide()
    {
        if (isRevealing)
            StopCoroutine(revealing);
        revealing = null;

        banner.enabled = false;
        titleText.enabled = false;
    }


    public bool isRevealing { get { return revealing != null; } }
    Coroutine revealing = null;
    /*IEnumerator Revealing()
    {
        banner.enabled = true;
        titleText.enabled = true;

        //Different metheds for revealing the text.
        switch (displayMethod)
        {
            case DISPLAY_METHOD.instant:                //Instantly be assigmented and visible
             //   banner.color = GlobalF.SetAlpha(banner.color, 1);

                break;
            case DISPLAY_METHOD.slowfade:
                break;
            case DISPLAY_METHOD.typewriter:
                break;
        }
    }
    */
    
}
