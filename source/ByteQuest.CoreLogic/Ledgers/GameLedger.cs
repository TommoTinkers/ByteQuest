using System.Collections.Immutable;
using ByteQuest.CoreLogic.Entries;

namespace ByteQuest.CoreLogic.Ledgers;

public sealed record GameLedger(ImmutableArray<Entry> Entries);