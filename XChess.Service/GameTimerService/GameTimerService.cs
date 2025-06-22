using System;
using System.Collections.Concurrent;
using XChess.Service.GameTimer;
using XChess.Service.GameTimerService;
using XChess.Model.GameModel;
public class GameTimerService : IGameTimerService
{
    private readonly ConcurrentDictionary<string, IGameTimerComponents> _timers = new ConcurrentDictionary<string, IGameTimerComponents>();

    public void CreateGame(string matchId, TimeSpan initialTime)
    {
        _timers[matchId] = new GameTimerComponents(initialTime);
    }

    public void SwitchTurn(string matchId)
    {
        if (_timers.TryGetValue(matchId, out var timer))
            timer.SwitchTurn();
    }

    public void Pause(string matchId)
    {
        if (_timers.TryGetValue(matchId, out var timer))
            timer.Pause();
    }

    public void Resume(string matchId)
    {
        if (_timers.TryGetValue(matchId, out var timer))
            timer.Resume();
    }

    public TimeSpan GetTimeLeft(string matchId, PlayerColor color)
    {
        return _timers.TryGetValue(matchId, out var timer)
            ? timer.GetTimeLeft(color)
            : TimeSpan.Zero;
    }

    public GameTimerState GetState(string matchId)
    {
        return _timers.TryGetValue(matchId, out var timer) ? timer.State : null;
    }
}