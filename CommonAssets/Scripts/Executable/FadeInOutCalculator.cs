using System;
using UnityEngine;
using static FadeInOutCalculator;

/// <summary>
/// フェードイン・フェードアウトによる値の変化を計算します。
/// 使い方は Start メソッドでフェードする値と時間を設定して
/// CalcNext メソッドでフェードを進めます。
/// </summary>
public class FadeInOutCalculator {
    public bool IsFading => fadeStatus == FadeStatus.Fading;

    private float startValue;
    private float endValue;
    private float fadeDuration;
    private float fadeTime;
    private FadeStatus fadeStatus = FadeStatus.NotStarted;
    
    private enum FadeStatus{NotStarted, Fading, Complete}

    /// <summary>
    /// フェードイン・アウトを開始します。
    /// </summary>
    public void Start(float startVal, float endVal, float fadeDurationArg = 2f) {
        this.startValue = startVal;
        this.endValue = endVal;
        this.fadeDuration = fadeDurationArg;
        fadeStatus = FadeStatus.Fading;
    }

    /// <summary>
    /// フェードイン・アウトを進めて、次の値を返します。
    /// </summary>
    public float CalcNext(float deltaTime) {
        switch (fadeStatus) {
            case FadeStatus.NotStarted:
                return -1;
            case FadeStatus.Fading:
                fadeTime += deltaTime;
                float t = fadeTime / this.fadeDuration;
                if (t >= 1f) {
                    fadeStatus = FadeStatus.Complete;
                    return endValue;
                }
                return Mathf.Lerp(startValue, endValue, t);
            case FadeStatus.Complete:
                return endValue;
            default:
                throw new Exception();
        }
    }
}