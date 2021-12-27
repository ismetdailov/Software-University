window.addEventListener('load', solve);

function solve() {
    
    let addBtn = document.getElementById('add-btn').addEventListener('click' ,add)

    function add(e){
        e.preventDefault()
        let genre = document.getElementById('genre')
        let songName = document.getElementById('name')
        let author = document.getElementById('author')
        let date = document.getElementById('date')

        if (genre.value==''|| songName.value==''||author.value==''||date.value=='') {
            return
        }
        let container = document.querySelector('[class="all-hits-container"]')
        let div = document.createElement('div')
        div.setAttribute('class', 'hits-info')
        let img=document.createElement('img')
        img.setAttribute('src', './static/img/img.png')
        
        let h2genre = document.createElement('h2')
        h2genre.textContent = genre.value
        let h2name = document.createElement('h2')
        h2name.textContent = songName.value
        let h2author = document.createElement('h2')
        h2author.textContent = author.value
        let h2date = document.createElement('h2')
        h2date.textContent = date.value

        genre.value = ''
        songName.value=''
        author.value =''
        date.value ='' 

        let saveBtn = document.createElement('button')
        saveBtn.setAttribute('class', 'save-btn')
        saveBtn.textContent = 'Save song'
        let likeBtn = document.createElement('button')
        likeBtn.setAttribute('class', 'like-btn')
        likeBtn.textContent ='Like song'

        let deleteBtn = document.createElement('button')
        deleteBtn.setAttribute('class', 'delete-btn')
        deleteBtn.textContent = 'Delete'

        div.appendChild(img)
        div.appendChild(h2genre)
        div.appendChild(h2name)
        div.appendChild(h2author)
        div.appendChild(h2date)
        div.appendChild(saveBtn)
        div.appendChild(likeBtn)
        div.appendChild(deleteBtn)

        container.appendChild(div);
        
         genre.textContent = ''
         songName.textContent=''
         author.textContent =''
         date.textContent ='' 
        likeBtn.addEventListener('click', likeFunc)

        function likeFunc(e){
           let currLikes = document.querySelector('[class="likes"]').children[0]
           let likes = currLikes.textContent
           let [totaltex, likescount]=likes.split(':')
           likescount = Number(likescount)
           likescount++;
           let neshto = `${totaltex}: ${likescount}`
           currLikes.textContent = neshto
            e.currentTarget.disabled = true

        }
        saveBtn.addEventListener('click', (e)=>{
            e.currentTarget
            let parent =e.currentTarget.parentElement.parentElement
            let child =e.currentTarget.parentElement
            let savedContainer = document.querySelector('[class="saved-container"]')
            div.removeChild(saveBtn)
            div.removeChild(likeBtn)
            savedContainer.appendChild(div)

        })
        deleteBtn.addEventListener('click', (e)=>{
            let parent =e.currentTarget.parentElement.parentElement
            let child =e.currentTarget.parentElement
            parent.removeChild(child)
       
        })
    }
}