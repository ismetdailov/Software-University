class Restaurant {
    constructor(budgetMoney) {
        this.budgetMoney = budgetMoney
        this.menu = {}
        this.stockProducts = {}
        this.history = []
    }
    loadProducts(arr) {

        arr.forEach(element => {
            let [productName, productQuantity, productTotalPrice] = element.split(' ');
            productQuantity = Number(productQuantity)
            if (this.budgetMoney >= productTotalPrice) {
                if (!(productName in this.stockProducts)) {
                    this.stockProducts[productName] = 0;
                }
                this.stockProducts[productName] += productQuantity
                this.budgetMoney -= productTotalPrice;
                this.history.push(`Successfully loaded ${productQuantity} ${productName}`)
            } else {
                this.history.push(`There was not enough money to load ${productQuantity} ${productName}`)
            }
        });

        return this.history.join('\n')
    }
    addToMenu(meal, arrNeedetProduct, priceProduct) {
        if (!(meal in this.menu)) {
            this.menu[meal] = []
            arrNeedetProduct.forEach(e => {
                let [productName, productQuantity] = e.split(' ')
                productQuantity = Number(productQuantity);
                this.menu[meal]
                this.menu[meal].push(productName, productQuantity)

            })
            this.menu[meal].value = priceProduct;
        } else {
            return `The ${meal} is already in the our menu, try something different.`
        } if (Object.keys(this.menu).length == 1) {
            return `Great idea! Now with the ${meal} we have 1 meal in the menu, other ideas?`
        } else {
            return `Great idea! Now with the ${meal} we have ${Object.keys(this.menu).length} meals in the menu, other ideas?`
        }
    }
    showTheMenu() {
        let menuObj = Object.keys(this.menu)
        if (menuObj.length == 0) {
            return "Our menu is not ready yet, please come later..."
        }
        let res = []
        Object.entries(this.menu).forEach(e => {
            let name = e[0]
            let price = e[1].value
            let re = `${name} - $ ${price}`
            res.push(re)
        })
        return res.join('\n')
    }
    makeTheOrder(meal) {
        
        let istr = Object.keys(this.menu).map(e => e == meal)
        if (!istr) {
            return `There is not ${meal} yet in our menu, do you want to order something else?`
        } else {
            let flag = true
            Object.entries(this.menu).forEach(i => {
                for (let index = 0; index < i[1].length; index++) {
               
                    if (index % 2 == 0) {
                        const product = i[1][index];
                        const quantity = i[1][index += 1];
                     

                        if (product in this.stockProducts) {
                            if (this.stockProducts[product] >= quantity) {
                                this.stockProducts[product] -= quantity;
                            }
                        } else {
                            flag = false
                            return `For the time being, we cannot complete your order (${meal}), we are very sorry...`
                            
                        }

                    }
                }
               
            })
            if (flag) {
                let prices = this.menu[meal].value
                return `Your order (${meal}) will be completed in the next 30 minutes and will cost you ${prices}.`
            } 
        }

    }
}
// let kitchen = new Restaurant(1000);
// console.log(kitchen.loadProducts(['Banana 10 5', 'Banana 20 10', 'Strawberries 50 30', 'Yogurt 10 10', 'Yogurt 500 1500', 'Honey 5 50']));
//let kitchen = new Restaurant(1000);
//console.log(kitchen.addToMenu('frozenYogurt', ['Yogurt 1', 'Honey 1', 'Banana 1', 'Strawberries 10'], 9.99));
//console.log(kitchen.addToMenu('Pizza', ['Flour 0.5', 'Oil 0.2', 'Yeast 0.5', 'Salt 0.1', 'Sugar 0.1', 'Tomato sauce 0.5', 'Pepperoni 1', 'Cheese 1.5'], 15.55));
//let kitchen = new Restaurant(1000);
////console.log(kitchen.showTheMenu());

let kitchen = new Restaurant(1000);
kitchen.loadProducts(['Yogurt 30 3', 'Honey 50 4', 'Strawberries 20 10', 'Banana 5 1']);
kitchen.addToMenu('frozenYogurt', ['Yogurt 1', 'Honey 1', 'Banana 1', 'Strawberries 10'], 9.99);
console.log(kitchen.makeTheOrder('frozenYogurt'));


