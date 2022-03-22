export function obrisiTablicu(element){

    if (element != null) { 
        let p = element.parentNode
        p.removeChild(element)
    }

}

export function tabela(kont,naziv, body, head){

    let tabelaDiv = kont.querySelector(".tabelaDiv")
    obrisiTablicu(tabelaDiv)
    tabelaDiv = document.createElement("div")
    tabelaDiv.className= "tabelaDiv"
    kont.appendChild(tabelaDiv)
    let tablica = tabelaDiv.querySelector("."+naziv)
    obrisiTablicu(tablica)
   
    tablica = document.createElement("table")

    tablica.className = "table table-dark"
    tablica.classList.add(naziv)
    
        tabelaDiv.appendChild(tablica)
        let thead = document.createElement("thead")
        thead.className = head
        let tbody = document.createElement("tbody")
        tbody.className = body
        tablica.appendChild(thead)
        tablica.appendChild(tbody)
        return tablica
    }

export function dodajhederkolona(heder, naziv){

    let header_column = document.createElement("th")
    header_column.innerHTML = naziv
    heder.appendChild(header_column)

}
export function dodajbodykolona(red, naziv){

    let body_column = document.createElement("td")
    body_column.innerHTML = naziv;
    red.appendChild(body_column)
}
export function cleanTable(tablica, tbody)
{
    console.log(tbody)
    let klas = tbody.className;
    tbody.remove()
    tbody = document.createElement("tbody")
    tbody.className = klas
    tablica.appendChild(tbody)
    return tbody
}
export function dodajdugme(red, naziv){

    let body_column = document.createElement("td")
    let btn = document.createElement("button")
    btn.innerHTML = naziv
    btn.className = "btn btn-primary"
    body_column.appendChild(btn)
    red.appendChild(body_column)
    return btn
}

export function pocetnaformica(prikaz,imeForme, dugmeTekst){
    
    let forma = prikaz.querySelector(".Forma"+imeForme)
    obrisiTablicu(forma)
    forma = document.createElement("div")
    forma.className = "Forma"+imeForme
    prikaz.appendChild(forma)

    // let hTimovi = forma.querySelector(".heder")
    // obrisiTablicu(hTimovi)
    // hTimovi = document.createElement("h4")
    // hTimovi.className = "heder"
    // hTimovi.innerHTML = imeForme
    // forma.appendChild(hTimovi)

    let dugme = forma.querySelector(".Novi"+imeForme)
    obrisiTablicu(dugme)
    dugme = document.createElement("button")
    dugme.className = "btn btn-primary"
    dugme.classList.add("Novi"+imeForme)
    dugme.innerHTML = dugmeTekst
    forma.appendChild(dugme)
    return dugme
}
export function dodajInput(forma, ime, className, placeholder){
    let el = forma.querySelector("."+ime)
    obrisiTablicu(el)

    el = document.createElement("input")
    el.className = className
    el.type = "text"
    el.placeholder = placeholder
    forma.appendChild(el)
    return el
}
export function dodajInputBroj(forma, ime, className, placeholder){
    let el = forma.querySelector("."+ime)
    obrisiTablicu(el)

    el = document.createElement("input")
    el.className = className
    el.type = "number"
    el.placeholder = placeholder
    forma.appendChild(el)
    return el
}

