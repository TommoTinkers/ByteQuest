using System.Collections.Immutable;
using ByteQuest.CoreLogic.Data;
using ByteQuest.CoreLogic.Events;
using static ByteQuest.CoreLogic.State.GameStateInititializationFailureReason;

namespace ByteQuest.CoreLogic.State;

public abstract record GameStateInitializationResult;

public sealed record GameStateInitialized(GameState State) : GameStateInitializationResult;

internal sealed record GameStateInitialiationFailed(GameStateInititializationFailureReason reasons) : GameStateInitializationResult;


internal enum GameStateInititializationFailureReason
{
	NoEventsToInitialize,
	NoGameStartedEvent
}


public static class GameStateBuilders
{
	public static GameStateInitializationResult InitializeFromEvents(ImmutableArray<Event> events)
	{
		return events switch
		{
			[] => new GameStateInitialiationFailed(NoEventsToInitialize),
			[GameStartedEvent c,] => new GameStateInitialized(ApplyEvents(new GameState(c.Player, c.Seed), events[1..])),
			_ => new GameStateInitialiationFailed(NoGameStartedEvent)
		};
	}
	
	public static GameState ApplyEvents(GameState state, ImmutableArray<Event> events)
	{
		return state;
	}
}



