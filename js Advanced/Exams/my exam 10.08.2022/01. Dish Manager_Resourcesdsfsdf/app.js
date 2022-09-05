window.addEventListener("load", solve);

function solve() {
  //TODO ....
  const input = {
    firstName: document.getElementById('first-name'),
    lastName: document.getElementById('last-name'),
    age: document.getElementById('age'),
    task: document.getElementById('task'),
    gender: document.getElementById('gender')
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
    if(firstName == '' || lastName == '' || age == '' || task == ''){
      return;
    }
    const li = document.createElement('li');
    li.className = 'each-line';
    li.innerHTML = `<article>
    <h4>${firstName} ${lastName}
    <p>${gender}, ${age}</p>
    <p>${task}
    </article>
    <button class="edit-btn">Edit</button>
    <button class="complete-btn>Mark as complete</button>"`

    const editBtn = li.querySelector('.edit-btn');
    const completeBtn = li.querySelector('.complete-btn');
    editBtn.addEventListener('click', edit)
    completeBtn.addEventListener('click', complete)

    status.inProgres.appendChild(li)

    input.firstName='';
    input.lastName = '';
    input.age = '';
    input.task = '';
    input.gender = '';

    function edit(){
      input.firstName=firstName;
      input.lastName =lastName; 
      input.age = age;
      input.task = task;
      input.gender = gender;
      li.remove();
    }
    function complete() {
      status.finished.appendChild(li);
      editBtn.remove();
      completeBtn.remove();
    }
    
  }
  function clear() {
    status.complete.innerHTML = '';
  }
  console.log(input),
    console.log(status)
    console.log(document.getElementById('form-btn').addEventListener('click', submit))

}
