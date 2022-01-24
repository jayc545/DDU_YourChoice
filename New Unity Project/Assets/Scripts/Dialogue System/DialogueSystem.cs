using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;

    public ELEMENTS elements;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    //Saying somethin from the speaker box.
    public void Say(string speech, string speaker = "")
    {
        StopSpeaking();
        speaking = StartCoroutine(Speaking(speech, speaker));
    }


    public void StopSpeaking()
    {
        if (isSpeaking)
        {
            StopCoroutine(speaking);
        }
        speaking = null;
    }
    public bool isSpeaking { get { return speaking != null; } }
    [HideInInspector] public bool isWaitingForUserInput = false;

    Coroutine speaking = null;

    IEnumerator Speaking(string targetSpeech, string speaker = "")
    {
        speechPanal.SetActive(true);
        speechText.text = "";
        SpeakerNameText.text = DetermineSpeaker(speaker);
        isWaitingForUserInput = false;

        while(speechText.text != targetSpeech)
        {
            speechText.text += targetSpeech[speechText.text.Length];
            yield return new WaitForEndOfFrame();
        }
        //Text Finished. 
        isWaitingForUserInput = true;
        while (isWaitingForUserInput)
            yield return new WaitForEndOfFrame();
       
        StopSpeaking();
    }

    string DetermineSpeaker(string s)
    {
        string retVal = SpeakerNameText.text;
        if (s != SpeakerNameText.text && s !="")
            retVal = (s.ToLower().Contains("narrator")) ? "narrator" : s;

        return retVal;
    }


    [System.Serializable]
    public class ELEMENTS
    {
        // Main panel for all the dialogue.
        public GameObject speechPanal;
        public Text speakerNameText;
        public Text speechText;
    }
    public GameObject speechPanal { get { return elements.speechPanal; } }
    public Text SpeakerNameText { get { return elements.speakerNameText; } }
    public Text speechText { get { return elements.speechText; } }
}
