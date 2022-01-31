using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character
{
    public string characterName;
    // Root container for all the images related to the character in the scenne.
    [HideInInspector] public RectTransform root;

    public bool enabled { get { return root.gameObject.activeInHierarchy; } set { root.gameObject.SetActive(value); } }

    // the space between the anchors.
    public Vector2 anchorPadding { get { return root.anchorMax - root.anchorMin; } }

    DialogueSystem dialogue;


    //Make this character say something.
    public void Say(string speech, bool add = false)
    {
        //The Dialogue can only function if the there's a character.
        if (!enabled)
            enabled = true;

        if (!add)
            dialogue.Say(speech, characterName);
        else
            dialogue.SayAdd(speech, characterName);
    }


    /// <summary>
    /// FOR MOVING THE CHARACTERS
    /// </summary>
    Vector2 targetPosition;
    Coroutine moving;
    bool isMoving { get { return moving != null; } }
    public void MoveTo(Vector2 Target, float speed, bool smooth = true)
    {
        // if we are moving, stop moving
        StopMoving();
        // start moving coroutine.
        moving = CharacterManager.instance.StartCoroutine(Moving(Target, speed, smooth));
    }

    public void StopMoving()
    {
        if (isMoving)
        {
            CharacterManager.instance.StopCoroutine (moving);
        }
        moving = null;
    }

    IEnumerator Moving(Vector2 target, float speed, bool smooth)
    {
        targetPosition = target;

        Vector2 padding = anchorPadding;

        float maxX = 1f - padding.x;
        float maxY = 1f - padding.y;

        Vector2 minAnchorTarget = new Vector2(maxX * targetPosition.x, maxY * targetPosition.y);
        speed *= Time.deltaTime;

        while(root.anchorMin != minAnchorTarget)
        {
            root.anchorMin = (!smooth) ? Vector2.MoveTowards(root.anchorMin, minAnchorTarget, speed) : Vector2.Lerp(root.anchorMin, minAnchorTarget, speed);
            root.anchorMax = root.anchorMin + padding;
            yield return new WaitForEndOfFrame();
        }

        StopMoving();
    }

    //Create a new character
    public Character (string _name, bool enableOnStart = true)
    {
        CharacterManager cm = CharacterManager.instance;
        // Locating the prefab.
        GameObject prefab = Resources.Load("Characters/Character[" + _name+"]") as GameObject;
        //Spawning the instance.
        GameObject ob = GameObject.Instantiate(prefab, cm.characterPanel);

        root = ob.GetComponent<RectTransform>();
        characterName = _name;


        dialogue = DialogueSystem.instance;

        enabled = enableOnStart;
    }

    
    class Renderers
    {
               //Sprites used images.
        public Image bodyRenderer1;
        public Image bodyRenderer2;
        public Image Head;
    }
}
