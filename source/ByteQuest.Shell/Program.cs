using ByteQuest.Core.Data;
using ByteQuest.Core.Modes;
using ByteQuest.Shell.Views;

Console.WriteLine("Welcome to ByteQuest!");


var enemy = new Enemy (Name: "Bob The Goblin", Health: 200u, Strength: 5u, Accuracy: 5u, Evasion: 5u, Defence: 5u);

var player = new Player( Health: 2u, Strength: 5u, Accuracy: 5u, Evasion: 5u, Defence: 5u);

Mode mode = new PlayersTurn(player, enemy);

while (true)
{
	if (mode is ExitGameMode)
	{
		return;
	}
	
	mode = mode switch
	{
		BattleMode bm => BattleView.View(bm),
		PlayerDiedMode => PlayerDiedView.View(),
		_ => throw new Exception($"Could not find view for mode: {mode}")
	};
}









