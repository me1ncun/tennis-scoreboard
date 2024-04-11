namespace tennis.Score.Score_system;

public class Point
{
    public int amount { get; set; } = 0;
    private int number;

    public void IncreaseCounter(int number)
    {
        amount += number;
    }
    public void IncreaseCounter()
    {
        amount++;
    }
}