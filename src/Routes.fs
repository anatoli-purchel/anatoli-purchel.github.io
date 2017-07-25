module Routes

type Page =
  | Home
  | ChemTone
  | About

let toHash page =
  match page with
  | About -> "#about"
  | ChemTone -> "#chem-tone"
  | Home -> "#home"
