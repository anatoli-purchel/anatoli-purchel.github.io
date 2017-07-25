module Router.View

open Routes
open Router.Types
open Router.State

let root model dispatch =
  let routings =
    function
    | Page.Home     -> Home.View.root
    | Page.About    -> About.View.root
    | Page.ChemTone -> ChemTone.View.root model.chemTone (ChemToneMsg >> dispatch)

  AppLayout.root (routings model.currentPage) model.currentPage
