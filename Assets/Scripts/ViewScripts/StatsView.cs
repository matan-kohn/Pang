using TMPro;

public class StatsView : GameElement
{
    public TMP_Text LevelNumber;
    public TMP_Text PlayerScore;
    public TMP_Text PlayerLives;
    
    public void UpdateScore()
    {
        var playerScore = Game.GameModel.PlayerModel.Score;
        PlayerScore.text = $"Score {playerScore}";
    }

    public void UpdateLives()
    {
        var playerLives = Game.GameModel.PlayerModel.Lives;
        PlayerLives.text = $"Lives {playerLives}";
    }

    public void UpdateLevel()
    {
        var levelNumber = Game.GameModel.UIModel.Level;
        LevelNumber.text = $"Level {levelNumber}";
    }
}
