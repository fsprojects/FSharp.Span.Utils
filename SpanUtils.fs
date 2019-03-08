(*
The MIT License

FSharp.Span.Utils - helper modules for Span<'T>/ReadOnlySpan<'T>
Copyright(c) 2019 cannorin

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*)

module FSharp.Span.Utils

open System
open System.Collections.Generic
open FSharp.NativeInterop

#nowarn "9"
type 'a span = Span<'a>
type 'a readonlyspan = ReadOnlySpan<'a>
type 'a memory = Memory<'a>
type 'a readonlymemory = ReadOnlyMemory<'a>
type stringspan = readonlyspan<char>

module SafeLowLevelOperators =
  let inline stackalloc<'a when 'a: unmanaged> length : 'a span =
    let mem = NativePtr.stackalloc<'a> length
    span<'a>(mem |> NativePtr.toVoidPtr, length)

module Span =
  let inline ofArray (xs: _[]) = span(xs)
  let inline ofPtr (p: 'a nativeptr, size) =
    span<'a>(p |> NativePtr.toVoidPtr, size)
  let inline ofMemory (mem: _ memory) : _ span = mem.Span
  let inline toArray (s: _ span) = s.ToArray()

  let inline isEmpty (s: _ span) = s.IsEmpty
  let inline length (s: _ span) = s.Length
  let inline item i (s: _ span) = s.[i]
  let inline rev (s: _ span) = s.Reverse()
  let inline take (i: int) (s: _ span) : _ span = s.Slice(0, i)
  let inline skip (i: int) (s: _ span) : _ span = s.Slice(i)
  let inline slice (startIndex: int) (length: int) (s: _ span) : _ span = s.Slice(startIndex, length)
  let inline findIndex (x: 'a) (s: 'a span) = MemoryExtensions.IndexOf(s, x)
  let inline findIndexOfSpan (xs: _ readonlyspan) (s: _ span) = MemoryExtensions.IndexOf(s, xs)
  let inline findIndexOfAnyOf2 x1 x2 (s: _ span) = MemoryExtensions.IndexOfAny(s, x1, x2)
  let inline findIndexOfAnyOf3 x1 x2 x3 (s: _ span) = MemoryExtensions.IndexOfAny(s, x1, x2, x3)
  let inline findIndexOfAnyOf (xs: _ readonlyspan) (s: _ span) = MemoryExtensions.IndexOfAny(s, xs)
  let inline findLastIndex (x: 'a) (s: 'a span) = MemoryExtensions.LastIndexOf(s, x)
  let inline findLastIndexOfSpan (xs: _ readonlyspan) (s: _ span) = MemoryExtensions.LastIndexOf(s, xs)
  let inline findLastIndexOfAnyOf2 x1 x2 (s: _ span) = MemoryExtensions.LastIndexOfAny(s, x1, x2)
  let inline findLastIndexOfAnyOf3 x1 x2 x3 (s: _ span) = MemoryExtensions.LastIndexOfAny(s, x1, x2, x3)
  let inline findLastIndexOfAnyOf (xs: _ readonlyspan) (s: _ span) = MemoryExtensions.LastIndexOfAny(s, xs)
  let inline overlaps (other: _ readonlyspan) (s: _ span) = s.Overlaps(other)
  let inline sequenceCompareTo (xs: _ readonlyspan) (s: _ span) = MemoryExtensions.SequenceCompareTo(s, xs)
  let inline sequenceEqual (xs: _ readonlyspan) (s: _ span) = MemoryExtensions.SequenceEqual(s, xs)
  let inline startsWith (xs: _ readonlyspan) (s: _ span) = MemoryExtensions.StartsWith(s, xs)
  let inline endsWith (xs: _ readonlyspan) (s: _ span) = MemoryExtensions.EndsWith(s, xs)

  let inline fillWith x (s: _ span) = s.Fill(x)
  let inline binarySearch<'a when 'a :> IComparable<'a>> (x: 'a) (s: 'a span) =
    s.BinarySearch(x :> IComparable<_>)
  let inline binarySearchBy (comparer: 'a -> 'a -> int) (x: 'a) (s: 'a span) =
    s.BinarySearch(x, Comparer.Create(Comparison(comparer)))

module ReadOnlySpan =
  let inline ofArray (xs: _[]) = readonlyspan(xs)
  let inline ofPtr (p: 'a nativeptr, size) =
    readonlyspan<'a>(p |> NativePtr.toVoidPtr, size)
  let inline ofMemory (mem: _ readonlymemory) : _ readonlyspan = mem.Span
  let inline ofString (s: string) : _ readonlyspan = s.AsSpan()

  let inline toArray (s: _ readonlyspan) = s.ToArray()
  
  let inline isEmpty (s: _ readonlyspan) = s.IsEmpty
  let inline length (s: _ readonlyspan) = s.Length
  let inline item i (s: _ readonlyspan) = s.[i]
  let inline take (i: int) (s: _ readonlyspan) : _ readonlyspan = s.Slice(0, i)
  let inline skip (i: int) (s: _ readonlyspan) : _ readonlyspan = s.Slice(i)
  let inline slice (startIndex: int) (length: int) (s: _ readonlyspan) : _ readonlyspan = s.Slice(startIndex, length)
  let inline findIndex (x: 'a) (s: 'a readonlyspan) = MemoryExtensions.IndexOf(s, x)
  let inline findIndexOfSpan (xs: _ readonlyspan) (s: _ readonlyspan) = MemoryExtensions.IndexOf(s, xs)
  let inline findIndexOfAnyOf2 x1 x2 (s: _ readonlyspan) = MemoryExtensions.IndexOfAny(s, x1, x2)
  let inline findIndexOfAnyOf3 x1 x2 x3 (s: _ readonlyspan) = MemoryExtensions.IndexOfAny(s, x1, x2, x3)
  let inline findIndexOfAnyOf (xs: _ readonlyspan) (s: _ readonlyspan) = MemoryExtensions.IndexOfAny(s, xs)
  let inline findLastIndex (x: 'a) (s: 'a readonlyspan) = MemoryExtensions.LastIndexOf(s, x)
  let inline findLastIndexOfSpan (xs: _ readonlyspan) (s: _ readonlyspan) = MemoryExtensions.LastIndexOf(s, xs)
  let inline findLastIndexOfAnyOf2 x1 x2 (s: _ readonlyspan) = MemoryExtensions.LastIndexOfAny(s, x1, x2)
  let inline findLastIndexOfAnyOf3 x1 x2 x3 (s: _ readonlyspan) = MemoryExtensions.LastIndexOfAny(s, x1, x2, x3)
  let inline findLastIndexOfAnyOf (xs: _ readonlyspan) (s: _ readonlyspan) = MemoryExtensions.LastIndexOfAny(s, xs)
  let inline overlaps (other: _ readonlyspan) (s: _ readonlyspan) = s.Overlaps(other)
  let inline sequenceCompareTo (xs: _ readonlyspan) (s: _ readonlyspan) = MemoryExtensions.SequenceCompareTo(s, xs)
  let inline sequenceEqual (xs: _ readonlyspan) (s: _ readonlyspan) = MemoryExtensions.SequenceEqual(s, xs)
  let inline startsWith (xs: _ readonlyspan) (s: _ readonlyspan) = MemoryExtensions.StartsWith(s, xs)
  let inline endsWith (xs: _ readonlyspan) (s: _ readonlyspan) = MemoryExtensions.EndsWith(s, xs)

  let inline binarySearch<'a when 'a :> IComparable<'a>> (x: 'a) (s: 'a readonlyspan) =
    s.BinarySearch(x :> IComparable<_>)
  let inline binarySearchBy (comparer: 'a -> 'a -> int) (x: 'a) (s: 'a readonlyspan) =
    s.BinarySearch(x, Comparer.Create(Comparison(comparer)))

type Span<'a> with
  member inline this.GetSlice(startIndex: int option, endIndex: int option) : _ span =
    let s = defaultArg startIndex 0
    if endIndex.IsSome then
      this.Slice(s, endIndex.Value - s + 1)
    else
      this.Slice(s)
  member inline this.Item
    with get (i: int) = Span.item i this
    and set (i: int) (v: _) =
      let r : byref<_> = &this.[i]
      r <- v
  member inline this.get i = this.[i]
  member inline this.set i x = this.[i] <- x
    
type ReadOnlySpan<'a> with
  member inline this.GetSlice(startIndex: int option, endIndex: int option) : _ readonlyspan =
    let s = defaultArg startIndex 0
    if endIndex.IsSome then
      this.Slice(s, endIndex.Value - s + 1)
    else
      this.Slice(s)
  member inline this.get i = this.[i]

type Memory<'a> with
  member inline this.GetSlice(startIndex: int option, endIndex: int option) : _ memory =
    let s = defaultArg startIndex 0
    if endIndex.IsSome then
      this.Slice(s, endIndex.Value - s + 1)
    else
      this.Slice(s)

type ReadOnlyMemory<'a> with
  member inline this.GetSlice(startIndex: int option, endIndex: int option) : _ readonlymemory =
    let s = defaultArg startIndex 0
    if endIndex.IsSome then
      this.Slice(s, endIndex.Value - s + 1)
    else
      this.Slice(s)

module StringSpan =
  let inline ofString (s: string) : stringspan = s.AsSpan()
  let inline toString (s: stringspan) = s.ToString()

  let inline isEmpty (s: stringspan) = s.IsEmpty
  let inline length (s: stringspan) = s.Length
  let inline item i (s: stringspan) = s.[i]
  let inline take (i: int) (s: stringspan) : stringspan = s.Slice(0, i)
  let inline skip (i: int) (s: stringspan) : stringspan = s.Slice(i)
  let inline findIndex (x: char) (s: stringspan) = MemoryExtensions.IndexOf(s, x)
  let inline findIndexOfSpan (xs: stringspan) (s: stringspan) = MemoryExtensions.IndexOf(s, xs)
  let inline findIndexOfAnyOf2 (x1: char) (x2: char) (s: stringspan) = MemoryExtensions.IndexOfAny(s, x1, x2)
  let inline findIndexOfAnyOf3 (x1: char) (x2: char) (x3: char) (s: stringspan) = MemoryExtensions.IndexOfAny(s, x1, x2, x3)
  let inline findIndexOfAnyOf (xs: stringspan) (s: stringspan) = MemoryExtensions.IndexOfAny(s, xs)
  let inline findLastIndex (x: char) (s: stringspan) = MemoryExtensions.LastIndexOf(s, x)
  let inline findLastIndexOfSpan (xs: stringspan) (s: stringspan) = MemoryExtensions.LastIndexOf(s, xs)
  let inline findLastIndexOfAnyOf2 (x1: char) (x2: char) (s: stringspan) = MemoryExtensions.LastIndexOfAny(s, x1, x2)
  let inline findLastIndexOfAnyOf3 (x1: char) (x2: char) (x3: char) (s: stringspan) = MemoryExtensions.LastIndexOfAny(s, x1, x2, x3)
  let inline findLastIndexOfAnyOf (xs: stringspan) (s: stringspan) = MemoryExtensions.LastIndexOfAny(s, xs)
  let inline overlaps (other: stringspan) (s: stringspan) = s.Overlaps(other)
  let inline sequenceCompareTo (other: stringspan) (s: stringspan) = MemoryExtensions.SequenceCompareTo(s, other)
  let inline sequenceEqual (other: stringspan) (s: stringspan) = MemoryExtensions.SequenceEqual(s, other)

  let inline substring startIndex endIndex (s: stringspan) : stringspan = s.Slice(startIndex, endIndex - startIndex)

  [<Literal>]
  let DefaultComparison =
    #if !NETSTANDARD1_6
    StringComparison.InvariantCulture
    #else
    StringComparison.CurrentCulture
    #endif

  let inline compare (other: stringspan) (s: stringspan) =
    MemoryExtensions.CompareTo(s, other, DefaultComparison)
  let inline compareOrdinal (other: stringspan) (s: stringspan) =
    MemoryExtensions.CompareTo(s, other, StringComparison.Ordinal)
  let inline compareWithComparison (cmp: StringComparison) (other: stringspan) (s: stringspan) =
    MemoryExtensions.CompareTo(s, other, cmp)
  let inline contains (other: stringspan) (s: stringspan) =
    MemoryExtensions.Contains(s, other, DefaultComparison)
  let inline containsOrdinal (other: stringspan) (s: stringspan) =
    MemoryExtensions.Contains(s, other, StringComparison.Ordinal)
  let inline containsWithComparison (cmp: StringComparison) (other: stringspan) (s: stringspan) =
    MemoryExtensions.Contains(s, other, cmp)
  let inline equals (other: stringspan) (s: stringspan) =
    MemoryExtensions.Equals(s, other, DefaultComparison)
  let inline equalsOrdinal (other: stringspan) (s: stringspan) =
    MemoryExtensions.Equals(s, other, StringComparison.Ordinal)
  let inline equalsWithComparison (cmp: StringComparison) (other: stringspan) (s: stringspan) =
    MemoryExtensions.Equals(s, other, cmp)
  let inline startsWith (other: stringspan) (s: stringspan) =
    MemoryExtensions.StartsWith(s, other, DefaultComparison)
  let inline startsWithOrdinal (other: stringspan) (s: stringspan) =
    MemoryExtensions.StartsWith(s, other, StringComparison.Ordinal)
  let inline startsWithComparison (cmp: StringComparison) (other: stringspan) (s: stringspan) =
    MemoryExtensions.StartsWith(s, other, cmp)
  let inline endsWith (other: stringspan) (s: stringspan) =
    MemoryExtensions.EndsWith(s, other, DefaultComparison)
  let inline endsWithOrdinal (other: stringspan) (s: stringspan) =
    MemoryExtensions.EndsWith(s, other, StringComparison.Ordinal)
  let inline endsWithComparison (cmp: StringComparison) (other: stringspan) (s: stringspan) =
    MemoryExtensions.EndsWith(s, other, cmp)

  let inline binarySearch (x: char) (s: stringspan) =
    s.BinarySearch(x :> IComparable<_>)
  let inline binarySearchBy (comparer: char -> char -> int) (x: char) (s: stringspan) =
    s.BinarySearch(x, Comparer.Create(Comparison(comparer)))

module String =
  let inline toSpan (s: string) : stringspan = s.AsSpan()
  let inline toMemory (s: string) : char readonlymemory = s.AsMemory()
