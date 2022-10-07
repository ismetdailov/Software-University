function solve() {
  const reviewList = document.querySelector('#review-list')
  const publishButton = document.querySelector('#publish-btn')
  publishButton.addEventListener('click',publish)
 
   //eventListener to clear button
   const clearButton = document.querySelector('#clear-btn')
   clearButton.type = "button"
   clearButton.addEventListener('click',clearPosts)
 
 
  function publish(){
    const liElement = document.createElement('li');
    liElement.className = 'rpost'
 
    //article creation
    const articleElement = document.createElement('article')
 
    //title creation and append
    const titleElement = document.createElement('h4')
    titleElement.textContent = document.querySelector('#post-title').value 
    articleElement.appendChild(titleElement)
 
    //category creation and append
    const categoryElement = document.createElement('p')
    categoryElement.textContent = `Category: ${document.querySelector('#post-category').value}`
    articleElement.appendChild(categoryElement)
 
    //content creation and append
    const content = document.createElement('p')
    content.textContent = `Content: ${document.querySelector('#post-content').value}`
    articleElement.appendChild(content) 
 
 
    //append article to liElement
    liElement.appendChild(articleElement)
 
    //create buttons
    const editButton = document.createElement('button')
    editButton.className = 'action-btn edit'
    editButton.textContent = "Edit"
 
 
    const approveButton = document.createElement('button')
    approveButton.className = 'action-btn approve'
    approveButton.textContent = "Approve"
 
    //append buttons to liElement
    liElement.appendChild(approveButton)
    liElement.appendChild(editButton)
 
 
    //append liElement to reviewList
    reviewList.appendChild(liElement)
 
 
    //clear input fields
    document.querySelector('#post-title').value = ""
    document.querySelector('#post-category').value = ""
    document.querySelector('#post-content').value = ""
 
    //eventListener to edit button
    editButton.addEventListener('click',edit)
 
    //eventListener to approve button
    approveButton.addEventListener('click',approve)
 
  }
 
 
  function edit(){
    //target parent LI
    const parentLi = event.target.parentElement
    //get header
    const headerText = parentLi.querySelector('h4').textContent
    //append header to input field
    document.querySelector('#post-title').value = headerText
 
 
    //get category
    let categoryText = parentLi.querySelector('p').textContent
    categoryText = categoryText.split("Category: ")
    const categoryWord = categoryText[1]
 
    //append category to input field
    document.querySelector('#post-category').value = categoryWord
 
 
    //get content
    let contentText = parentLi.querySelectorAll("p")[1].textContent
    contentText = contentText.split("Content: ")
    const contentWord = contentText[1]
 
    //append content to input field
    document.querySelector('#post-content').value = contentWord
 
    //remove LI
    parentLi.remove()
 
  }
 
  function approve(){
    //target parent LI
    const parentLi = event.target.parentElement
 
    //append to published
    document.querySelector('#published-list').append(parentLi)
 
    //remove buttons
    event.target.remove()
    parentLi.querySelector('button').remove()
 
 
  }
 
  function clearPosts(){
    //get ul
    const ul = document.querySelector('#published-list')
    let liElements = ul.querySelectorAll('li')
 
    //remove elements
    liElements.forEach(function(el){
      el.remove()
    })
  }
}