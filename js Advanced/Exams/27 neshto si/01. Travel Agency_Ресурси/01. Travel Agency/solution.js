window.addEventListener('load', solution);

function solution() {

  let submitBtn = document.getElementById('submitBTN').addEventListener('click', add)

  function add(e) {
    e.preventDefault()
    let fullName = document.getElementById('fname')
    let email = document.getElementById('email')
    let phoneNumber = document.getElementById('phone')
    let address = document.getElementById('address')
    let postalCode = document.getElementById('code')
    
    let preview = document.getElementById('infoPreview')

    if (email.value===''|| fullName ==='') {
      return
    }
    
    
    let liFullName = document.createElement('li')
    let liEmail = document.createElement('li')
    let liPhone = document.createElement('li')
    let liAddress = document.createElement('li')
    let liPosteCode = document.createElement('li')
    liFullName.textContent  = `Full name: ${fullName.value}` 
    liEmail.textContent =`Email: ${email.value}`
    liPhone.textContent =`Phone Number: ${phoneNumber.value}`  
    liAddress.textContent = `Address: ${address.value}`
    liPosteCode.textContent=`Postal Code: ${postalCode.value}` 
    
    preview.appendChild(liFullName)
    preview.appendChild(liEmail)
    preview.appendChild(liPhone)
    preview.appendChild(liAddress)
    preview.appendChild(liPosteCode)

    fullName.value =''
    email.value = ''
    phoneNumber.value = ''
    address.value = ''
    postalCode.value =''
    e.currentTarget.disabled= true
    let editB =document.getElementById('editBTN')
    editB.disabled= false
    let continueB = document.getElementById('continueBTN')
    continueB.disabled = false
    continueB.addEventListener('click',(e)=>{
      let asulParent = document.getElementById('block')
      let bab =asulParent.children
      let ch0 = bab[0]
      let ch1 = bab[1]
      let ch2 = bab[2]
      let ch3 = bab[3]
      let ch4 = bab[4]
      asulParent.removeChild(ch0)
      asulParent.removeChild(ch1)
      asulParent.removeChild(ch2)
      asulParent.removeChild(ch3)
      asulParent.removeChild(ch4)
     
      let h3 = document.createElement('h3')
      h3.textContent= "Thank you for your reservation!";
      asulParent.appendChild(h3)
    })
    let editBtn =document.getElementById('editBTN').addEventListener('click', (e)=>{
      
      let parent =e.currentTarget.parentElement.parentElement
      let par =parent.children[0]
      let params =par.children


      fullName.value = params[0].innerText.split(': ')[1]
      email.value = params[1].innerText.split(': ')[1]
      phoneNumber.value = params[2].innerText.split(': ')[1]
      address.value = params[3].innerText.split(': ')[1]
      postalCode.value = params[4].innerText.split(': ')[1]
      preview.removeChild(liFullName)
      preview.removeChild(liEmail)
      preview.removeChild(liPhone)
      preview.removeChild(liAddress)
      preview.removeChild(liPosteCode)

      let submitBtn = document.getElementById('submitBTN').disabled = false
      editB.disabled = true
      continueB.disabled= true

    })
  }
}
