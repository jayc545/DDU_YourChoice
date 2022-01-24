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
    public static void Show()
    {

    }

    public static void Hide()
    {

    }
}
