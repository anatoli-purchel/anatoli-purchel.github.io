module ChemTone.View

open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props

open Fable.Import.Browser

open ChemTone.Types

let private simpleButton txt disabled action dispatch =
  div
    [ ClassName "btn btn-info" ]
    [ a
        [ Disabled disabled
          OnClick (fun _ -> action |> dispatch) ]
        [ str txt ] ]

let private fileUploader labelText onDataCmd dispatch =
  div
    [ ClassName "file-upload" ]
    [ label [ ] [ str labelText ]
      input [
        Type "file"
        OnChange (fun x -> onDataCmd(x) |> dispatch) ] ]
let private errorMessage error =
  match error with
  | Some error -> label [ Style [ Color "Red" ] ] [ str error ]
  | None       -> div [] []

type private BootstrapProps =
  | [<CompiledName("aria-describedby")>] AriaDescribedBy of string
  interface IHTMLProp

let root (model:Model) dispatch =
  div
    [ ClassName "col-sm-11" ]
    [ h2
        []
        [ str "Chem tone :)" ]
      div
        []
        [ label
            [ HtmlFor "frequency" ]
            [ str "Frequency:" ]
          div
            [ ClassName "input-group" ]
            [ span
                [ ClassName "input-group-addon"
                  Id "frequency-metric" ]
                [ str "MHz" ]
              input
                ([ ClassName "form-control" :> IHTMLProp
                   Id "frequency" :> IHTMLProp
                   Type "text" :> IHTMLProp ] |> List.append([ AriaDescribedBy "frequency-metric" :> IHTMLProp ])) ]
          label
            [ HtmlFor "csv-file" ]
            [ str "Select csv file:" ]
          div
            [ ClassName "input-group" ]
            [ span
                [ ClassName "btn btn-default btn-file input-group-addon" ]
                [ div
                    []
                    [ str "Browse" ]
                  input
                    [ Type "file"
                      OnChange (fun ev -> !!ev.target?files?(0) |> FileSelected |> dispatch) ] ]
              input
                [ ClassName "form-control"
                  Id "csv-file"
                  Type "text"
                  Disabled true
                  Value (match model.selectedFile with | Some file -> (unbox file?name) | None -> unbox "select file...") ] ]
          br []
          (match model.data with
          | Some data ->
              Waveform.root 500 100 (data |> List.ofSeq)
          | None ->
              span
                []
                [ button
                    [ ClassName "btn btn-default"
                      Disabled (model.selectedFile.IsNone)
                      OnClick (fun _ -> LoadFile |> dispatch) ]
                    [ str "Process..." ]
                  errorMessage model.error ]) ] ]

// TODO: add validation for frequency, disable "Process" button until file and frequency selected
//       also change view to soundwave with play/pause button
//       and also show file name instead of file blob when file is selected :)
