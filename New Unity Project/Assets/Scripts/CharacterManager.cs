using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Responsible for adding and maintaining character in the scenes.
public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;

    // All characters must be attached to the character panel.
    public RectTransform characterPanel;


    //A list of all characters currently in our scene.
    public List<Character> characters = new List<Character>();

    // Lookup for our Character.
    public Dictionary<string, int> characterDictionary = new Dictionary<string, int>();



    void Awake()
    {
        instance = this;   
    }

    // try to get a character by name provided from the character List.
    public Character GetCharacter(string characterName, bool createCharacterIfDoesNotExist = true, bool enableCreatedCharacterOnStart = true)
    {
        int index = -1;
        if (characterDictionary.TryGetValue (characterName, out index))
        {
            return characters [index];
        }
        else if (createCharacterIfDoesNotExist)
        {
            return CreateCharacter(characterName, enableCreatedCharacterOnStart);
        }
        return null;
    }

    public Character CreateCharacter(string characterName, bool enableOnStart = true)
    {
        Character newCharacter = new Character(characterName, enableOnStart);

        characterDictionary.Add(characterName, characters.Count);
        characters.Add(newCharacter);

        return newCharacter;
    }

    
    public class CHARACTERPOSITIONS
    {
        public Vector2 bottomLeft = new Vector2(0, 0);
        public Vector2 tipRight = new Vector2(1f, 1f);
        public Vector2 center = new Vector2(0.5f, 0.5f);
        public Vector2 bottemRight = new Vector2(1f, 0);
        public Vector2 topLeft = new Vector2(0, 1f);
    }

    public static CHARACTERPOSITIONS characterpositions = new CHARACTERPOSITIONS();
    
    public class CHARACTEREXPRESSIONS
    {
        public int normal = 3;
        public int surprised = 3;
        public int angry = 3;
        public int flustered = 3;
    }

    public static CHARACTEREXPRESSIONS characterexpressions = new CHARACTEREXPRESSIONS();
}
