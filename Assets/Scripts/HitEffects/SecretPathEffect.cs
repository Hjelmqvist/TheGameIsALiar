using System.Collections;
using UnityEngine;

public class SecretPathEffect : HitEffect
{
    [SerializeField, Range(0, 1)] float fadedAlpha = 0.5f;
    [SerializeField] float fadeOutSpeed = 1;
    [SerializeField] float fadeInSpeed = 1;
    Renderer rend = null;

    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    protected override void EnterEffect(GameObject go)
    {
        StopAllCoroutines();
        StartCoroutine(EnterCoroutine());
    }

    protected override void ExitEffect(GameObject go)
    {
        StopAllCoroutines();
        StartCoroutine(ExitCoroutine());
    }

    IEnumerator EnterCoroutine()
    {
        Color c = rend.material.GetColor("_BaseColor");
        while (c.a > fadedAlpha)
        {
            c.a -= fadeOutSpeed * Time.deltaTime;
            rend.material.SetColor("_BaseColor", c);
            yield return null;
        }
    }

    IEnumerator ExitCoroutine()
    {
        Color c = rend.material.GetColor("_BaseColor");
        while (c.a < 1)
        {
            c.a += fadeInSpeed * Time.deltaTime;
            rend.material.SetColor("_BaseColor", c);
            yield return null;
        }
    }
}
