using UnityEngine;
using UnityEngine.EventSystems;

public class DropPlace : MonoBehaviour, IDropHandler
{
    [SerializeField] private float cardScale = 1f;
    
    public void OnDrop(PointerEventData eventData)
    {
        var card =  eventData.pointerDrag.GetComponent<Card>();

        if (card)
        {
             card.DefaultParent = transform;
             
             card.transform.localScale = new Vector3(cardScale, cardScale, cardScale);
        }
    }
}
