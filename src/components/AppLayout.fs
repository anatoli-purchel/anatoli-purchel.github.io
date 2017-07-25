module AppLayout

open Fable.Helpers.React
open Fable.Helpers.React.Props

let root child currentPage =
  div
    [ ClassName "container" ]
    [ div
        []
        [ AppHeader.root currentPage ]
      div
        [ ClassName "panel panel-default" ]
        [ div
            [ ClassName "container" ]
            [ child ] ] ]
