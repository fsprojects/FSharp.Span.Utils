FSharp.Span.Utils
=================
Makes Span/ReadOnlySpan easy to use from F#.

* Wrapper modules for accessing members and `System.MemoryExtensions` (`module Span, module ReadOnlySpan, module StringSpan`).
* Slicing support for `Span<T>` and `ReadOnlySpan<T>` (`let spanSlice : _ span = spanOrig.[0..10]`).
* `String.toSpan / String.toMemory`.
* `SafeLowLevelOperators.stackalloc`.

## License

MIT
