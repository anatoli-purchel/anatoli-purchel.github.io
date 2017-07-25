module Home.View

open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props

type RCom = Fable.Import.React.ComponentClass<obj>

let markdown = importDefault<RCom> "react-markdown"

let private aboutContent = """
# Home page

`¯\_(ツ)_/¯`
"""

let root =
  div
    []
    [ Fable.Helpers.React.from markdown
        (createObj [ "source" ==> aboutContent
                     "skipHtml" ==> false
                     "className" ==> "result" ]) [] ]
