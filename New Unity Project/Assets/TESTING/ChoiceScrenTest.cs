using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceScrenTest : MonoBehaviour
{
    public string title = "Make a choice";
    public string[] choices;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChoiceScreen.Show(title, choices);
    }
    /*
    IEnumerator DynamicStoryExample()
    {
        List
    }
    */
}
