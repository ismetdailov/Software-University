window.addEventListener('load', solve);

function solve() {

    let addBtn = document.getElementById('add').addEventListener('click', addFurniture)
    let priceEl = []
    function addFurniture(e) {
        e.preventDefault();
        let model = document.getElementById('model')
        let year = document.getElementById('year')
        let description = document.getElementById('description')
        let price = document.getElementById('price')


        if (model.value === '' || description.value === '') {
            return
        }
        if (!Number(year.value) || !Number(price.value) || Number(year.value) <= 0 || Number(price.value) <= 0) {
            return
        }

        let modelTd = document.getElementById('furniture-list')
        let tr = document.createElement('tr')
        tr.setAttribute('class', 'info')
        let td = document.createElement('td')
        td.textContent = model.value
        let tdprice = document.createElement('td')
        tdprice.textContent = Number(price.value).toFixed(2)

        let tdB = document.createElement('td')
        let tdBut = document.createElement('button')
        tdBut.setAttribute('class', 'moreBtn')
        tdBut.textContent = 'More Info';

        let tdBut1 = document.createElement('button')
        tdBut1.setAttribute('class', 'buyBtn')
        tdBut1.textContent = 'Buy it';

        let trHide = document.createElement('tr')
        trHide.setAttribute('class', 'hide')
        let tdHide = document.createElement('td')
        tdHide.textContent = `Year: ${Number(year.value)}`
        let tdColspand = document.createElement('td')
        tdColspand.setAttribute('colspan', '3')
        tdColspand.textContent = `Description: ${description.value}`;
        let sprice = Number(price.value).toFixed(2)
        priceEl.push(sprice)

        trHide.appendChild(tdHide)
        trHide.appendChild(tdColspand)

        tdB.appendChild(tdBut)
        tdB.appendChild(tdBut1)

        tr.appendChild(td)
        tr.appendChild(tdprice)

        tr.appendChild(tdB)

        modelTd.appendChild(tr)
        modelTd.appendChild(trHide)


        let moreBtn = tr.querySelectorAll('button')
        moreBtn[0].addEventListener('click', showMoreInfo)
        moreBtn[1].addEventListener('click', showMoreInfo)
        model.innerText = '';
        year.innerText = '';
        description.innerText = '';
        price.innerText = '';
    }
    function showMoreInfo(e) {

        let bum = e.currentTarget.parentElement.parentElement
        let curr = e.currentTarget
        let tr = e.currentTarget.parentElement.parentElement.nextElementSibling
        if (curr.textContent === 'More Info') {
            curr.textContent = 'Less Info'
            tr.style.display = 'contents'


        } else if (curr.textContent === 'Less Info') {
            curr.textContent = 'More Info'
            tr.style.display = 'none'

        }

        else if (curr.textContent === 'Buy it') {
            let total = document.querySelector('[class="total-price"]')
            let child = bum.children[1]
            let sum = Number(total.textContent)
            sum += Number(child.textContent)
            let bbum = e.currentTarget.parentElement.parentElement
            total.textContent = sum
            bum.parentElement.removeChild(bbum)
        }

    }

}
