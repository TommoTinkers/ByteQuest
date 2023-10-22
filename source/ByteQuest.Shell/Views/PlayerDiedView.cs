using ByteQuest.Core.Modes;
using ByteQuest.Core.State;

namespace ByteQuest.Shell.Views;

public static class PlayerDiedView
{
	public static (GameState, Mode) View(GameState state)
	{
		Console.WriteLine("Game Over.");
		return (state, new ExitGameMode());
	}
}