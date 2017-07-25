module ChemTone.Types

type Model =
  { synth        : obj
    selectedFile : obj option
    loadingFile  : bool
    data         : (float * float) seq option
    error        : string option }

type Msg =
  | FileSelected of obj
  | LoadFile
  | FileLoaded of (float * float) seq option
  | FileLoadingFailed of string
  | Play
  | NoOp
