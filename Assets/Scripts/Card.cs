using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]
public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Canvas _canvas;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    private Canvas _cardCanvas;

    private int indexToDrop;
    
    [HideInInspector] public Transform DefaultParent;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _cardCanvas = GetComponent<Canvas>();
        _canvas = GetComponentInParent<Canvas>().rootCanvas;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DefaultParent = transform.parent;
        transform.SetParent(DefaultParent.parent);
        
        _canvasGroup.blocksRaycasts = false;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        CheckPosition();
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(DefaultParent);
        transform.SetSiblingIndex(indexToDrop);
        _canvasGroup.blocksRaycasts = true;
    }

    private void CheckPosition()
    {
        var fields = DefaultParent.parent.GetComponentsInChildren<DropPlace>();
        var fieldToDrop = Vector3.Distance(fields[0].transform.position, transform.position) <
                          Vector3.Distance(fields[1].transform.position, transform.position) ? fields[0].gameObject : fields[1].gameObject;

        foreach (var card in fieldToDrop.GetComponentsInChildren<Card>().Reverse())
        {
            if (card.transform.position.x < transform.position.x)
            {
                indexToDrop = card.transform.GetSiblingIndex() + 1;
                return;
            }
        }
        
        indexToDrop = 0;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _cardCanvas.overrideSorting = true;
        _cardCanvas.sortingOrder = 1;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _cardCanvas.sortingOrder = 0;
        _cardCanvas.overrideSorting = false;
    }
}