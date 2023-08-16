using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverView : GameElement
{

    [SerializeField] private TMP_Text _message;

    void Start()
    {
        StartCoroutine(FadeIn());
        _message.text = $"GAME OVER\n YOUR SCORE: {Game.GameModel.PlayerModel.Score}";
    }

    private IEnumerator FadeIn()
    {
        var canvasGroup = GetComponent<CanvasGroup>();

        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(5);
        
        Destroy(GameObject.FindWithTag(PangTags.ProjectContext));
        SceneManager.LoadScene("Menu");
    }
}
