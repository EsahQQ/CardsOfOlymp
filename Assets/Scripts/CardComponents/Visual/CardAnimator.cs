using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using BattleComponents;

namespace CardComponents.Visual
{
    public class CardAnimator : MonoBehaviour
    {
        public static CardAnimator Instance;
        [Header("Настройки анимации сброса")]
        [SerializeField] private Transform dropPileTarget;
        [SerializeField] private float moveDuration = 0.7f;
        [SerializeField] private float arcHeight = 2.0f;
        [SerializeField] private Transform flyingInDropCards;
        
        [SerializeField] private float rotationAngle = 30f;
        [SerializeField] private Ease moveEase = Ease.InOutQuad; 

        [Header("Настройки анимации взятия карты")]
        [SerializeField] private Transform deckTarget; 
        [SerializeField] private float drawMoveDuration = 0.6f;
        [SerializeField] private float drawArcHeight = 1.5f;
        [SerializeField] private Ease drawMoveEase = Ease.OutQuad;
        [SerializeField] private Transform flyingFromDeckCards;

        [SerializeField] private GameObject placeholderPrefab;
        
        private LogicalDrop _drop;

        private void Awake()
        {
            Instance = this;
        }

        public void Init(LogicalDrop drop)
        {
            _drop = drop;
            _drop.OnCardAddToDrop += OnCardAddToDrop;
        }

        private void OnCardAddToDrop(object sender, Card card)
        {
            AnimateCardToDropPile(card);
        }

        private void AnimateCardToDropPile(Card cardToAnimate)
        {
            cardToAnimate.transform.SetParent(flyingInDropCards.transform, true);
            
            
            Vector3[] path = new Vector3[2];
            path[0] = (cardToAnimate.transform.position + dropPileTarget.position) / 2 + Vector3.up * arcHeight; // Контрольная точка
            path[1] = dropPileTarget.position; 

            cardToAnimate.transform.DOPath(path, moveDuration, PathType.CatmullRom)
                .SetEase(moveEase);
            
            cardToAnimate.transform.DORotate(dropPileTarget.eulerAngles + new Vector3(0, 0, rotationAngle), moveDuration / 2)
                .SetEase(Ease.InQuad) 
                .OnComplete(() => {
                    cardToAnimate.transform.DORotate(dropPileTarget.eulerAngles, moveDuration / 2)
                        .SetEase(Ease.OutQuad);
                });
            
            DOVirtual.DelayedCall(moveDuration, () => {
                Destroy(cardToAnimate.gameObject);
            });
        }
        
        public void AnimateCardToHand(GameObject cardObject, Transform handContainer)
        {
            
            cardObject.transform.position = deckTarget.position;
            cardObject.transform.rotation = deckTarget.rotation;
            cardObject.transform.localScale = deckTarget.localScale;
            
            cardObject.transform.SetParent(flyingFromDeckCards);
            cardObject.SetActive(true);
            
            var placeholder = Instantiate(placeholderPrefab, handContainer);
            placeholder.SetActive(true);
            
            StartCoroutine(MoveToPlaceholderCoroutine(cardObject.transform, placeholder.transform));
        }

        private IEnumerator MoveToPlaceholderCoroutine(Transform cardToAnimate, Transform placeholder)
        {
            yield return null; // ждем 1 кадр, чтоб layoutgroup обновил позицию placeholder`а
            
            /*Vector3[] path = new Vector3[2];
            path[0] = (cardToAnimate.transform.position + deckTarget.position) / 2 + Vector3.up * arcHeight; // Контрольная точка
            path[1] = placeholder.position; 

            cardToAnimate.transform.DOPath(path, moveDuration, PathType.CatmullRom)
                .SetEase(moveEase);*/
            
            cardToAnimate.DOMove(placeholder.position, drawMoveDuration).SetEase(drawMoveEase);
            cardToAnimate.DORotateQuaternion(placeholder.rotation, drawMoveDuration).SetEase(drawMoveEase);
            
            yield return new WaitForSeconds(drawMoveDuration);
 
            cardToAnimate.SetParent(placeholder.parent);
            cardToAnimate.SetSiblingIndex(placeholder.GetSiblingIndex());
            
            Destroy(placeholder.gameObject);
        }
    }
}