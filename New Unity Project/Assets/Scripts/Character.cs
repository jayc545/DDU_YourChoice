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

    public bool isMultiLayerCharacter { get { return renderers.renderer == null; } }

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

    /// <summary>
    /// Move to a specific point relative to the canvas space. (1,1) = far top right.
    /// </summary>
    /// <param name="Target"></param>
    /// <param name="speed"></param>
    /// <param name="smooth"></param>
    public void MoveTo(Vector2 Target, float speed, bool smooth = true)
    {
        // if we are moving, stop moving
        StopMoving();
        // start moving coroutine.
        moving = CharacterManager.instance.StartCoroutine(Moving(Target, speed, smooth));
    }



    /// <summary>
    /// Stops the character in the it's trach, either setting it immediatly at the target position or not.
    /// </summary>
    /// <param name="arriveAtTargetPositionImmediatly"></param>
    public void StopMoving(bool arriveAtTargetPositionImmediatly = false)
    {
        if (isMoving)
        {
            CharacterManager.instance.StopCoroutine (moving);
            if (arriveAtTargetPositionImmediatly)
                SetPosition(targetPosition);
        }
        moving = null;
    }

/// <summary>
///  Immediatly set the position of this character to the intended target.
/// </summary>
/// <param name="target"></param>
    public void SetPosition(Vector2 target)
    {
        Vector2 padding = anchorPadding;
        float maxX = 1f - padding.x;
        float maxY = 1f - padding.y;
        Vector2 minAnchorTarget = new Vector2(maxX * targetPosition.x, maxY * targetPosition.y);
        root.anchorMin = minAnchorTarget;
        root.anchorMax = root.anchorMin + padding;

    }

    /// <summary>
    /// Moves gradually the character towards a position using coroutine.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="speed"></param>
    /// <param name="smooth"></param>
    /// <returns></returns>

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
            root.anchorMin = (!smooth) ? Vector2.MoveTowards (root.anchorMin, minAnchorTarget, speed) : Vector2.Lerp (root.anchorMin, minAnchorTarget, speed);
            root.anchorMax = root.anchorMin + padding;
            yield return new WaitForEndOfFrame();
        }

        StopMoving();
    }

    // Begin Transitioning Images //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public Sprite GetSprite(int index = 0)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Images/Characters/" + characterName);
        return sprites[index];
    }

    public void SetBody (int index)
    {
        renderers.bodyRenderer.sprite = GetSprite(index);
    }

    public void SetBody (Sprite sprite)
    {
        renderers.bodyRenderer.sprite = sprite;
    }

    public void SetExpression (int index)
    {
        renderers.expressionRenderer.sprite = GetSprite(index);
    }

    public void SetExpression(Sprite sprite)
    {
        renderers.expressionRenderer.sprite = sprite;
    }

	//Transition Body
	bool isTransitioningBody {get{ return transitioningBody != null;}}
	Coroutine transitioningBody = null;

	public void TransitionBody(Sprite sprite, float speed, bool smooth)
	{
		if (renderers.bodyRenderer.sprite == sprite)
			return;
		
		StopTransitioningBody ();
		transitioningBody = CharacterManager.instance.StartCoroutine (TransitioningBody (sprite, speed, smooth));
	}

	void StopTransitioningBody()
	{
		if (isTransitioningBody)
			CharacterManager.instance.StopCoroutine (transitioningBody);
		transitioningBody = null;
	}

	public IEnumerator TransitioningBody(Sprite sprite, float speed, bool smooth)
	{
		for (int i = 0; i < renderers.allBodyRenderers.Count; i++) 
		{
			Image image = renderers.allBodyRenderers [i];
			if (image.sprite == sprite) 
			{
				renderers.bodyRenderer = image;
				break;
			}
		}

		if (renderers.bodyRenderer.sprite != sprite) 
		{
			Image image = GameObject.Instantiate (renderers.bodyRenderer.gameObject, renderers.bodyRenderer.transform.parent).GetComponent<Image> ();
			renderers.allBodyRenderers.Add (image);
			renderers.bodyRenderer = image;
			image.color = GlobalF.SetAlpha (image.color, 0f);
			image.sprite = sprite;
		}

		while (GlobalF.TransitionImages (ref renderers.bodyRenderer, ref renderers.allBodyRenderers, speed, smooth))
			yield return new WaitForEndOfFrame ();

		Debug.Log ("done");
		StopTransitioningBody ();
	}

	//Transition Expression
	bool isTransitioningExpression {get{ return transitioningExpression != null;}}
	Coroutine transitioningExpression = null;

	public void TransitionExpression(Sprite sprite, float speed, bool smooth)
	{
		if (renderers.expressionRenderer.sprite == sprite)
			return;

		StopTransitioningExpression ();
		transitioningExpression = CharacterManager.instance.StartCoroutine (TransitioningExpression (sprite, speed, smooth));
	}

	void StopTransitioningExpression()
	{
		if (isTransitioningExpression)
			CharacterManager.instance.StopCoroutine (transitioningExpression);
		transitioningExpression = null;
	}

	public IEnumerator TransitioningExpression(Sprite sprite, float speed, bool smooth)
	{
		for (int i = 0; i < renderers.allExpressionRenderers.Count; i++) 
		{
			Image image = renderers.allExpressionRenderers [i];
			if (image.sprite == sprite) 
			{
				renderers.expressionRenderer = image;
				break;
			}
		}

		if (renderers.expressionRenderer.sprite != sprite) 
		{
			Image image = GameObject.Instantiate (renderers.expressionRenderer.gameObject, renderers.expressionRenderer.transform.parent).GetComponent<Image> ();
			renderers.allExpressionRenderers.Add (image);
			renderers.expressionRenderer = image;
			image.color = GlobalF.SetAlpha (image.color, 0f);
			image.sprite = sprite;
		}

		while (GlobalF.TransitionImages (ref renderers.expressionRenderer, ref renderers.allExpressionRenderers, speed, smooth))
			yield return new WaitForEndOfFrame ();

		Debug.Log ("done");
		StopTransitioningExpression ();
	}



    // End Transitioning images ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //Create a new character
    public Character (string _name, bool enableOnStart = true)
    {
        CharacterManager cm = CharacterManager.instance;
        // Locating the prefab.
        GameObject prefab = Resources.Load("Characters/Character[" + _name+"]") as GameObject;
        //Spawning the instance.
        GameObject ob = GameObject.Instantiate(prefab, cm.characterPanel);

        root = ob.GetComponent<RectTransform> ();
        characterName = _name;


        renderers.bodyRenderer = ob.transform.Find("BodyLayer").GetComponent<Image> ();
        renderers.expressionRenderer = ob.transform.Find("ExpressionLayer").GetComponent<Image> ();
        renderers.allBodyRenderers.Add(renderers.bodyRenderer);
        renderers.allExpressionRenderers.Add(renderers.expressionRenderer);



        dialogue = DialogueSystem.instance;

        enabled = enableOnStart;

        renderers.renderer = ob.GetComponentInChildren<RawImage>();
    }

     [System.Serializable]
    public class Renderers
    {
        // Uses as the only image for a single layer character
        public RawImage renderer;

        // For the body of the multilayer character.
        public Image bodyRenderer;

        // the expression for the renderer of the multilayer character.
        public Image expressionRenderer;

        public List<Image> allBodyRenderers = new List<Image>();
        public List<Image> allExpressionRenderers = new List<Image>();
    }

    public Renderers renderers = new Renderers();
}
