using DefaultNamespace;

public class PlayerModel : GameElement
{
    public PlayerState PlayerState { get; set; }
    public int Lives { get; set; }
    public int Score { get; set; }

    private void Awake()
    {
        PlayerState = PlayerState.Idle;
        Lives = 3;
        Score = 0;
    }
}
