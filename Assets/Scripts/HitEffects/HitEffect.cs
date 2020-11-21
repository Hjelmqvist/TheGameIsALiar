using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class HitEffect : MonoBehaviour
{
    [SerializeField] bool destroyAfterHit = false;
    [SerializeField] GameObject destroyOverride = null;
    [SerializeField] bool exitEffect = false;

    void OnCollisionEnter2D(Collision2D col)
    {
        EnterEffect(col.gameObject);
        if (destroyAfterHit)
        {
            GameObject objectToDestroy = destroyOverride ? destroyOverride : gameObject;
            Destroy(objectToDestroy);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (exitEffect)
            ExitEffect(col.gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        EnterEffect(col.gameObject);
        if (destroyAfterHit)
        {
            GameObject objectToDestroy = destroyOverride ? destroyOverride : gameObject;
            Destroy(objectToDestroy);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (exitEffect)
            ExitEffect(col.gameObject);
    }

    protected abstract void EnterEffect(GameObject go);

    protected abstract void ExitEffect(GameObject go);
}
