class CarDealership {
    constructor(name) {
        this.name = name;
        this.availableCars = [];
        this.soldCars = [];
        this.totalIncome = 0;
    }
    addCar(model, horsepower, price, mileage) {
        if (model === undefined || horsepower < 0 || horsepower === undefined ||
            price < 0 || price === undefined || mileage < 0 || mileage === undefined ||
            model ==='') {
            throw new Error('Invalid input!');
        }
        this.availableCars.push({ model: model, horsepower: horsepower, price: price, mileage: mileage })
        return `New car added: ${model} - ${horsepower} HP - ${mileage.toFixed(2)} km - ${price.toFixed(2)}$`;

    }
    sellCar(model, desiredMileage) {

        let car = this.availableCars.find(x => x.model === model);
        let difference = desiredMileage - car.mileage;
        let index = this.availableCars.findIndex(x => x.model === model)

        if (car === undefined) {
            throw new Error(`${model} was not found!`)
        }
        if (car.mileage <= desiredMileage) {

        } else {

            if (difference > 40000) {
                car.price -= car.price * 0.05;
            } if (difference <= 40000) {
                car.price -= car.price * 0.10;
            }
        }
        this.availableCars.splice(index, 1);
        this.soldCars.push({ model: car.model, horsepower: car.horsepower, soldPrice: car.price });
        this.totalIncome += car.price;
        return `${model} was sold for ${car.price.toFixed(2)}$`
    }
    currentCar() {
        let toReturn = `-Available cars:\n`
        if (this.availableCars.length > 0) {

            this.availableCars.forEach(e => {
                toReturn += `---${e.model} - ${e.horsepower} HP - ${e.mileage.toFixed(2)} km - ${e.price.toFixed(2)}$\n`
            })
        } else {
            return `There are no available cars`;
        }
        return toReturn;

    }
    salesReport(criteria) {
        let totalIncome = 0;
        this.soldCars.forEach(e=>{
            totalIncome += e.soldPrice;
        })
        if (criteria === 'horsepower') {
            
            this.soldCars.sort((a,b) =>(b.horsepower - a.horsepower) )
        }else if (criteria !== 'model') {
            this.soldCars.sort((a,b) =>(a.horsepower - b.horsepower) )
        }else{
            throw new Error('Invalid criteria!')
            
        }
        let toReturn = '';
        toReturn += `-${this.name} has a total income of ${totalIncome.toFixed(2)}$\n`
        toReturn+= `-${this.soldCars.length} cars sold:\n`
        this.soldCars.forEach(e =>{
            toReturn+=`---${e.model} - ${e.horsepower} HP - ${e.soldPrice.toFixed(2)}$\n`
        })
        return toReturn
        // objs.sort((a,b) => (a.last_nom > b.last_nom) ? 1 : ((b.last_nom > a.last_nom) ? -1 : 0))
        // this.soldCars.sort(x=>x.horsepower).localCompare((a,b)=>b-a);
    }
}
// let dealership = new CarDealership('SoftAuto');
// console.log(dealership.addCar('Toyota Corolla', 100, 3500, 190000));
// console.log(dealership.addCar('Mercedes C63', 300, 29000, 187000));
// console.log(dealership.addCar('', 120, 4900, 240000));

// let dealership = new CarDealership('SoftAuto');
// dealership.addCar('Toyota Corolla', 100, 3500, 190000);
// dealership.addCar('Mercedes C63', 300, 29000, 187000);
// dealership.addCar('Audi A3', 120, 4900, 240000);
// console.log(dealership.sellCar('Toyota Corolla', 230000));
// console.log(dealership.sellCar('Mercedes C63', 110000));

// let dealership = new CarDealership('SoftAuto');
// dealership.addCar('Toyota Corolla', 100, 3500, 190000);
// dealership.addCar('Mercedes C63', 300, 29000, 187000);
// dealership.addCar('Audi A3', 120, 4900, 240000);
// console.log(dealership.currentCar());

// let dealership = new CarDealership('SoftAuto');
// dealership.addCar('Toyota Corolla', 100, 3500, 190000);
// dealership.addCar('Mercedes C63', 300, 29000, 187000);
// dealership.addCar('Audi A3', 120, 4900, 240000);
// console.log(dealership.sellCar('Toyota Corolla', 230000));
// console.log(dealership.sellCar('Mercedes C63', 110000));

// let dealership = new CarDealership('SoftAuto');
// console.log(dealership.addCar('Toyota Corolla', 100, 3500, 190000));
// console.log(dealership.addCar('Mercedes C63', 300, 29000, 187000));
// console.log(dealership.addCar('', 120, 4900, 240000));

let dealership = new CarDealership('SoftAuto');
dealership.addCar('Toyota Corolla', 100, 3500, 190000);
dealership.addCar('Mercedes C63', 300, 29000, 187000);
dealership.addCar('Audi A3', 120, 4900, 240000);
dealership.sellCar('Toyota Corolla', 230000);
dealership.sellCar('Mercedes C63', 110000);
console.log(dealership.salesReport('horsepower'));

