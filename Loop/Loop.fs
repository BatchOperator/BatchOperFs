module BatchOper.Loop

open System.Collections.Generic

type BreakOrContinue =
/// The break statement terminates the closest enclosing loop
| Break
/// The continue statement passes control to the next iteration of the enclosing loop statement in which it appears.
| Continue

/// Loop with Break and Continue 
/// 
/// <example>
/// <c>
/// open BatchOper.Loop
///
/// loop <| seq {
///     for i in 1..10 -> seq {
///         if i < 3 then
///             yield Continue
///         if i > 5 then
///             yield Break
///         }
///     }
/// </c>
/// </example>
///
let loop (s: BreakOrContinue seq seq) = 
    let e = s.GetEnumerator()
    let inline check (c: BreakOrContinue seq) =
        let e = c.GetEnumerator()
        if e.MoveNext() then e.Current
        else Continue
    let inline MoveNext (e: BreakOrContinue seq IEnumerator) =
        e.MoveNext() &&
        match check e.Current with
        | Break -> false
        | Continue -> true
    while MoveNext(e) do ()