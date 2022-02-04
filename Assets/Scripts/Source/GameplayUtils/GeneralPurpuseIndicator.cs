using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class GeneralPurpuseIndicator : MonoBehaviour
{
    private TextMeshPro text;

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }

    public void ShowIndicator(string text)
    {
        this.text.text = text;
    }
}
