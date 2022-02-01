using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScrenTesting : MonoBehaviour
{
    public string displayTittle = "";

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
            InputScreen.Show(displayTittle);


        if (Input.GetKey(KeyCode.Return) && InputScreen.isWaitingforUserInput)
            {
            InputScreen.instance.Accept();
            print("You're dom" + InputScreen.currentInput);
        }
            
    }
}
