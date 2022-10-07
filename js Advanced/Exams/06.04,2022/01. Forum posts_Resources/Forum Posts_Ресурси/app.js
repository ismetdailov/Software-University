window.addEventListener("load", solve);

function solve() {
    let title = document.getElementById('post-title')
    let category = document.getElementById('post-category')
    let content = document.getElementById('post-content')
    document.getElementById('publish-btn').addEventListener('click', publish)
    document.getElementById('clear-btn').addEventListener('click', removeUl)

    function publish(e) {
        e.preventDefault();
        let titleValue = title.value;
        let categoryValue = category.value;
        let contentValue = content.value;

        if (titleValue === '' || categoryValue === '' || contentValue === '') {
            return;
        }

        let reviewList = document.getElementById('review-list')
        let li = document.createElement('li')
        li.className = 'rpost';
        li.innerHTML = `<article>
    <h4>${titleValue}</h4>
    <p>Category: ${categoryValue}</p>
    <p>Content: ${contentValue}</p>
    </article>
    `;
        let editButton = document.createElement('button');
        editButton.addEventListener('click', edit);
        let approveButton = document.createElement('button');
        approveButton.addEventListener('click', approve);
        editButton.className = 'action-btn edit'
        approveButton.className = 'action-btn approve'
        approveButton.textContent = 'Approve'
        editButton.textContent = 'Edit';

        li.appendChild(approveButton);
        li.appendChild(editButton)
        reviewList.appendChild(li);
        title.value = '';
        category.value = '';
        content.value = '';

        let upload = document.getElementById('published-list')
        function edit(e) {
            title.value = titleValue;
            category.value = categoryValue;
            content.value = contentValue;
            e.currentTarget.parentNode.remove();
        }
        function approve() {
            li.lastChild.remove()
            li.lastChild.remove()
            upload.appendChild(li)
        }
    }
    function removeUl() {
        upload.innerHTML = '';
    }
}
