using System;
using System.Collections;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverView : GameElement
{

    [SerializeField] private TMP_Text _message;

    IEnumerator Start()
    {
        _message.text = $"GAME OVER\n YOUR SCORE: {Game.GameModel.PlayerModel.Score}";
        yield return FadeIn();
        RestartGame();
    }

    /**
     * <summary>creates a fade-in effect for the game-over screen</summary>
     */
    private IEnumerator FadeIn()
    {
        var canvasGroup = GetComponent<CanvasGroup>();

        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(5);
    }

    /**
     * <summary>Restarts the game and returns to the menu</summary>
     */
    private void RestartGame()
    {
        Destroy(GameObject.FindWithTag(PangTags.ProjectContext));
        SceneManager.LoadScene("Menu");
    }
}
