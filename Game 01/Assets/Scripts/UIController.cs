using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    /// <summary>
    /// Quits the game
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Restarts the game
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}