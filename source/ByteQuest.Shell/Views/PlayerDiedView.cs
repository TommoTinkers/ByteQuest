using ByteQuest.Core.Modes;

namespace ByteQuest.Shell.Views;

public static class PlayerDiedView
{
	public static Mode View()
	{
		Console.WriteLine("Game Over.");
		return new ExitGameMode();
	}
}