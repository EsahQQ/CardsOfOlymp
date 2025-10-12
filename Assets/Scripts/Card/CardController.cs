using System.Linq;
using Battle;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Card
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasGroup))]
    public class CardController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler//, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject placeholderPrefab;
        
        private Canvas _rootCanvas;
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;

        private Canvas _cardCanvas;
    
        [HideInInspector] public Transform DefaultParent;
        
        private GameObject _placeholder;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _cardCanvas = GetComponent<Canvas>();
            _rootCanvas = GetComponentInParent<Canvas>().rootCanvas;
            
            if (placeholderPrefab != null)
            {
                _placeholder = Instantiate(placeholderPrefab, _rootCanvas.transform);
                _placeholder.name = "Placeholder for " + this.gameObject.name;
                
                _placeholder.SetActive(false);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (placeholderPrefab == null)
            {
                Debug.LogError("Префаб плейсхолдера не назначен в инспекторе!", this);
                return;
            }
            
            DefaultParent = transform.parent;
            
            _placeholder.transform.SetParent(DefaultParent);
            _placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
            _placeholder.SetActive(true);
            
            transform.SetParent(_rootCanvas.transform);
            _canvasGroup.blocksRaycasts = false;
        }
    
        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _rootCanvas.scaleFactor;
            
            if (_placeholder.transform.parent != DefaultParent)
            {
                _placeholder.transform.SetParent(DefaultParent);
            }

            int newSiblingIndex = DefaultParent.childCount;
            for (int i = 0; i < DefaultParent.childCount; i++)
            {
                if (this.transform.position.x < DefaultParent.GetChild(i).position.x)
                {
                    newSiblingIndex = i;
                    if (_placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                    {
                        newSiblingIndex--;
                    }
                    break;
                }
            }
            _placeholder.transform.SetSiblingIndex(newSiblingIndex);
        }
    
        public void OnEndDrag(PointerEventData eventData)
        {
            transform.SetParent(DefaultParent);
            transform.SetSiblingIndex(_placeholder.transform.GetSiblingIndex());
            
            _placeholder.SetActive(false); 
            _placeholder.transform.SetParent(_rootCanvas.transform); 
            
            _canvasGroup.blocksRaycasts = true;
        }

        /*public void OnPointerEnter(PointerEventData eventData)
        {
            _cardCanvas.overrideSorting = true;
            _cardCanvas.sortingOrder = 1;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _cardCanvas.sortingOrder = 0;
            _cardCanvas.overrideSorting = false;
        }*/
    }
}