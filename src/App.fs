module App

open Elmish
open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser
open Elmish.Debug
open Elmish.React
open Fable.Core.JsInterop

// Add styles
importAll "../styles/main.sass"

importAll "../node_modules/jquery/dist/jquery.js"
importAll "../node_modules/bootstrap-sass/assets/javascripts/bootstrap.js"

// App
Program.mkProgram Router.State.init Router.State.update Router.View.root
|> Program.toNavigable (parseHash Router.State.pageParser) Router.State.urlUpdate
|> Program.withReact "stage"
//-:cnd
#if DEBUG
|> Program.withDebugger
#endif
//+:cnd
|> Program.run
