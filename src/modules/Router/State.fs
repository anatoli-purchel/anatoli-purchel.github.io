module Router.State

open Elmish
open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser
open Fable.Import.Browser

open Router.Types
open Routes

let pageParser: Parser<Page->Page,Page> =
  oneOf [
    map About    (s "about")
    map ChemTone (s "chem-tone")
    map Home     (s "home")
  ]

let urlUpdate (result: Option<Page>) model =
  match result with
  | None ->
      console.error("Error parsing url")
      model, Navigation.modifyUrl (toHash model.currentPage)
  | Some page ->
      { model with currentPage = page }, []

let init result =
  // initialize each page state
  let (chemTone, chemToneCmd) = ChemTone.State.init()

  // create initial app state
  let initialState =
    { currentPage = Home
      chemTone    = chemTone }

  // update url after initial state created if needed
  let (model, cmd) = urlUpdate result initialState

  model, Cmd.batch [
    cmd
    Cmd.map ChemToneMsg chemToneCmd
  ]

let update msg model =
  match msg with
  | ChemToneMsg msg ->
      let (chemTone, chemToneCmd) = ChemTone.State.update msg model.chemTone
      { model with chemTone = chemTone }, Cmd.map ChemToneMsg (Cmd.batch chemToneCmd)
