namespace ByteQuest.Shell;

public static class Shell
{
	public static string GetLine()
	{
		Console.Write("-> ");
		var line = Console.ReadLine();
		while (line == null)
		{
			Console.Write("-> ");
			line = Console.ReadLine();
		}

		return line;
	}
}