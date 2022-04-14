using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public void backToMainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
