using System.Collections.Immutable;
using ByteQuest.CoreLogic.Data;
using ByteQuest.CoreLogic.Data.Modes;
using ByteQuest.CoreLogic.Entries;
using ByteQuest.CoreLogic.Ledgers;
using ByteQuest.CoreLogic.State;
using ByteQuest.Shell.Views;
using static ByteQuest.CoreLogic.State.GameStateBuilders;


Console.WriteLine("Welcome to ByteQuest!");


var ledger = new GameLedger(ImmutableArray<Entry>.Empty);


var state = CreateFromLedger(ledger);

if (state.Mode is not BattleMode battleMode)
{
	return;
}

BattleModeView.View(battleMode, new StateAndLedger(state, ledger));
