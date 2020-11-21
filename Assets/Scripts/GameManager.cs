using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] float restartTime = 2;

    Coroutine restart = null;

    public delegate void Restart();
    public static event Restart OnRestart;

    void Update()
    {
        if (Input.GetButtonDown("Restart"))
            RestartCurrent();
    }

    public void RestartLevel()
    {
        if (restart == null)
            restart = StartCoroutine(RestartCoroutine(restartTime));
    }

    IEnumerator RestartCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        RestartCurrent();
    }

    void RestartCurrent()
    {
        OnRestart?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
