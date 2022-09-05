window.addEventListener('load', solve);

function solve() {
     let addBtn = document.getElementById("add-btn")
     addBtn.addEventListener('click', add)
    function add(e){
        e.preventDefault()
        let genre = document.getElementById("genre");
        let name = document.getElementById("name");
        let author = document.getElementById("author");
        let date = document.getElementById("date");
        let sectionDiv = document.querySelector(".all-hits-container");

        let div = document.createElement('div');
        div.setAttribute('class', 'hits-info')
        let img = document.createElement("img")
        let h2 = document.createElement("h2")
        let h21 = document.createElement("h2")
        let h22 = document.createElement("h2")
        let h3 = document.createElement("h3")
        let saveBtn = document.createElement("button")
        saveBtn.setAttribute('class', "save-btn")
        let likeBtn = document.createElement("button")
        likeBtn.setAttribute('class', "like-btn")
        let deleteBtn = document.createElement("button")
        deleteBtn.setAttribute('class', "delete-btn")
        if(genre.value!==''&&  name.value!==''&&author.value !==''&& date.value!=='' ){
            img.setAttribute('src', './static/img/img.png');
            h2.innerText = 'Genre:'+'' + genre.value;
            h21.innerText = 'Name:'+''+ name.value;
            h22.innerText = 'Author:'+''+ author.value;
            h3.innerText = 'Date:'+'' + date.value;
            saveBtn.innerText = 'Save song';
            likeBtn.innerText = 'Like song';
            deleteBtn.innerText = 'Delete';
            div.appendChild(img);
            div.appendChild(h2);
            div.appendChild(h21);
            div.appendChild(h22);
            div.appendChild(h3);
            div.appendChild(saveBtn);
            div.appendChild(likeBtn);
            div.appendChild(deleteBtn);
            sectionDiv.appendChild(div);
            
            genre.value ="";
            name.value = '';
            date.value = '';
            author.value = '';
        }
            
         likeBtn.addEventListener('click', click)
         function click(){
             if(likeBtn.disabled == false){
                let likes = document.querySelectorAll('p')[1];
                let value = likes.innerText.split(': ')
                let text = value[0];
                let val = Number(value[1])
                val ++;
                 likes.innerText= text+ ': ' +val;
                 likeBtn.disabled = true;
                }
                else{
                    likeBtn.disabled == false;
                    likes.value -=1;
                }
         }
         let save = document.querySelector('.saved-container');
         saveBtn.addEventListener('click', saveSong)
         function saveSong () {
            div.removeChild(saveBtn);
            div.removeChild(likeBtn);
            save.appendChild(div)

         }
         deleteBtn.addEventListener('click', (e)=>{
             var parent = e.target.parentElement.parentElement
             var child = e.target.parentElement;
            parent.removeChild(child);
            //  sectionDiv.remove(e.parentElement)
         });

    }
}