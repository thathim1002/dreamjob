using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class DragableIcon : EventTrigger
{
    //Hold this letter data A-Z
    public string letter;

    //original position for return
    public Vector3 originalPosition;

    private PointerEventData _lastPointerData;

    public bool canDrag;

    public Sprite correctSprite;

    void Start()
    {
        canDrag = true;

        //if wrong letter -> set position original
        originalPosition = this.transform.position;

        //check from name Letter_"A" position = 7 
        letter = gameObject.name.Substring(7);

       
    }

    //change image if correct answer 
    public void setSpriteCorrect()
    {
        GetComponent<Image>().sprite = correctSprite;
        //Debug.LogError(gameObject.name.Substring(7));
    }

    IEnumerator waitCanDragAgain()
    {
        yield return new WaitForSeconds(0.1f);
        canDrag = true;
    }

    public void returnToOriginal()
    {
        canDrag = false;
        this.transform.position = originalPosition;
        _lastPointerData.pointerDrag = null;
        StartCoroutine(waitCanDragAgain());
    }

    //Call when player drag this icon
    //Update this position to mouse position

    public override void OnDrag(PointerEventData eventData)
    {
        //can select again
        _lastPointerData = eventData;

        //move word
        if(canDrag == true)
        {
            this.transform.position = eventData.position;
        }

        //base.OnDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        if (canDrag)
        {
            returnToOriginal();
        }
    }

    //public override void OnEndDrag(PointerEventData eventData)
    //{
    //    if (canDrag)
    //    {
    //        returnToOriginal();
    //    }
    //    base.OnEndDrag(eventData);
    //}
}
