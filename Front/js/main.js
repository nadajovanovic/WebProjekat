import { Tim } from "./tim.js";
import {obrisiTablicu} from "./funkcije.js"


let host = document.body
let kont = document.createElement("div")
kont.className = "GlavniKontejner"
host.appendChild(kont)

let divSelekt = document.createElement("div")
divSelekt.className = "divSelekt"
kont.appendChild(divSelekt)
let selekt =  document.createElement("select")
selekt.className = "liga"

let option = document.createElement("option")
option.value = ""
option.innerHTML = "Izaberite ligu"
option.selected="selected"
option.hidden="hidden"
selekt.appendChild(option)

fetch("https://localhost:5001/Liga/VratiLige").then(l => {
    l.json().then(lige => {
        lige.forEach(liga => {
            let option = document.createElement("option")
            option.value = liga.id
            option.innerHTML = liga.naziv
            selekt.appendChild(option)
        })       
    })
})

divSelekt.appendChild(selekt)


selekt.onchange = () => {

    let prikazUtakmice = kont.querySelector(".prikazUtakmice")
    obrisiTablicu(prikazUtakmice)
    prikazUtakmice = document.createElement("div")
    prikazUtakmice.className = "prikazUtakmice"
    kont.appendChild(prikazUtakmice)

    let prikazTimova = kont.querySelector(".prikazTimova")
    obrisiTablicu(prikazTimova)
    prikazTimova = document.createElement("div")
    prikazTimova.className = "prikazTimova"
    kont.appendChild(prikazTimova)
    
    let prikazIgraca = kont.querySelector(".prikazIgraca")
    obrisiTablicu(prikazIgraca)
    prikazIgraca = document.createElement("div")
    prikazIgraca.className = "prikazIgraca"
    kont.appendChild(prikazIgraca)

    let prikazUtakmiceTim = kont.querySelector(".prikazUtakmiceTim")
    obrisiTablicu(prikazUtakmiceTim)
    prikazUtakmiceTim = document.createElement("div")
    prikazUtakmiceTim.className = "prikazUtakmiceTim"
    kont.appendChild(prikazUtakmiceTim)
    
    let t = new Tim((document.querySelector("select.liga").value))
    console.log(t)
    t.crtaj(kont)

}






