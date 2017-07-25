module Router.Types

type Msg =
  | ChemToneMsg of ChemTone.Types.Msg

type Model =
  { currentPage : Routes.Page
    chemTone    : ChemTone.Types.Model }
