using UnityEngine;
using UnityEngine.SceneManagement;

namespace ViewScripts
{
    public class MenuView : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene("Game");
        }

        public void OpenLeaderboard()
        {
            SceneManager.LoadScene("Leaderboard");
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
