using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTesting : MonoBehaviour
{
    public Character newOC;

    // Start is called before the first frame update
    void Start()
    {
        newOC = CharacterManager.instance.GetCharacter ("girl", enableCreatedCharacterOnStart: false);

    }

    public string[] speech;
    int i = 0;

    public Vector2 moveTarget;
    public float moveSpeed;
    public bool smooth;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (i < speech.Length)
                newOC.Say(speech[i]);
            else
                DialogueSystem.instance.Close();

            i++;
        }

        if (Input.GetKey(KeyCode.M))
        {
            newOC.MoveTo(moveTarget, moveSpeed, smooth);
        }
    }
}
