class Restaurant {
    constructor(budget) {
        this.budgetMoney = budget;
        this.menu = {};
        this.stockProducts = {};
        this.history = [];
    }

    loadProducts(products) {

        for (const element of products) {

            let isAdded = false;

            let product = {
                pName: element.split(' ')[0],
                quantity: element.split(' ')[1],
                totalPrice: element.split(' ')[2]
            };

            if (product.totalPrice <= this.budgetMoney) {
                if (this.stockProducts[product.pName] != undefined) {
                    this.stockProducts[product.pName] += product.quantity;
                    this.budgetMoney -= product.totalPrice;
                } else {
                    this.stockProducts[product.pName] = product.quantity;
                    this.budgetMoney -= product.totalPrice;
                }
                isAdded = true;
            }

            this.history.push(isAdded ? `Successfully loaded ${product.quantity} ${product.pName}` : `There was not enough money to load ${product.quantity} ${product.pName}`);
        }

        return this.history.join('\n');
    }

    addToMenu(mealName, neededProducts, price) {

        let meal = {
            mName: mealName,
            mProducts: [],
            mPrice: price
        }

        for (const item of neededProducts) {
            let product = {
                pName: item.split(' ')[0],
                pQuantity: item.split(' ')[1]
            };
            meal.mProducts.push(product);
        }

        if (this.menu[meal.mName] == undefined) {
            this.menu[meal.mName] = meal;
            let mealsCount = Object.keys(this.menu).length;
            return mealsCount == 1 ? `Great idea! Now with the ${mealName} we have 1 meal in the menu, other ideas?` : `Great idea! Now with the ${mealName} we have ${mealsCount} meals in the menu, other ideas?`
        } else {
            return `The ${mealName} is already in the our menu, try something different.`
        }

    }

    showTheMenu() {
        let mealsCount = Object.keys(this.menu).length;
        let result = [];
        if (mealsCount > 0) {
            for (const key in this.menu) {
                const element = this.menu[key];
                result.push(`${element.mName} - $ ${element.mPrice}`)
            }
            return result.join('\n');
        } else {
            return `Our menu is not ready yet, please come later...`;
        }
    }

    makeTheOrder(mealName) {
        let meal = this.menu[mealName];
        if (meal == undefined) {
            return `There is not ${meal} yet in our menu, do you want to order something else?`
        } else {
            let price = this.menu[mealName].mPrice;
            let neddedProducts = meal.mProducts;
            let hasAllProducts = true;
            for (const product of neddedProducts) {
                let name = product.pName;
                let quantity = product.pQuantity;
                if (this.stockProducts[name] == undefined || this.stockProducts[name] < quantity) {
                    hasAllProducts = false;
                    break;
                } else {
                    this.stockProducts[name] -= quantity;
                    this.budgetMoney += price;
                }
            }

            if (!hasAllProducts) {
                return `For the time being, we cannot complete your order (${mealName}), we are very sorry...`;
            } else {
                return `Your order (${mealName}) will be completed in the next 30 minutes and will cost you ${price}.`
            }
        }
    }
}
let kitchen = new Restaurant(1000);
console.log(kitchen.addToMenu('frozenYogurt', ['Yogurt 1', 'Honey 1', 'Banana 1', 'Strawberries 10'], 9.99));
console.log(kitchen.addToMenu('Pizza', ['Flour 0.5', 'Oil 0.2', 'Yeast 0.5', 'Salt 0.1', 'Sugar 0.1', 'Tomato sauce 0.5', 'Pepperoni 1', 'Cheese 1.5'], 15.55));

//let kitchen = new Restaurant(1000);
console.log(kitchen.showTheMenu());
