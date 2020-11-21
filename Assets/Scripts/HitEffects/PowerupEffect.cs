using UnityEngine;

public class PowerupEffect : HitEffect
{
    [SerializeField] Element element = Element.Normal;

    protected override void EnterEffect(GameObject go)
    {
        Player player = go.GetComponent<Player>();
        if (player)
            player.ChangeType(element);
    }

    protected override void ExitEffect(GameObject go)
    {
        throw new System.NotImplementedException();
    }
}
