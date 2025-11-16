using System;
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
    
        // --- Дополнительные настройки для красоты ---
        [SerializeField] private float rotationAngle = 30f;
        [SerializeField] private Ease moveEase = Ease.InOutQuad; // <--- Выбирайте любую!

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
    }
}