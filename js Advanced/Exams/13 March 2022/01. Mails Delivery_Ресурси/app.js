function solve() {
    let recipientName = document.getElementById('recipientName')
    let title = document.getElementById('title')
    let message = document.getElementById('message')
    document.getElementById('add').addEventListener('click', submit)
    document.getElementById('reset').addEventListener('click', reset)
    function reset(e) {
        e.preventDefault();
        recipientName.value = '';
        title.value = '';
        message.value = '';
    }
    function submit(e) {
        e.preventDefault();
        let recipiName = recipientName.value
        let titl = title.value
        let messag = message.value

        if (recipiName === '' || titl === '' || messag === '') {
            return;
        }

        let ul = document.getElementById('list')
        let li = document.createElement('li')
        let div = document.createElement('div')
        div.id = 'list-action';
        li.innerHTML = `<h4>Title: ${titl}</h4>
                        <h4>Recipient Name: ${recipiName}</h4>
                        <span>${messag}</span>
                        `;
        let sendBtn = document.createElement('button')
        sendBtn.type = 'submit'
        sendBtn.id = 'send'
        sendBtn.innerText = 'Send'
        let deleteBtn = document.createElement('button')
        deleteBtn.type = 'submit';
        deleteBtn.id = 'delete'
        deleteBtn.innerText = 'Delete'
        deleteBtn.addEventListener('click', deleteFunc)
        div.appendChild(sendBtn)
        div.appendChild(deleteBtn);

        li.appendChild(div)
        ul.appendChild(li)


        sendBtn.addEventListener('click', send)
        let ulList = document.querySelector('.sent-list')
        let liSend = document.createElement('li')
        // deleteBtn.addEventListener('click', deleteFunc)
        function send(e) {
            e.currentTarget.parentNode.parentNode.remove();
            liSend.innerHTML = `<span>To: ${recipiName}</span>
                            <span>Title: ${titl}</span>

                `;
            let div = document.createElement('div')
            div.className = 'btn';
            let button = document.createElement('button')
            button.type = 'submit'
            button.className = 'delete'
            button.textContent = 'Delete';
            button.addEventListener('click', deleteFunc)
            div.appendChild(button);
            liSend.appendChild(div);
            ulList.appendChild(liSend);
        }
        function deleteFunc(e) {
            let deleteLis = document.querySelector('.delete-list')
            // e.currentTarget.parentNode.remove(e.currentTarget)
            e.currentTarget.parentNode.parentNode.remove();
            let remElem = e.currentTarget.parentNode.parentNode;
            remElem.removeChild(remElem.lastChild)
            deleteLis.appendChild(remElem)
        }

    }
}
solve();