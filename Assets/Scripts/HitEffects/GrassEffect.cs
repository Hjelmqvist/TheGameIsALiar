using UnityEngine;

public class GrassEffect : HitEffect
{
    [SerializeField] ParticleSystem effect = null;

    Collider2D col = null;

    void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    protected override void EnterEffect(GameObject go)
    {
        Player player = go.GetComponent<Player>();
        if (player)
        {
            if (player.Element == Element.Fire)
            {
                effect.Stop();
                col.enabled = false;
                enabled = false;
            }
        }
    }

    protected override void ExitEffect(GameObject go)
    {
        throw new System.NotImplementedException();
    }
}
