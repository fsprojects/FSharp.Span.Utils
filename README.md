FSharp.Span.Utils
=================
Makes Span/ReadOnlySpan easy to use from F#.

Can be [installed from NuGet](https://nuget.org/packages/FSharp.Span.Utils).

```
  PM> Install-Package FSharp.Span.Utils
```

* Wrapper modules for accessing members and `System.MemoryExtensions` (`module Span, module ReadOnlySpan, module StringSpan`).
* Slicing support for `Span<T>` and `ReadOnlySpan<T>` (`let spanSlice : _ span = spanOrig.[0..10]`).
* `String.toSpan / String.toMemory`.
* `SafeLowLevelOperators.stackalloc`.

## License

MIT
