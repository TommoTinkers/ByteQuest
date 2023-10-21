using ByteQuest.Core.Data;

namespace ByteQuest.Core.Modes;

public sealed record BattleMode(Player Player, Enemy Enemy, Turn Turn) : Mode;

public enum Turn
{
	Players,
	Enemies
}