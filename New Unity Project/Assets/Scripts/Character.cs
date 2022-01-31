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

    DialogueSystem dialogue;

    public void Say(string speech)
    {
        dialogue.Say(speech, characterName);
    }

    //Create a new character
    public Character (string _name)
    {
        CharacterManager cm = CharacterManager.instance;
        // Locating the prefab.
        GameObject prefab = Resources.Load("Characters/Character[" + _name+"]") as GameObject;
        //Spawning the instance.
        GameObject ob = GameObject.Instantiate(prefab, cm.characterPanel);

        root = ob.GetComponent<RectTransform>();
        characterName = _name;


        dialogue = DialogueSystem.instance;
    }

    
    class Renderers
    {
               //Sprites used images.
        public Image bodyRenderer1;
        public Image bodyRenderer2;
        public Image Head;
    }
}
