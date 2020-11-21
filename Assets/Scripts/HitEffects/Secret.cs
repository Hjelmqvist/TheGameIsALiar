using UnityEngine;

public class Secret : HitEffect
{
    public static int SecretsFound { get { return secretsFound; } }
    static int secretsFound = 0;

    public static int AmountOfSecrets { get { return amountOfSecrets; } }
    static int amountOfSecrets = 0;

    bool pickedUp = false;

    Collider2D col = null;
    Renderer rend = null;

    void Awake()
    {
        col = GetComponent<Collider2D>();
        rend = GetComponent<Renderer>();
        amountOfSecrets++;
    }

    void OnEnable()
    {
        GameManager.OnRestart += GameManager_OnRestart;
    }

    void OnDisable()
    {
        GameManager.OnRestart -= GameManager_OnRestart;
    }

    private void GameManager_OnRestart()
    {
        amountOfSecrets--;
        if (pickedUp)
            secretsFound--;
    }

    public static void ResetSecretsFound()
    {
        secretsFound = 0;
    }

    protected override void EnterEffect(GameObject go)
    {
        if (pickedUp)
            return;

        Player player = go.GetComponent<Player>();
        if (player)
        {
            secretsFound++;
            pickedUp = true;
            col.enabled = false;
            rend.enabled = false;
        }
    }

    protected override void ExitEffect(GameObject go)
    {
        throw new System.NotImplementedException();
    }
}
