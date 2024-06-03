using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float unscaledTimer = 0;
    private float scaledTimer = 0;

    [SerializeField] private TextMeshPro objUnscaledTimer;
    [SerializeField] private TextMeshPro objScaledTimer;

    private void Awake()
    {
        StartCoroutine(TikTokUnscaled());
        StartCoroutine(TikTokScaled());
    }

    void Update()
    {
        objUnscaledTimer.text = $"{unscaledTimer}";
        objScaledTimer.text = $"{scaledTimer}";


    }

    private IEnumerator TikTokUnscaled()
    {
        unscaledTimer += 1f;

        yield return new WaitForSecondsRealtime(1.0f);

        StartCoroutine(TikTokUnscaled());
    }

    private IEnumerator TikTokScaled()
    {
        scaledTimer += 1f;

        yield return new WaitForSeconds(1.0f);

        StartCoroutine(TikTokScaled());
    }
}
