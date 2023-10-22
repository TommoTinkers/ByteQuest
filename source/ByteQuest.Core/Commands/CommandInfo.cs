using ByteQuest.Core.Modes;
using ByteQuest.Core.State;

namespace ByteQuest.Core.Commands;

public record CommandInfo<TResult, TMode>(string Name, Handler<TResult,TMode> Handler) where TMode : Mode;

public delegate TResult Handler<out TResult, in TMode>(GameState state, TMode mode) where TMode : Mode;