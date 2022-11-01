let {expect,assert} = require('chai');
let {rentCar} = require(".rentCar");
describe("Test class caar Service ", ()=>{
    describe("Test search car invalid output",()=>{
        it("It should return error invalid input", ()=>{
            assert.Throw(()=> rentCar.searchCar('search', 'search'),"Invalid input!")
            assert.Throw(()=> rentCar.searchCar('search', 9), 'Invalid input!')
        })
        it("It should return valid input", ()=>{
            assert.equal(rentCar.searchCar(['orizova', 'rizaata'],'kaloriq'),`There is 2 car of model kaloriq in the catalog!`)
            rentCar.searchCar()
            
        })
    })
})