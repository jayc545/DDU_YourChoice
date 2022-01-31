using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceScreenTest : MonoBehaviour
{

    public string title = "MAke a Choice";
    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetKeyDown(KeyCode.E))
            ChoiceScreen.Show(title, "Dogs", "Cats" );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
