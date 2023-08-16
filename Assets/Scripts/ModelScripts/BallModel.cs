using DefaultNamespace;

public class BallModel
{
    public int Id { get; }
    public BallSize BallSize { get; }

    public BallModel(int id, BallSize ballSize)
    {
        Id = id;
        BallSize = ballSize;
    }
}
