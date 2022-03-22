import { Tim } from "./tim.js";
import { Igrac } from "./igrac.js";
import { dodajbodykolona, dodajhederkolona, tabela, cleanTable, dodajdugme, obrisiTablicu, pocetnaformica, dodajInput, dodajInputBroj} from "./funkcije.js";

export class Utakmica{

    constructor(timid, ligaid ){
        this.ligaid = ligaid
        this.timid =timid
    }
    crtajFormuUtakmica(forma){

        let inInfo = dodajInput(forma, "inInfo", "inInfo", "Info")

        let inDomacin = forma.querySelector(".inDomacin")        
        obrisiTablicu(inDomacin)
        inDomacin =  document.createElement("select")
        inDomacin.className = "inDomacin"
        
        let option1 = document.createElement("option")
        option1.value = ""
        option1.innerHTML = "Domacin"
        option1.selected="selected"
        option1.hidden="hidden"
        inDomacin.appendChild(option1)
        
        let inGost = forma.querySelector(".inGost")        
        obrisiTablicu(inGost)
        inGost =  document.createElement("select")
        inGost.className = "inGost"

        let option = document.createElement("option")
        option.value = ""
        option.innerHTML = "Gost"
        option.selected="selected"
        option.hidden="hidden"
        inGost.appendChild(option)



        fetch("https://localhost:5001/Tim/VratiTim/" + this.ligaid).then(vt => {
            vt.json().then(timovi=>{
                timovi.forEach(tim => {
                    let option = document.createElement("option")
                    option.value = tim.id
                    option.innerHTML = tim.naziv
                    inDomacin.appendChild(option)
                    let option1 = document.createElement("option")
                    option1.value = tim.id
                    option1.innerHTML = tim.naziv
                    inGost.appendChild(option1)

                })       
            })
        })

        console.log(inDomacin)
        console.log(inDomacin.value)
        forma.appendChild(inDomacin)
        forma.appendChild(inGost)
        //inDomacin.disabled = true

    }

    crtajDodaj(kont){

        let prikazUtakmice = kont.querySelector(".prikazUtakmice")

        const imeForme = "Utakmica"
        let dugmeNovaUtakmica = prikazUtakmice.querySelector(".NovaUtakmica")
        obrisiTablicu(dugmeNovaUtakmica)
        dugmeNovaUtakmica = pocetnaformica(prikazUtakmice, imeForme, "NOVA UTAKMICA")

        let forma = prikazUtakmice.querySelector(".Forma"+imeForme)

        dugmeNovaUtakmica.onclick = () =>{
    
            this.crtajFormuUtakmica(forma)

            let ocisti = forma.querySelectorAll("input")
            ocisti.forEach(element => {
                element.value = ""
            });            

            let dugmeDodajUtakmicu = forma.querySelector(".dodajUtakmicu")
            obrisiTablicu(dugmeDodajUtakmicu)

            dugmeDodajUtakmicu = document.createElement("button")
            dugmeDodajUtakmicu.className = "btn btn-primary"
            dugmeDodajUtakmicu.classList.add("dodajUtakmicu")
            dugmeDodajUtakmicu.innerHTML = "Dodaj utakmicu"
            forma.appendChild(dugmeDodajUtakmicu)
            dugmeDodajUtakmicu.onclick = () =>{


                let info = (forma.querySelector(".inInfo").value)             
                let domacin =(forma.querySelector("select.inDomacin").value)
                let gost =(forma.querySelector("select.inGost").value)
                this.timid = domacin
                
                if(info == "" || domacin == "" || gost ==""){
                    alert("UNSETE SVE PODATKE!!!")
                    return
                }else if( gost == domacin){
                    alert("MORATE UNETI 2 RAZLICITE EKIPE")
                    return
                }

                fetch("https://localhost:5001/Utakmica/DodajUtakmicu/" + info + "/" + this.ligaid + "/" + domacin + "/" + gost,
                        {
                            method: "POST",
                            headers: {
                                "Content-Type": "application/json"
                            },
                        }).then(u => {
                            if (u.ok) {
                                this.crtajPrikazi(kont)
                                alert("Uspresno dodata utakmica!")                               
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

    }

    crtajPrikazi(kont){
        
        console.log(this.timid)
        let prikazUtakmiceTim = kont.querySelector(".prikazUtakmiceTim")

        let tablica = tabela(prikazUtakmiceTim, "UtakmiceTim", "utBody", "utHead")
        let thead = tablica.querySelector(".utHead")
        let header_row = document.createElement("tr")
        let tbody = tablica.querySelector(".utBody")

        thead.appendChild(header_row)
        const lista = ["Gost", "Domacin", "Info"]
    
        lista.forEach(p => dodajhederkolona(header_row, p))
        this.crtaj(tablica, tbody)
        console.log(prikazUtakmiceTim)
        console.log(tablica)

    }

    crtaj(tablica, tbody){

        tbody = cleanTable(tablica, tbody)
        //console.log( this.timid)
        fetch("https://localhost:5001/Utakmica/UtakmiceTimovi/" + this.timid).then(vt => {
        vt.json().then(utakmice=>{
            //console.log(utakmice)

                    utakmice.forEach(utakmica => {
                    let body_row = document.createElement("tr")
                    tbody.appendChild(body_row)
                    let lista = []
                    utakmica.timovi.forEach(
                        p => lista.push(p.naziv)
                    ) 
                    lista.push(utakmica.info)
                    lista.forEach(p => dodajbodykolona(body_row, p))
                }
            )
        })
    })
    }
}
