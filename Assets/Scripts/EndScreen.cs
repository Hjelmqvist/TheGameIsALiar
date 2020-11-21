using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI secretText = null;
    [SerializeField] TextMeshProUGUI foundAllText = null;
    [SerializeField] TextMeshProUGUI didntFindAllText = null;

    void Awake()
    {
        int secretsFound = Secret.SecretsFound;
        int totalSecrets = Secret.AmountOfSecrets;
        Secret.ResetSecretsFound();
        secretText.text = string.Format("Secrets found: {0} / {1}", secretsFound, totalSecrets);
        if (secretsFound == totalSecrets)
            foundAllText.gameObject.SetActive(true);
        else
            didntFindAllText.gameObject.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
