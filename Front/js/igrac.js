import { dodajbodykolona, dodajhederkolona, tabela, cleanTable, dodajdugme, obrisiTablicu, pocetnaformica, dodajInput, dodajInputBroj} from "./funkcije.js";

export class Igrac{

    constructor(timid, nazivtima){
        this.timid = timid
        this.nazivtima = nazivtima
    }
    

    crtajFormuIgrac(forma){
        let inJMBG = dodajInput(forma, "inJMBG", "inJMBG", "JMBG")
        let inIme = dodajInput(forma, "inIme", "inIme", "Ime")
        let inPrezime = dodajInput(forma, "inPrezime", "inPrezime", "Prezime")
        let inGodina = dodajInputBroj(forma, "inGodina", "inGodina", "Broj godina")
        // let inTim = dodajInput(forma, "inTim", "inTim", "Tim")
        // inTim.disabled = true;
        let inPozicija = document.querySelector(".inPozicija")        
        obrisiTablicu(inPozicija)
        inPozicija =  document.createElement("select")
        inPozicija.className = "inPozicija"

        fetch("https://localhost:5001/Pozicija/VratiPozicije").then(p => {
            p.json().then(pozicije => {
                pozicije.forEach(pozicija => {
                    let option = document.createElement("option")
                    option.value = pozicija.id
                    option.innerHTML = pozicija.naziv
                    inPozicija.appendChild(option)
                })       
            })
        })

        forma.appendChild(inPozicija)

    }
    crtajIgrace(kont){


        let prikazIgraca = kont.querySelector(".prikazIgraca")

        const imeForme = "Igrac"
        let dugmeNoviIgrac = prikazIgraca.querySelector(".NoviIgrac")
        obrisiTablicu(dugmeNoviIgrac)
        dugmeNoviIgrac = pocetnaformica(prikazIgraca, imeForme, "NOVI IGRAC")
        let forma = prikazIgraca.querySelector(".Forma"+imeForme)

        dugmeNoviIgrac.onclick = () =>{
    
            this.crtajFormuIgrac(forma)

            let ocisti = forma.querySelectorAll("input")
            ocisti.forEach(element => {
                element.value = ""
            });            

            let dugmeDodajIgraca = forma.querySelector(".dodajIgraca")
            obrisiTablicu(dugmeDodajIgraca)

            dugmeDodajIgraca = document.createElement("button")
            dugmeDodajIgraca.className = "btn btn-primary"
            dugmeDodajIgraca.classList.add("dodajIgraca")
            dugmeDodajIgraca.innerHTML = "Dodaj igraca"
            forma.appendChild(dugmeDodajIgraca)
            dugmeDodajIgraca.onclick = () =>{


                let JMBG = (kont.querySelector(".inJMBG").value)
                let ime = (kont.querySelector(".inIme").value)
                let prezime = (kont.querySelector(".inPrezime").value)
                let godina = (kont.querySelector(".inGodina").value)
                let pozicija =(kont.querySelector("select.inPozicija").value)
                
                
                if(JMBG == "" || ime == "" || prezime =="" || godina =="" || pozicija ==""){
                    alert("UNSETE SVE PODATKE!!!")
                    return
                }


                fetch("https://localhost:5001/Igrac/DodajIgraca/" + JMBG + "/" + ime + "/" + prezime + "/" + godina +"/" + pozicija +"/"+this.timid,
                        {
                            method: "POST",
                            headers: {
                                "Content-Type": "application/json"
                            },
                        }).then(i => {
                            if (i.ok) {
                                alert("Uspresno dodat igraca!")
                                this.crtajIgrace(kont, tablica, tbody)
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

        
        let tablica = tabela(prikazIgraca, "Igraci", "igraciBody", "igraciHead")
        let thead = tablica.querySelector(".igraciHead")
    
        let header_row = document.createElement("tr")
        let tbody = tablica.querySelector(".igraciBody")
        //console.log(tbody)

        thead.appendChild(header_row)
        const lista = ["JMBG", "Ime", "Prezime", "Godine", "Pozicija" ]
    
        lista.forEach(p => dodajhederkolona(header_row, p))
        this.crtaj(kont,tablica, tbody, forma)
    }

    crtaj(kont,tablica, tbody, forma){

        tbody = cleanTable(tablica, tbody)

        fetch("https://localhost:5001/Igrac/VratiIgrace/" + this.timid).then(vt => {
        vt.json().then(igraci=>{

                    igraci.forEach(igrac => {
                    console.log(igrac);
                    let body_row = document.createElement("tr")
                    tbody.appendChild(body_row)
                    const lista = [igrac.jmbg, igrac.ime, igrac.prezime,igrac.godina,igrac.poz]
                    lista.forEach(p => dodajbodykolona(body_row, p))

                    let btnObrisi = dodajdugme(body_row, "Obrisi")
                    btnObrisi.onclick = ev => {

                        fetch("https://localhost:5001/Igrac/IzbrisiIgraca/" + igrac.id, {
                            method: "DELETE"
                        }).then(i => {
                            if (i.ok) {
                                this.crtajIgrace(kont)
                                alert("Uspesno obrisan igrac!");
                            }
                            else {
                                alert("Doslo je do greske!");
                            }
                        });
                    }

                    let btnAzuriraj = dodajdugme(body_row, "Azriraj")
                    btnAzuriraj.onclick = ev => {
                        
                        this.crtajFormuIgrac(forma)

                        kont.querySelector(".inJMBG").value = igrac.jmbg
                        kont.querySelector(".inIme").value = igrac.ime
                        kont.querySelector(".inPrezime").value = igrac.prezime
                        kont.querySelector(".inGodina").value = igrac.godina
                        kont.querySelector("select.inPozicija").value = igrac.idPoz
                       
                        let dugme = forma.querySelector(".dodajIgraca")

                        obrisiTablicu(dugme)
            
                        dugme = document.createElement("button")
                        dugme.className = "btn btn-primary"
                        dugme.classList.add("dodajIgraca")
                        dugme.innerHTML = "Azuriraj igraca"
                        forma.appendChild(dugme)
                        dugme.onclick = () =>{
            
                            let JMBG = (forma.querySelector(".inJMBG").value)
                            let ime = (forma.querySelector(".inIme").value)
                            let prezime = (forma.querySelector(".inPrezime").value)
                            let godina = (forma.querySelector(".inGodina").value)
                            let pozicija =(forma.querySelector("select.inPozicija").value)
                            
                            
                            if(JMBG == "" || ime == "" || prezime =="" || godina =="" || pozicija ==""){
                                alert("UNSETE SVE PODATKE!!!")
                                return
                            }
                            fetch("https://localhost:5001/Igrac/AzurirajIgraca/"+ igrac.id + "/" + ime +"/"+ prezime +"/" + godina + "/"  + pozicija,
                                    {
                                        method: "PUT",
                                        headers: {
                                            "Content-Type": "application/json"
                                        },
                                    }).then(i => {
                                        if (i.ok) {
                                            alert("Uspresno azuriran igraca!")
                                            this.crtajIgrace(kont, tablica, tbody, forma)
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