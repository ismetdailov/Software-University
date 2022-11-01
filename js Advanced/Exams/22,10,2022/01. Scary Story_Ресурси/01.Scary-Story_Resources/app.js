window.addEventListener("load", solve);

function solve() {
    let firstName = document.getElementById('first-name')
    let lastName = document.getElementById('last-name')
    let age = document.getElementById('age')
    let genre = document.getElementById('genre')
    let storyTitle = document.getElementById('story-title')
    let story = document.getElementById('story')
    document.getElementById('form-btn').addEventListener('click', Publish)

    function Publish(e) {
        e.preventDefault();
        let firstNameValue = firstName.value
        let lastNameValue = lastName.value
        let ageValue = age.value
        let genreValue = genre.value
        let storyTitleValue = storyTitle.value
        let storyValue = story.value

        if (firstNameValue === '' || lastNameValue === '' || ageValue === '' 
        || genreValue === '' || storyTitleValue === '' || storyTitleValue === '' || storyValue === '') {
            return
        }
        let preview = document.getElementById('preview-list')
        let li = document.createElement('li')
        li.className = 'story-info';
        li.innerHTML = `
        <article>
        <h4>Name: ${firstNameValue} ${lastNameValue}</h4>
        <p>Age: ${ageValue}</p>
        <p>Title: ${storyTitleValue}</p>
        <p>Genre: ${genreValue}</p>
        <p>${storyValue}</p>
        </article>
        `
        let saveStoryBtn = document.createElement('button')
        let editStoryBtn = document.createElement('button')
        let deleteStoryBtn = document.createElement('button')
        saveStoryBtn.className = 'save-btn'
        editStoryBtn.className = 'edit-btn'
        deleteStoryBtn.className = 'delete-btn'
        saveStoryBtn.innerText = 'Save Story'
        editStoryBtn.innerText = 'Edit Story'
        deleteStoryBtn.innerText = 'Delete Story'
        editStoryBtn.disabled = false
        saveStoryBtn.disabled = false
        deleteStoryBtn.disabled = false
        li.appendChild(saveStoryBtn)
        li.appendChild(editStoryBtn)
        li.appendChild(deleteStoryBtn)
        preview.appendChild(li);
        firstName.value = '';
        lastName.value = '';
        age.value = '';
        genre.value = genre.children[0].value ;
        storyTitle.value = '';
        story.value = '';

        let publishBtn = document.getElementById('form-btn')
        publishBtn.disabled = true

        editStoryBtn.addEventListener('click', Edit)
        function Edit(e) {
            firstName.value = firstNameValue;
            lastName.value =lastNameValue ;
            age.value = ageValue;
            genre.value = genreValue;
            storyTitle.value =storyTitleValue ;
            story.value =storyValue ;
            saveStoryBtn.disabled= true
            editStoryBtn.disabled= true
            deleteStoryBtn.disabled= true
            publishBtn.disabled = false
            e.currentTarget.parentNode.remove()
        }
        saveStoryBtn.addEventListener('click', Save)
        function Save(){
            let main = document.getElementById('main')
            main.innerHTML = ''
            let h1 = document.createElement('h1')
            h1.innerText = 'Your scary story is saved!'
            main.appendChild(h1);
        }
        deleteStoryBtn.addEventListener('click', Delete)
        function Delete(e){
            e.currentTarget.parentNode.remove()
            publishBtn.disabled = false
        }
    }
}
