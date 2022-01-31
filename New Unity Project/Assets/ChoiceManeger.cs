using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManeger : MonoBehaviour
{

    public GameObject Panel;

    public Text textbox;


    public void PanelOpener()
    {
        if (textbox.text == "Love")
        {
            Panel.SetActive(true);
        }
    }
}
