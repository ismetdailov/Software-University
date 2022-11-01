let {expect, assert} = require('chai')
let {chooseYourCar} = require("../ChooseYouCarResources/chooseYourCar.js");

describe("chooseYourCar", ()=> {
    describe("chooseYourCar- choosingType ", ()=> {
        it("choosingType should return error when year is less 1900", ()=> {
            assert.throw(()=>chooseYourCar.choosingType('Sedan','red',1899), `Invalid Year!`)
        });
        it("choosingType should return error when year is 0", ()=> {
            assert.throw(()=>chooseYourCar.choosingType('Sedan','red',0), `Invalid Year!`)
        });
        it("choosingType should return error when year is 1900", ()=> {
            assert.throw(()=>chooseYourCar.choosingType('Sedan','red',2023), `Invalid Year!`)
        });
        it("choosingType should return error when type is not Sedan", ()=> {
            assert.throw(()=>chooseYourCar.choosingType('Sen','red',2022), `This type of car is not what you are looking for.`)
        });
        it("choosingType should return error when type is Sedan", ()=> {
            assert.equal(chooseYourCar.choosingType('Sedan','red',2009), `This Sedan is too old for you, especially with that red color.`)
        });
        it("choosingType should return error when year is 2010 or highest", ()=> {
            assert.equal(chooseYourCar.choosingType('Sedan','red',2010), `This red Sedan meets the requirements, that you have.`)
        });
        it("choosingType should return error when year is 2022", ()=> {
            assert.equal(chooseYourCar.choosingType('Sedan','red',2022), `This red Sedan meets the requirements, that you have.`)
        });
     });
     describe("brandName", ()=> {
        it("brandName when is not arr", ()=> {
            assert.throw(()=>chooseYourCar.brandName('Sedan',20), "Invalid Information!")
        });
        it("brandName when is not IS not integer", ()=> {
            assert.throw(()=>chooseYourCar.brandName(["Sedan","bMW"],'integer'), "Invalid Information!")
        });
        it("brandName when is not IS less 0", ()=> {
            assert.throw(()=>chooseYourCar.brandName(["Sedan","bMW"],-5), "Invalid Information!")
        });
        it("brandName when index is bigger than arr lenght", ()=> {
            assert.throw(()=>chooseYourCar.brandName(["Sedan","bMW"],4), "Invalid Information!")
        });
        it("brandName when parameters is corrext filled", ()=> {
            assert.equal(chooseYourCar.brandName(["Sedan","bMW","Mercedes","opel"],3), "Sedan, bMW, Mercedes")
        });
    });
    describe("carFuelConsumption", ()=> {
        it("carFuelConsumption when first parametur is string", ()=> {
            assert.throw(()=>chooseYourCar.carFuelConsumption('4',5), "Invalid Information!")
        });
        it("carFuelConsumption when second parametur is string", ()=> {
            assert.throw(()=>chooseYourCar.carFuelConsumption(5,'4'), "Invalid Information!")
        });
        it("carFuelConsumption when first parametur is 0", ()=> {
            assert.throw(()=>chooseYourCar.carFuelConsumption(0,5), "Invalid Information!")
        });
        it("carFuelConsumption when first parametur is less 0 ", ()=> {
            assert.throw(()=>chooseYourCar.carFuelConsumption(-2,5), "Invalid Information!")
        });
        it("carFuelConsumption when second parametur is 0", ()=> {
            assert.throw(()=>chooseYourCar.carFuelConsumption(5,0), "Invalid Information!")
        });
        it("carFuelConsumption when second parametur is less 0", ()=> {
            assert.throw(()=>chooseYourCar.carFuelConsumption(5,-20), "Invalid Information!")
        });
        it("carFuelConsumption when parameturs is correct", ()=> {
            assert.equal(chooseYourCar.carFuelConsumption(100,6), "The car is efficient enough, it burns 6.00 liters/100 km.")
        });
        it("carFuelConsumption when parameturs is correct when is 7 ", ()=> {
            assert.equal(chooseYourCar.carFuelConsumption(100,7), "The car is efficient enough, it burns 7.00 liters/100 km.")
        });
        it("carFuelConsumption when parameturs is correct when is 8 ", ()=> {
            assert.equal(chooseYourCar.carFuelConsumption(100,8), "The car burns too much fuel - 8.00 liters!")
        });
    });
});
