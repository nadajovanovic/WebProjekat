
import { Igrac } from "./igrac.js";
import { dodajbodykolona, dodajhederkolona, tabela, cleanTable, dodajdugme, obrisiTablicu, pocetnaformica, dodajInput} from "./funkcije.js";
import { Utakmica } from "./utakmica.js";
export class Tim{

    constructor( liga){

        this.liga = liga;

    }

    crtajFormuTim(forma){
        let inNaziv = dodajInput(forma, "inNaziv", "inNaziv", "Naziv")
        let inPred = dodajInput(forma, "inPred", "inPred", "Predsednik")
        let inTr = dodajInput(forma, "inTr", "inTr", "Trener")

    }


    crtaj(kont){
        
        let prikazTimova = kont.querySelector(".prikazTimova")
       console.log(prikazTimova)
const imeForme = "Tim"
let dugmeNoviTim = prikazTimova.querySelector(".NoviTim")
obrisiTablicu(dugmeNoviTim)
dugmeNoviTim = pocetnaformica(prikazTimova, imeForme, "NOVI TIM")
let forma = prikazTimova.querySelector(".Forma"+imeForme)



dugmeNoviTim.onclick = () =>{
    
    this.crtajFormuTim(forma)

    let dugmeDodajTim = document.querySelector(".dodajTim")
    obrisiTablicu(dugmeDodajTim)

    dugmeDodajTim = document.createElement("button")
    dugmeDodajTim.className = "btn btn-primary"
    dugmeDodajTim.classList.add("dodajTim")
    dugmeDodajTim.innerHTML = "Dodaj tim"
    forma.appendChild(dugmeDodajTim)
    dugmeDodajTim.onclick = () =>{
    
        let naziv = (document.querySelector(".inNaziv").value)
        let trener = (document.querySelector(".inTr").value)
        let predsednik = (document.querySelector(".inPred").value)
        let liga = (document.querySelector("select.liga").value)
        
        if(naziv == "" || trener == "" || predsednik ==""){
            alert("UNSETE SVE PODATKE!!!")
            return
        }


        fetch("https://localhost:5001/Tim/DodajTim/" + naziv + "/" + trener + "/" + predsednik + "/" + liga,
                {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                }).then(i => {
                    if (i.ok) {
                        pronadjiga()
                        let ocisti = document.querySelectorAll("input")
                        ocisti.forEach(element => {
                            element.value = ""
                        });
                        alert("Uspresno dodat tim!")
                        

                    }
                    else{
                        alert("Doslo je do greske!")
                    }
                })
    
    }
}
    let dugmeDodajUtakmicu = document.querySelector(".dugmeDodajUtakmicu")
    obrisiTablicu(dugmeDodajUtakmicu)

    let utakmice = new Utakmica(0,(kont.querySelector("select.liga").value))
    utakmice.crtajDodaj(kont)

    
    let tablicaTimovi = tabela(prikazTimova, "Timovi", "timoviBody", "timoviHead")
    
    let thead = tablicaTimovi.querySelector(".timoviHead")
    let tbody = tablicaTimovi.querySelector(".timoviBody")

    let header_row = document.createElement("tr")
    thead.appendChild(header_row)

    const lista = ["Naziv", "Trener", "Predsednik"]

    lista.forEach(p => dodajhederkolona(header_row, p))
    this.pronadjiga(kont, tablicaTimovi, tbody, forma)
}
    
 pronadjiga(kont, tablicaTimovi, tbody, forma){   
  tbody = cleanTable(tablicaTimovi, tbody)

  fetch("https://localhost:5001/Tim/VratiTim/" + this.liga).then(vt => {
       vt.json().then(timovi=>{
           timovi.forEach(tim => {

                let body_row = document.createElement("tr")
                tbody.appendChild(body_row)
                const lista = [tim.naziv, tim.predsednik, tim.trener]
                lista.forEach(p => dodajbodykolona(body_row, p))

                let btnIgrac = dodajdugme(body_row, "Igrac")
                btnIgrac.onclick = ev => {

                        let prikazUtakmiceTim = kont.querySelector(".prikazUtakmiceTim")
                        obrisiTablicu(prikazUtakmiceTim)
                        prikazUtakmiceTim = document.createElement("div")
                        prikazUtakmiceTim.className = "prikazUtakmiceTim"
                        kont.appendChild(prikazUtakmiceTim)

                        let igraci = new Igrac(tim.id, tim.naziv)
                        igraci.crtajIgrace(kont)

                    }
                let btnUtakmice = dodajdugme(body_row, "Utakmica")
                btnUtakmice.onclick = ev => {
                    let prikazIgraca = kont.querySelector(".prikazIgraca")
                    obrisiTablicu(prikazIgraca)
                    prikazIgraca = document.createElement("div")
                    prikazIgraca.className = "prikazIgraca"
                    kont.appendChild(prikazIgraca)
                     let utakmice = new Utakmica(tim.id,(document.querySelector("select.liga").value))
                     utakmice.crtajPrikazi(kont)
                    }
                
                    let btnAzuriraj = dodajdugme(body_row, "Azriraj")
                    btnAzuriraj.onclick = ev => {
                        
                        this.crtajFormuTim(forma)

                        kont.querySelector(".inNaziv").value = tim.naziv
                        kont.querySelector(".inPred").value = tim.predsednik
                        kont.querySelector(".inTr").value = tim.trener
                       
                        let dugme = forma.querySelector(".dodajTim")

                        obrisiTablicu(dugme)
            
                        dugme = document.createElement("button")
                        dugme.className = "btn btn-primary"
                        dugme.classList.add("dodajTim")
                        dugme.innerHTML = "Azuriraj tima"
                        forma.appendChild(dugme)
                        dugme.onclick = () =>{
            
                            let naziv = (document.querySelector(".inNaziv").value)
                            let trener = (document.querySelector(".inTr").value)
                            let predsednik = (document.querySelector(".inPred").value)
                            
                            if(naziv == "" || trener == "" || predsednik ==""){
                                alert("UNSETE SVE PODATKE!!!")
                                return
                            }
                            console.log(tim.id)
                            console.log(naziv)
                            console.log(predsednik)
                            console.log(trener)
                            fetch("https://localhost:5001/Tim/AzurirajTim/"+ tim.id + "/" + naziv +"/"+ predsednik +"/" + trener,
                                    {
                                        method: "PUT",
                                        headers: {
                                            "Content-Type": "application/json"
                                        },
                                    }).then(i => {
                                        if (i.ok) {
                                            alert("Uspresno azuriran tim!")
                                            this.crtaj(kont)
                                            let ocisti = forma.querySelectorAll("input")
                                            ocisti.forEach(element => {
                                                element.value = ""
                                            });
            
                                        }
                                        else{
                                            alert("Doslo je do greske!")
                                        }
                                    })                            
                    }
                   
                    }
           })
       })
   })
}
}