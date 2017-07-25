module ChemTone.State

open Elmish
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.Browser

open ChemTone.Types

// https://tonejs.github.io/
let private Tone : obj = importAll "Tone"

let private synth = (createNew Tone?Synth ())?toMaster()


[<Emit("new FileReader()")>]
let private getReader () : obj = jsNative

let init () : Model * Cmd<Msg> =
  { synth = (createNew Tone?Synth ())?toMaster()
    selectedFile = None
    loadingFile = false
    data = None
    error = None }, []

let private play (model:Model) =
  let minFreq = 32
  let maxFreq = 16384
  let playFrame(x, y) =
    model.synth?triggerAttackRelease(x, x * 10.0) |> ignore

  async {
    console.info("will play" + model.data.ToString())
    match model.data with
    | Some data -> data |> Seq.iter playFrame
    | None      -> ()
  }

let private loadFile (model:Model) dispatch =
  match model.selectedFile with
  | None -> ()
  | Some fileName ->
      let reader = getReader()
      console.info(reader)
      reader?onload <- (fun () ->
        console.info("onload fired!")
        let data = !!reader?result: string
        let regex = System.Text.RegularExpressions.Regex("[\r\n]+")
        console.log(regex.Split(data).Length)
        regex.Split(data)
        |> Array.skip 3 // skip 2 lines containing some chem data and headers line
        |> Array.map(fun line ->
             let parts = line.Split [| '\t'; ',' |]
             if parts.Length >= 2 then
               parts.[0] |> float, parts.[1] |> float
             else
               0.0, 0.0)
        |> Seq.ofArray
        |> Some |> FileLoaded |> dispatch)
      reader?onerror <- (FileLoadingFailed >> dispatch)
      reader?readAsText fileName |> ignore
      console.info("reader readAsText called")

let private noop _ = NoOp

let update msg model =
  match msg with
  | NoOp ->
      model, []
  | Play ->
      model, [ Cmd.ofAsync play model noop noop ]
  | FileSelected file ->
      console.log("file selected: " + !!file?name)
      { model with selectedFile = Some file; error = None }, []
  | LoadFile ->
      { model with loadingFile = true }, [ Cmd.ofSub (loadFile model) ]
  | FileLoaded data ->
      { model with data = data; error = None; loadingFile = false }, []
  | FileLoadingFailed error ->
      { model with error = Some error; loadingFile = false }, []
