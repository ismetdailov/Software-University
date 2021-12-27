class Restaurant {
    constructor(budgetMoney) {
        this.budgetMoney = budgetMoney
        this.menu = {}
        this.stockProducts = {}
        this.history = []
    }
    loadProducts(arr) {
        arr.forEach(e => {


            let [productName, productQuantity, productTotalPrice] = e.split(' ')
            if (this.budgetMoney >= productTotalPrice) {
                if (!(productName in this.stockProducts)) {
                    this.stockProducts[productName] = 0
                }
                this.stockProducts[productName] += productQuantity
                this.budgetMoney -= productTotalPrice;
                this.history.push(`Successfully loaded ${productQuantity} ${productName}`)
            } else {
                this.history.push(`There was not enough money to load ${productQuantity} ${productName}`)
            }
        })
        return this.history.join('\n')
    }
    addToMenu(meal, arrNeedet, price) {
        if (meal in this.menu) {
            return `The ${meal} is already in the our menu, try something different.`
        } else {
            Object.assign(this.menu,{[meal]:[],[price]:price})
            
            if (!(meal in this.menu)) {
                
            }
            this.menu={
                [meal]:meal,
                products:[],
                price:price
            }
          

        }
        arrNeedet.forEach(element => {
            let [productName, productQuantity] = element.split(' ')
           
            this.menu.products.push({
                productName,
                productQuantity
            })
        });
        let tr = this.menu[meal]
        let mr = Object.keys(this.menu).values
        if (Object.values(this.menu).length/2===1) {
            return `Great idea! Now with the ${meal} we have 1 meal in the menu, other ideas?`
        } else {
            return `Great idea! Now with the ${meal} we have ${Object.values(this.menu).length/2} meals in the menu, other ideas?`
        }

    }
    showTheMenu(){
        let res=[]
        let name=''
        let price = 0

       // Object.values(this.menu).forEach(e=>e.some(e=>res.push(`${s.name} - $ ${e.price}`) ))
        Object.entries(this.menu).forEach(e=>name=e[0]&&Object.keys(e[1]).forEach(s=>res.push(`${name} - $ ${s[1]}`) ))
    }
}

let kitchen = new Restaurant(1000);
console.log(kitchen.addToMenu('frozenYogurt', ['Yogurt 1', 'Honey 1', 'Banana 1', 'Strawberries 10'], 9.99));
console.log(kitchen.addToMenu('Pizza', ['Flour 0.5', 'Oil 0.2', 'Yeast 0.5', 'Salt 0.1', 'Sugar 0.1', 'Tomato sauce 0.5', 'Pepperoni 1', 'Cheese 1.5'], 15.55));

//let kitchen = new Restaurant(1000);
console.log(kitchen.showTheMenu());
