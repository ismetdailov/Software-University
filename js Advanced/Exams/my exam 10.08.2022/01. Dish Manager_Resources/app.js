window.addEventListener("load", solve);

function solve() {
  //TODO ....
  const input = {
    firstName: document.getElementById('first-name'),
    lastName: document.getElementById('last-name'),
    age: document.getElementById('age'),
    task: document.getElementById('task'),
    gender: document.getElementById('genderSelect')
  }
  const status = {
    inProgres: document.getElementById('in-progress'),
    finished: document.getElementById('finished-wrapper')
  }
  document.getElementById('form-btn').addEventListener('click', submit)
  document.getElementById('clear-btn').addEventListener('click', clear)
  function submit(event) {
    event.preventDefault();
    const firstName = input.firstName.value;
    const lastName = input.lastName.value;
    const age = input.age.value;
    const task = input.task.value;
    const gender = input.gender.value;
    if(firstName == '' || lastName == '' || age == '' || task == '' || gender == ''){
      return;
    }
    const li = document.createElement('li');
    li.className = 'each-line';
    li.innerHTML = `<article>
    <h4>${firstName}&nbsp${lastName}
    <p>${gender},&nbsp${age}</p>
    <p>${task}
    </article>
    <button class="edit-btn">Edit</button>
    <button class="complete-btn">Mark as complete</button>`

    const editBtn = li.querySelector('.edit-btn');
    const completeBtn = li.querySelector('.complete-btn');
    editBtn.addEventListener('click', edit)
    completeBtn.addEventListener('click', complete)

    status.inProgres.appendChild(li)

    input.firstName.value='';
    input.lastName.value = '';
    input.age.value = '';
    input.task.value = '';
    // input.gender.value = '';

    function edit(){
      input.firstName.value=firstName;
      input.lastName.value =lastName; 
      input.age.value = age;
      input.task.value = task;
      input.gender.value = gender;
      li.remove();
    }
    function complete() {
      status.finished.appendChild(li);
      editBtn.remove();
      completeBtn.remove();
    }
    
  }
  function clear() {
    status.finished.innerHTML = '';
  }

}
