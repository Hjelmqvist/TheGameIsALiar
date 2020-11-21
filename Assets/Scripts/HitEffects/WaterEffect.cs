using UnityEngine;

public class WaterEffect : HitEffect
{
    [SerializeField] float movementSpeedMultiplier = 0.5f;
    [SerializeField] float jumpMultiplier = 0.5f;

    [SerializeField] float linearDragInWater = 2;
    [SerializeField] float gravityMultiplier = 0.5f;


    Player player = null;

    void Update()
    {
        if (player)
        {
            if (player.Element == Element.Fire)
            {
                player.Kill();
                if (GameManager.Instance)
                    GameManager.Instance.RestartLevel();
                return;
            }
            player.ResetValues();
            player.movementSpeed *= movementSpeedMultiplier;
            player.RB.drag = linearDragInWater;
            player.RB.gravityScale *= gravityMultiplier;
        }
    }

    protected override void EnterEffect(GameObject go)
    {
        Player player = go.GetComponent<Player>();
        if (player)
        {
            this.player = player;
            player.OnJump += Player_OnJump;
        }
    }

    protected override void ExitEffect(GameObject go)
    {
        Player player = go.GetComponent<Player>();
        if (player)
        {
            this.player = null;
            player.ResetValues();
        }    
    }

    private void Player_OnJump(Player player)
    {
        Vector2 vel = player.RB.velocity;
        vel.y *= jumpMultiplier;
        player.RB.velocity = vel;
    }
}
