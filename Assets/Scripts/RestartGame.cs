using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class RestartGame : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

}
