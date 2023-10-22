using ByteQuest.Core.Data;
using ByteQuest.Core.Modes;
using ByteQuest.Core.Modes.Battle;
using ByteQuest.Core.State;
using ByteQuest.Shell.Views;

Console.WriteLine("Welcome to ByteQuest!");


var enemy = new Enemy (Name: "Bob The Goblin", Health: 200u, Strength: 5u, Accuracy: 5u, Evasion: 5u, Defence: 5u);

var player = new Player( Health: 2u, Strength: 5u, Accuracy: 5u, Evasion: 5u, Defence: 5u);

var state = new GameState(10);
Mode mode = new PlayersTurn(player, enemy);

var pair = (state, mode);

while (true)
{
	if (pair.mode is ExitGameMode)
	{
		return;
	}
	
	pair = pair.mode switch
	{
		BattleMode bm => BattleView.View(pair.state, bm),
		PlayerDiedMode => PlayerDiedView.View(pair.state),
		_ => throw new Exception($"Could not find view for mode: {mode}")
	};
}









