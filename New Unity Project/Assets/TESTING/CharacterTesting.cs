using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTesting : MonoBehaviour
{
    public Character girl;

    // Start is called before the first frame update
    void Start()
    {
        girl = CharacterManager.instance.GetCharacter ("girl", enableCreatedCharacterOnStart: true);
        girl.GetSprite(0);
    }

    public string[] speech;
    int i = 0;

    public Vector2 moveTarget;
    public float moveSpeed;
    public bool smooth;
    // Expression
    public int  expressionIndex, bodyIndex = 0;

    public float speed = 5f;
    public bool smoothtransitions = false;



    // Update is called once per frame
    void Update()
    {

        // Her saying something
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (i < speech.Length)
                girl.Say(speech[i]);
            else
                DialogueSystem.instance.Close();
            i++;
        }




        // Moving test
        if (Input.GetKey(KeyCode.M))
        {
            girl.MoveTo(moveTarget, moveSpeed, smooth);
        }

        // Imitiate movement
        if (Input.GetKeyDown(KeyCode.S))
        {
            girl.StopMoving(true);
        }

        //Expression and such.
        // Moving test
        if (Input.GetKey(KeyCode.B))
        {
            if (Input.GetKey(KeyCode.T))
                girl.TransitionBody(girl.GetSprite(bodyIndex), speed, smoothtransitions);
            else
                girl.SetBody(bodyIndex);
        }

        // Imitiate movement
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Input.GetKey(KeyCode.T))
                girl.TransitionExpression(girl.GetSprite(expressionIndex), speed, smoothtransitions);
            else
                girl.SetExpression(expressionIndex);
        }




    }
}
