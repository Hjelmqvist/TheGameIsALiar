using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelEffect : HitEffect
{
    protected override void EnterEffect(GameObject go)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    protected override void ExitEffect(GameObject go)
    {
        throw new System.NotImplementedException();
    }
}
