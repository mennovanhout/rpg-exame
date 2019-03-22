﻿using System.Collections;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class AnimateTitle : MonoBehaviour
{
    RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();

        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        rect.DOShakeScale(100, .1f, 2);

        yield return new WaitForEndOfFrame();
    }
}
