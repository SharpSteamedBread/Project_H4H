using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeechBubbleResizer : MonoBehaviour
{
    [SerializeField] private RectTransform speechBubble;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float minHeight = 100f;
    [SerializeField] private float padding;

    private void Awake()
    {
        minHeight = 100f;
        padding = 20f;
    }

    private void Update()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(speechBubble);

        float textHeight = text.preferredHeight;

        Vector2 newSize = speechBubble.sizeDelta;
        newSize.y = Mathf.Max(minHeight, textHeight + padding * 2);
        speechBubble.sizeDelta = newSize;
    }
}
