class  VegetableStore {
    constructor(owner,location){
        this.owner = owner;
        this.location= location;
        this.availableProducts =[]
    }
    loadingVegetables (vegetables){
       const arr = vegetables.split(' ')
    }
}





let vegStore = new VegetableStore("Jerrie Munro", "1463 Pette Kyosheta, Sofia");
 console.log(vegStore.loadingVegetables(["Okra 2.5 3.5", "Beans 10 2.8", "Celery 5.5 2.2", "Celery 0.5 2.5"]));


