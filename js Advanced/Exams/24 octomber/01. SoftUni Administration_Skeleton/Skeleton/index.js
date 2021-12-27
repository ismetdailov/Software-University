function solve() {

    let addBtn = document.querySelector('button')
    addBtn.addEventListener('click', (e) => add(e))
    let h4aort = []
    let nesht = []
    function add(e) {
        e.preventDefault()
        let lectureName = document.querySelector('[name="lecture-name"').value
        let module = document.querySelector('[name="lecture-module"]').value
        module = module.toUpperCase()
        let dat = document.querySelector('[type="datetime-local"]').value
        let d = new Date(dat)
        let date = d.toLocaleString("en-US")
        let time = d.toLocaleTimeString()
        date = date.slice(0, 10)
        time = time.slice(0, 5)
        //
        if (lectureName == undefined && lectureName == '' && date == false && module == 'Select module') {
            return
        }
        if (nesht.includes(module) == false) {
            nesht.push(module)
            h4aort[module] = []
        }
        h4aort[module].push({
            lectureName,
            date,
            time
        })

        h4aort[module].sort((a, b) => new Date(a.date) - new Date(b.date))
        let modulesClass = document.querySelector('[class="modules"]')

        let count =0;
        let cou= 0;
      let elll=  Object.values(h4aort)//.forEach(e => {
          
           elll[0].forEach(s => {
                console.log(s)
                count++
                let name = s.lectureName
                let date = s.date
                let time = s.time
                let li = document.createElement('li')
                let modu = document.createElement('div')
                let butDel = document.createElement('button')
                modu.setAttribute('class', 'module')
                let h3 = document.createElement('h3')
                h3.textContent = module.toUpperCase();
                h3.textContent += ' MODULE'
                let neshto = modulesClass.childNodes.forEach(c=>{
                    c.childNodes.forEach(h =>{
                     let ccc= h.childNodes==h3
                     let b =ccc.innerText
                    })
                })
                let ul = document.createElement('ul')
                li.setAttribute('class', 'flex')
                let h4 = document.createElement('h4')
                butDel.setAttribute('class', 'red')
                butDel.textContent = 'Del'
                h4.textContent = `${name} - ${date} - ${time}\n`
                
                li.appendChild(h4)
                li.appendChild(butDel)
                ul.appendChild(li)
                if (count<=1) {
                    modu.appendChild(h3)
                }
                modu.appendChild(ul)
                modulesClass.appendChild(modu);

                butDel.addEventListener('click',e=>{
                   let mu= e.currentTarget.parentElement
                   let gu = e.target.parentElement.parentElement.parentElement
                    modulesClass.remove(mu)
                    if (gu.length<=0) {
                        modulesClass.remove(gu)
                    }
                    
                        
                    

                })
            })
            
        //})
      




    }

};