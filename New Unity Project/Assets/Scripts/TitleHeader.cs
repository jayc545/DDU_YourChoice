using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleHeader : MonoBehaviour
{
    public Image banner;
    public TextMeshProUGUI titleText;
    public string title { get { return titleText.text;} set { titleText.text = value; } }

    public enum DISPLAY_METHOD
    {
        instant,
        slowFade,
        TypeWriter
    }
    public DISPLAY_METHOD displayMetohed = DISPLAY_METHOD.instant;
    public float fadeSpeed = 1;

    public void Show(string displayTitle)
    {
        title = displayTitle;

        if (isRevealing)
            StopCoroutine(revealing);

        revealing = StartCoroutine(Revealing());
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
    IEnumerator Revealing()
    {
        banner.enabled = true;
        titleText.enabled = true;

        switch (displayMetohed)
        {
            case DISPLAY_METHOD.instant:
                banner.color = GlobalF.SetAlpha(banner.color, 1);
                titleText.color = GlobalF.SetAlpha(titleText.color, 1);
                    break;
            case DISPLAY_METHOD.slowFade:
                banner.color = GlobalF.SetAlpha(banner.color, 0);
                titleText.color = GlobalF.SetAlpha(titleText.color, 0);
                while(banner.color.a < 1)
                {
                    banner.color = GlobalF.SetAlpha(banner.color, Mathf.MoveTowards(banner.color.a, 1, fadeSpeed * Time.unscaledDeltaTime));
                    titleText.color = GlobalF.SetAlpha(titleText.color, banner.color.a);
                    yield return new WaitForEndOfFrame();
                }
                break;
            case DISPLAY_METHOD.TypeWriter:
                banner.color = GlobalF.SetAlpha(banner.color, 1);
                titleText.color = GlobalF.SetAlpha(titleText.color, 1);
                // I'll make sure to do this later.
                break;
        }

        revealing = null;
    }
}
