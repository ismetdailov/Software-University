function solve() {

   let createBtn = document.querySelector('[class="btn create"]')
   createBtn.addEventListener('click', create)
   function create(e) {
      e.preventDefault()
      let auhtor = document.getElementById('creator')
      let title = document.getElementById('title')
      let category = document.getElementById('category')
      let content = document.getElementById('content')
      let archiveSection = document.querySelectorAll('main section')[0]

      if (auhtor.value === '' || title.value === '' || category.value === '' || content.value === '') {

      }
      let article = document.createElement('article')

      let h1 = document.createElement('h1')
      let p1 = document.createElement('p')
      let strong1 = document.createElement('strong')
      let p2 = document.createElement('p')
      let strong2 = document.createElement('strong')
      let p3 = document.createElement('p')

      let div = document.createElement('div')
      div.setAttribute('class', 'buttons')

      let deleteBtn = document.createElement('button')
      deleteBtn.setAttribute('class', 'btn delete')
      deleteBtn.textContent = 'Delete'

      let btnArchive = document.createElement('button')
      btnArchive.setAttribute('class', 'btn archive')
      btnArchive.textContent = 'Archive'

      h1.textContent = title.value
      p1.textContent = 'Category: '
      strong1.textContent = category.value
      p2.textContent = 'Creator: '
      strong2.textContent = auhtor.value
      p3.textContent = content.value

      div.appendChild(deleteBtn)
      div.appendChild(btnArchive)
      p2.appendChild(strong2)
      p1.appendChild(strong1)

      article.appendChild(h1)
      article.appendChild(p1)
      article.appendChild(p2)
      article.appendChild(p3)
      article.appendChild(div)
      archiveSection.appendChild(article)

      auhtor.value = ''
      title.value = ''
      category.value = ''
      content.value = ''

      btnArchive.addEventListener('click', (e) => {

         let archive = document.querySelector('[class="archive-section"]')
         if (archive.children.length >= 2) {
            let child = archive.children[1]
            let li = document.createElement('li')
            let charr=[]
            li.textContent = h1.textContent
            child.appendChild(li)
            archiveSection.removeChild(article)
            if (child.children.length>=2) {
               let ch1= child.children
               for (let index = 0; index <= ch1.length; index++) {
                  if (index>0) {
                     index--;
                     
                  }
                  const element = ch1[index];
                  charr.push(element)
                  child.removeChild(element)
                  
               }
               // for (const iterator of ch1.length) {
                  
                  //    charr.push(iterator)
                  //    child.removeChild(iterator)
                  // }
                  // let li = document.createElement('li')
                  charr.sort((a,b)=>a.textContent.localeCompare(b.textContent))
                  for (let index = 0; index < charr.length; index++) {
                     const element = charr[index];
                  child.appendChild(element)
               }
              /// charr.sort((a,b)=>a-b).forEach(e=>li.appendChild(e.textContent),child.appendChild(li))
              // let ch2 = child.children[1]
              
            }

         }

      })
      deleteBtn.addEventListener('click', (e) => {

         archiveSection.removeChild(article)

      })
   }

}
