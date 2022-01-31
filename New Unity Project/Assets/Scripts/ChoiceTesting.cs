using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceTesting : MonoBehaviour
{
    public string title = "Make a choice";

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChoiceScreen.Show(title, "Dogs");
    }
}
