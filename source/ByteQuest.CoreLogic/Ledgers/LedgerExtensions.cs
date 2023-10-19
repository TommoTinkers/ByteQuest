using ByteQuest.CoreLogic.Entries;
using ByteQuest.CoreLogic.State;

namespace ByteQuest.CoreLogic.Ledgers;

public static class LedgerExtensions
{

	public static StateAndLedger RecordEntry(this StateAndLedger snl, Entry entry) =>
		new(State: snl.State.ApplyChange(entry), Ledger: new GameLedger(Entries: snl.Ledger.Entries.Add(entry)));
}