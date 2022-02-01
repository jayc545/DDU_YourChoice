using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalF : MonoBehaviour
{
    public static bool TransitionImages(ref ImagePosition activeImage, ref List<Image> allImages, float speed, bool smooth)
    {
        bool anyValyeChanges = false;

        speed *= Time.deltaTime;
        for (int i = allImages.Count - 1; i >= 0; i--)
        {
            Image image = allImages[i];
        }

        return anyValyeChanges;
    }
}
