using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DummyEnemy : MonoBehaviour, IDamageable<float, DamageMessage, int>
{
    [SerializeField] private TextMeshPro damageText;

    void SetTextAlpha(float alpha)
    {
        Color dmgColor = damageText.color;
        damageText.color = new Color(dmgColor.r, dmgColor.g, dmgColor.b, alpha);
    }

    private void Awake()
    {
        SetTextAlpha(0);
    }

    #region IDamageable Implementation
    public DamageMessage Damage(float damage, int faction)
    {
        damageText.text = damage.ToString(CultureInfo.InvariantCulture);
        Vector3 localEuler = damageText.transform.localEulerAngles;
        damageText.transform.localEulerAngles = new Vector3(localEuler.x, localEuler.y, Random.Range(-45f, 45f));
        SetTextAlpha(1.0f);
        LeanTween.value( 1.0f, 0.0f, 0.2f).setOnUpdate(SetTextAlpha);
        return DamageMessage.Damage;
    }

    public DamageMessage Die()
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
