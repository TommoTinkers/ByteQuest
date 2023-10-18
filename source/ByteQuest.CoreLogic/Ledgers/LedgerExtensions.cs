using ByteQuest.CoreLogic.Entries;
using ByteQuest.CoreLogic.State;

namespace ByteQuest.CoreLogic.Ledgers;

public static class LedgerExtensions
{
	public static GameLedger RecordEntry(this GameLedger ledger, Entry entry)
	{
		if (ledger.Entries.Select(e => e.id).Contains(entry.id))
		{
			return ledger;
		}
		return new GameLedger(Entries: ledger.Entries.Add(entry));
	}

	public static StateAndLedger RecordEntry(this StateAndLedger snl, Entry entry) =>
		new(State: snl.State.ApplyChange(entry), Ledger: new GameLedger(Entries: snl.Ledger.Entries.Add(entry)));
}