module Waveform

open System

open Fable.Core
open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props

let private bar color width maxHeight (position:float, height:float) =
  line
    [ X1 (!^ position)
      Y1 (!^ (maxHeight / 2.0 - Math.Abs(height / 2.0)))
      X2 (!^ position)
      Y2 (!^ (maxHeight / 2.0 + Math.Abs(height / 2.0)))
      Stroke color
      StrokeWidth (!^ width) ]
    []

let private wave width height data =
  let entries = data |> List.length |> float
  let toBar = bar "green" (width / entries) height
  let abs (x:float) = Math.Abs(x)
  let (_, maxY) = data |> List.maxBy (snd >> abs)
  let (minX, _) = data |> List.minBy (fst >> abs)
  let (maxX, _) = data |> List.maxBy (fst >> abs)

  data
  |> List.map(fun (x, y) ->
       let position = (x - minX) * width / (maxX - minX)
       let barHeight = y * (height / 2.0) / maxY |> abs
       toBar (position, barHeight))

let root width height data =
  svg
    [ ViewBox <| sprintf "0 0 %i %i" width height
      unbox("width", "100%")
      unbox("height", "100%") ]
    (wave (float width) (float height) data)
