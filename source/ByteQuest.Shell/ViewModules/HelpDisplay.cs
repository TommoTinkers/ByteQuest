namespace ByteQuest.Shell.ViewModules;

public static class HelpDisplay
{
	public static void DisplayHelp(IEnumerable<(string, string)> entries)
	{
		Console.WriteLine("Available Commands");
		Console.WriteLine("| Command |    Description    |");
		Console.WriteLine("|---------|-------------------|");
		foreach (var entry in entries)
		{
			Console.WriteLine($"| {entry.Item1}  | { entry.Item2 } |");
		}
		
		Console.WriteLine();
		Console.WriteLine();
	}
}