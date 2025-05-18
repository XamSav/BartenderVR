using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public void ChangeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
