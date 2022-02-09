using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(TextMeshPro))]
public class GeneralPurpuseIndicator : MonoBehaviour
{
    private TextMeshPro text;

    private bool visible;
    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            ShowIndicator("Def +10");
        }
    }

    void SetTextAlpha(float alpha)
    {
        Color col = text.color;
        text.color = new Color(col.r, col.g, col.b, alpha);
    }

    public void ShowIndicator(string text)
    {
        this.text.text = text;
        Vector3 localEuler = this.text.transform.localEulerAngles;
        this.text.transform.localEulerAngles = new Vector3(localEuler.x, localEuler.y, Random.Range(-45f, 45f));
        SetTextAlpha(1.0f);
        LeanTween.value( 1.0f, 0.0f, 0.2f).setOnUpdate(SetTextAlpha);
    }
}
