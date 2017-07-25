module AppHeader

open Fable.Helpers.React
open Fable.Helpers.React.Props

open Routes

let private menuItem label page currentPage =
    li
      [ classList [ "active", page = currentPage ] ]
      [ a
          [ Href (toHash page) ]
          [ str label ] ]

let private menu currentPage =
  ul
    [ ClassName "nav navbar-nav" ]
    [ menuItem "Home"       Page.Home     currentPage
      menuItem "About"      Page.About    currentPage
      menuItem "Playground" Page.ChemTone currentPage ]

let private socialLink icon label url =
  li
    []
    [ a
        [ Href url
          Alt label
          ClassName ("glyphicon " + icon) ]
        [] ]

let private socialLinks =
  ul
    [ ClassName "nav navbar-nav navbar-right" ]
    [ socialLink "glyphicon-envelope" "email" "mailto:purch004@umn.edu" ]

type BootstrapProps =
  | [<CompiledName("data-target")>] DataTarget of string
  interface IHTMLProp

let root currentPage =
  nav
    [ ClassName "navbar navbar-default" ]
    [ div
        [ ClassName "container-fluid" ]
        [ div
            [ ClassName "navbar-header" ]
            [ button
                ([ ClassName "navbar-toggle collapsed" :> IHTMLProp
                   Type "button" :> IHTMLProp
                   DataToggle "collapse" :> IHTMLProp
                   AriaExpanded false :> IHTMLProp ]
                 |> List.append ([DataTarget "#collapsable-navbar" :> IHTMLProp])
                )
                [ span [ ClassName "sr-only" ] [ str "Toggle navigation" ]
                  span [ ClassName "icon-bar" ] []
                  span [ ClassName "icon-bar" ] []
                  span [ ClassName "icon-bar" ] [] ]
              a
                [ ClassName "navbar-brand"
                  Href (toHash Page.About) ]
                [ str "Anatoli Purchel" ] ]
          div
            [ ClassName "collapse navbar-collapse"
              Id "collapsable-navbar" ]
            [ menu currentPage
              socialLinks ] ] ]
