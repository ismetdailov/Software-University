window.addEventListener("load", solve);

function solve() {

    const input = {

        makeInput: document.getElementById('make'),
        modelInput: document.getElementById('model'),
        yearInput: document.getElementById('year'),
        fuelType: document.getElementById('fuel'),
        originalCost: document.getElementById('original-cost'),
        sellingPrice: document.getElementById('selling-price'),
    };
    let totalProfit = 0;
    let table = document.getElementById('table-body');
    let tableSell = document.getElementById('cars-list');
    let profit = document.getElementById('profit');

    document.getElementById('publish').addEventListener('click', publish)

    function publish(event) {
        let modelInput = input.modelInput.value;
        let makeInput = input.makeInput.value;
        let yearInput = input.yearInput.value;
        let fuelType = input.fuelType.value;
        let originalCost = input.originalCost.value;
        let sellingPrice = input.sellingPrice.value;
        event.preventDefault();
        if (makeInput === '' || modelInput === '' || yearInput === '' || fuelType === '' ||
            originalCost === '' || sellingPrice === '' || originalCost >= sellingPrice) {
            return;
        }
        let tr = document.createElement('tr')
        tr.className = 'row'
        tr.innerHTML = `<td>${makeInput}</td>
                          <td>${modelInput}</td>
                          <td>${yearInput}</td>
                          <td>${fuelType}</td>
                          <td>${originalCost}</td>
                          <td>${sellingPrice}</td>
                        <td>
                        </td>`;
                let edidBtn = document.createElement('button')
                edidBtn.className = "action-btn edit"
                edidBtn.innerHTML = 'Edit'
                tr.lastChild.appendChild(edidBtn);
                edidBtn.addEventListener('click', edid)

                let dltBtn = document.createElement('button')
                dltBtn.className = "action-btn sell"
                dltBtn.innerHTML = 'Sell'
                tr.lastChild.appendChild(dltBtn);
                dltBtn.addEventListener('click', sell)
        table.appendChild(tr);
    //   let editBtn =   document.querySelector('.edit')
    //   editBtn.addEventListener('click', edid);
    //    let dltBtn =  document.querySelector('.sell')
    //    dltBtn.addEventListener('click',sell);
        input.makeInput.value = '';
        input.modelInput.value = '';
        input.yearInput.value = '';
        input.fuelType.value = '';
        input.originalCost.value = '';
        input.sellingPrice.value = '';
        function edid(event) {

            input.makeInput.value = makeInput;
            input.modelInput.value = modelInput;
            input.yearInput.value = yearInput;
            input.fuelType.value = fuelType;
            input.originalCost.value = originalCost;
            input.sellingPrice.value = sellingPrice;
            event.target.parentNode.parentNode.remove();
        }
        function sell(event) {
            event.target.parentNode.parentNode.remove();
            let li = document.createElement('li')
            li.className = 'each-list'
            li.innerHTML = `<span>${makeInput} ${modelInput}</span>
        <span>${yearInput}</span>
        <span>${sellingPrice - originalCost}`;
            tableSell.appendChild(li);
            let profit = document.getElementById('profit');
            let number = sellingPrice - originalCost
            totalProfit += number;
            profit.textContent = totalProfit.toFixed(2);
        }
    }
}
