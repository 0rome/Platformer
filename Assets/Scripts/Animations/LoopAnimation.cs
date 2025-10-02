using UnityEngine;
using DG.Tweening;
using System;
using System.Collections.Generic;
using NUnit.Framework;

public class LoopAnimation : MonoBehaviour
{
    [SerializeField] private List<AnimParams> animParams = new List<AnimParams>();
    [SerializeField] private float duration = 1f; // время одного шага
    [SerializeField] private int loopCount = -1; // -1 = бесконечно
    [SerializeField] private LoopType loopType = LoopType.Yoyo; // тип зацикливания

    void Start()
    {
        Sequence seq = DOTween.Sequence();

        for (int i = 0; i < animParams.Count; i++)
        {
            // Двигаем относительно текущих координат
            seq.Append(transform.DOMove(animParams[i].position, duration)
                .SetEase(Ease.Linear)
                .SetRelative(true));

            // Вращаем относительно текущего угла
            seq.Join(transform.DORotate(animParams[i].rotation, duration)
                .SetEase(Ease.Linear)
                .SetRelative(true));

            // Масштабируем относительно текущего размера
            seq.Join(transform.DOScale(animParams[i].size, duration)
                .SetEase(Ease.Linear)
                .SetRelative(true));
        }

        // Зацикливаем
        seq.SetLoops(loopCount, loopType);
    }
}
