class Garden {
	constructor(spaceAvaailable){
        this.spaceAvaailable = spaceAvaailable;
        this.plants = [];
        this.storage = [];
    }
    addPlant (plantName, spaceRequired){
        if (this.spaceAvaailable<spaceRequired) {
            throw new Error('Not enough space in the garden.')
        }
        let garden = {
            plantName: plantName,
            spaceRequired:spaceRequired,
            ripe: false,
            quantity:0
        }
        this.plants.push(garden);
        this.spaceAvaailable -= spaceRequired;
        return `The ${plantName} has been successfully planted in the garden.`
    }
    ripenPlant(plantName, quantity){
        let plant = this.plants.find(x=>x.plantName === plantName)
        if (!plant) {
            throw new Error(`There is no ${plantName} in the garden.`)
        }
        if (plant.ripe === true) {
            throw new Error(`The ${plantName} is already ripe.`)
        }if (quantity<=0) {
            throw new Error(`The quantity cannot be zero or negative.`)
        }
        plant.ripe = true;
        plant.quantity += quantity;
        if (plant.quantity === 1) {
            return `${quantity} ${plantName} has successfully ripened.`
        }
        else{
            return `${quantity} ${plantName}s have successfully ripened.`
        }
    }
    harvestPlant(plantName) {
        let plant = this.plants.find(x=>x.plantName === plantName)
        if (!plant) {
            throw new Error(`There is no ${plantName} in the garden.`)
        }if (plant.ripe === false) {
            throw new Error(`The ${plantName} cannot be harvested before it is ripe.`)
        }
        this.storage.push(plant)
        this.spaceAvaailable += plant.spaceRequired;
        let index= this.plants.findIndex(x=>x.plantName===plantName)
     let removedIndex = this.plants.splice(index,1)

        return `The ${plantName} has been successfully harvested.`
    }
    generateReport(){
        let space = 0
        let plantNames = []
        this.plants.forEach((x)=>{
            plantNames.push(x.plantName)
            space += x.quantity;
        })
        let arr = []

        this.storage.forEach(e =>{
            arr.push(`${e.plantName} (${e.quantity})`)
        })
        plantNames = plantNames.sort();
        return `The garden has ${this.spaceAvaailable} free space left.\n Plants in the garden: ${plantNames.join(", ")}\n ${this.storage<=0 ? `Plants in storage: The storage is empty.`: `Plants in storage: ${arr.join(", ")}`}
        `;
    }
}
// const myGarden = new Garden(250)
// console.log(myGarden.addPlant('apple', 20));
// console.log(myGarden.addPlant('orange', 200));
// console.log(myGarden.addPlant('olive', 50));
// const myGarden = new Garden(250)
// console.log(myGarden.addPlant('apple', 20));
// console.log(myGarden.addPlant('orange', 100));
// console.log(myGarden.addPlant('cucumber', 30));
// console.log(myGarden.ripenPlant('apple', 10));
// console.log(myGarden.ripenPlant('orange', 1));
// console.log(myGarden.ripenPlant('orange', 4));
// const myGarden = new Garden(250)
// console.log(myGarden.addPlant('apple', 20));
// console.log(myGarden.addPlant('orange', 200));
// console.log(myGarden.addPlant('raspberry', 10));
// console.log(myGarden.ripenPlant('apple', 10));
// console.log(myGarden.ripenPlant('orange', 1));
// console.log(myGarden.harvestPlant('apple'));
// console.log(myGarden.harvestPlant('olive'));
const myGarden = new Garden(250)
console.log(myGarden.addPlant('apple', 20));
console.log(myGarden.addPlant('orange', 200));
console.log(myGarden.addPlant('raspberry', 10));
console.log(myGarden.ripenPlant('apple', 10));
console.log(myGarden.ripenPlant('orange', 1));
console.log(myGarden.harvestPlant('orange'));
console.log(myGarden.generateReport());
