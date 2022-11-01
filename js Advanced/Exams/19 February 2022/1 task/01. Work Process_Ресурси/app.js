function solve() {
    //TODO
    let firstName = document.getElementById('fname');
    let lastName = document.getElementById('lname');
    let email = document.getElementById('email')
    let birth = document.getElementById('birth')
    let position = document.getElementById('position')
    let salary = document.getElementById('salary')
    let budget = document.getElementById('sum')
    let budgetValue = 0;
    document.getElementById('add-worker').addEventListener('click', publick)

    function publick(e) {
        e.preventDefault();
        let firstNameValue = firstName.value

        let lastNameValue = lastName.value
        let emailValue = email.value
        let birthValue = birth.value
        let positionValue = position.value
        let salaryValue = Number(salary.value)

        if (firstNameValue === '' || lastNameValue === '' || emailValue === ''
            || emailValue === '' || birthValue === '' || positionValue === '' || salaryValue === '') {
            return;
        }
        let tableDiv = document.getElementById('tbody');
        let tr = document.createElement('tr')
        tr.innerHTML = `
                        <td>${firstNameValue}</td>
                        <td>${lastNameValue}</td>
                        <td>${emailValue}</td>
                        <td>${birthValue}</td>
                        <td>${positionValue}</td>
                        <td>${salaryValue}</td>
                        <td>${salaryValue}</td>
        
        `
        let td = document.createElement('td')
        let fired = document.createElement('button')
        let edit = document.createElement('button')
        fired.className = "fired"
        edit.className = "edit"
        fired.innerText = "Fired"
        edit.innerText = "Edit"
        td.appendChild(fired)
        td.appendChild(edit)
        tr.appendChild(td)
        tableDiv.appendChild(tr)
        firstName.value = '';
        lastName.value = '';
        email.value = '';
        birth.value = '';
        position.value = '';
        salary.value = '';
        budgetValue += salaryValue;
        budget.innerText = budgetValue.toFixed(2);

        edit.addEventListener('click', editFunc)

        function editFunc(e) {
            firstName.value = firstNameValue
            lastName.value = lastNameValue
            email.value = emailValue
            birth.value = birthValue
            position.value = positionValue
            salary.value = salaryValue

            budgetValue -=Number(e.currentTarget.parentNode.parentNode.children[6].innerText);
            budget.innerText = budgetValue.toFixed(2);
            e.currentTarget.parentNode.parentNode.remove()
        }
        fired.addEventListener('click', Fire)
        function Fire(e){
            e.currentTarget.parentNode.parentNode.remove()
            budgetValue -= salaryValue
            budget.innerText = budgetValue.toFixed(2)
        }
    }
}
solve()