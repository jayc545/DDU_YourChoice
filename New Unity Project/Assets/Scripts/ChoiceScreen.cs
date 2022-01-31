using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChoiceScreen : MonoBehaviour
{
    public static ChoiceScreen instance;

    public GameObject root;

    //TODO Tittle header.

    public ChoiveButton choicePrefab;

    static List<ChoiveButton> choices = new List<ChoiveButton>();

    public VerticalLayoutGroup layoutGroup;


    private void Awake()
    {
        instance = this;
    }
    public static void Show(string title, params string[] choices)
    {
        instance.root.SetActive(true);

        if (isShowingChoices)
            instance.StopCoroutine(showingChoices);

        ClearAllCurrentChoices();
        showingChoices = instance.StartCoroutine(ShowingChoices(choices));
    }

    public static void Hide()
    {
        if (isShowingChoices)
            instance.StopCoroutine(showingChoices);
        showingChoices = null;

        ClearAllCurrentChoices();
        instance.root.SetActive(false);
    }

    static void ClearAllCurrentChoices()
    {
        foreach(ChoiveButton b in choices)
        {
            DestroyImmediate(b.gameObject);
        }
        choices.Clear();
    }
    public static bool isWaitingForChoicesToBeMade { get { return isShowingChoices && lastChoiceMade.hasBeenMade; } }
    public static bool isShowingChoices { get { return showingChoices != null; } }
    static Coroutine showingChoices = null;
    public static IEnumerator ShowingChoices(string[] choices)
    {
        yield return new WaitForEndOfFrame();
        lastChoiceMade.Reset();

        for (int i = 0; i < choices.Length; i++)
        {
            CreateChoices(choices[i]);
        }

        SetLayoutSpacing();

        while (isWaitingForChoicesToBeMade)
            yield return new WaitForEndOfFrame();

        Hide();
    }
    static void SetLayoutSpacing()
    {
        int i = choices.Count;
        if (i <= 3)
            instance.layoutGroup.spacing = 20;
        else if (i <= 7)
            instance.layoutGroup.spacing = 1;
    }

    static void CreateChoices(string choice)
    {
        GameObject ob = Instantiate(instance.choicePrefab.gameObject, instance.choicePrefab.transform.parent);
        ob.SetActive(true);
        ChoiveButton b = ob.GetComponent<ChoiveButton>();

        b.text = choice;
        b.choiceIndex = choices.Count;

        choices.Add(b);
    }

    [System.Serializable]
    public class CHOICE
    {
        public bool hasBeenMade { get { return tittle != "" & index != -1; } }

        public string tittle = "";
        public int index = -1;

        public void Reset()
        {
            tittle = "";
            index = -1;
        }
    }
    public CHOICE choice = new CHOICE();
    public static CHOICE lastChoiceMade { get { return instance.choice; } }

    public void MakeChoice (ChoiveButton button)
    {
        choice.index = button.choiceIndex;
        choice.tittle = button.text;
    }

}
