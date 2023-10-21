using ByteQuest.Core.Data;
using ByteQuest.Core.Modes;
using ByteQuest.Shell.Views;

Console.WriteLine("Welcome to ByteQuest!");



var enemy = new Enemy (Name: "Bob The Goblin", Health: 10u, Strength: 5u, Accuracy: 5u, Evasion: 5u, Defence: 5u);

var player = new Player( Health: 10u, Strength: 5u, Accuracy: 5u, Evasion: 5u, Defence: 5u);

var battleMode = new BattleMode(player, enemy, ByteQuest.Core.Modes.Turn.Players);


BattleView.View(battleMode);









