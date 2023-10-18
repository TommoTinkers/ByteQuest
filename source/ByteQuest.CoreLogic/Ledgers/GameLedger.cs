using System.Collections.Immutable;
using ByteQuest.CoreLogic.Entries;
using ByteQuest.CoreLogic.State;

namespace ByteQuest.CoreLogic.Ledgers;

public sealed record GameLedger(ImmutableArray<Entry> Entries);

public sealed record StateAndLedger(GameState State, GameLedger Ledger);



