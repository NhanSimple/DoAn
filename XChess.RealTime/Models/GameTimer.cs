using System;
public class GameTimer
{
    public TimeSpan WhiteTime { get; private set; } = TimeSpan.FromMinutes(5);
    public TimeSpan BlackTime { get; private set; } = TimeSpan.FromMinutes(5);
    private DateTime lastMoveTime;
    private bool isWhiteTurn = true;
    public void SwitchTurn()
    {
        var now = DateTime.Now;
        var elapsed = now - lastMoveTime;
        if (isWhiteTurn)
            WhiteTime -= elapsed;
        else
            BlackTime -= elapsed;
        isWhiteTurn = !isWhiteTurn;
        lastMoveTime = now;
    }
}