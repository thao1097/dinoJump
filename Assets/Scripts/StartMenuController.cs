using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("DinoGame");
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}
