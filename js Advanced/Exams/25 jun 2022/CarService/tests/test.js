let {expect, assert} = require('chai')
let {carService} = require("../03. Car Service_Resources");

describe("Car Service", function(){
    describe ("isItExpensive", ()=>{
        it("Check issue is Engine or Transmission", ()=>{
            assert.equal(carService.isItExpensive('Engine'), 'The issue with the car is more severe and it will cost more money')
            assert.equal(carService.isItExpensive('Transmission'), 'The issue with the car is more severe and it will cost more money')
        });
        it('it should return else'), ()=>{
            assert.equal(carService.isItExpensive('asd'), 'The overall price will be a bit cheaper')
        }
    })
    describe('Discount', ()=>{
        it('check numbers', ()=>{
            assert.throw(()=> carService.discount("ret",3),'Invalid input')
        })
        it('percentage',()=>{
            assert.equal(carService.discount(5,5), `Discount applied! You saved ${0.75}$`)
            assert.equal(carService.discount(1,1), `You cannot apply a discount`)
        })
        describe('partsToBuy',()=>{
            it('partsToBuy',()=>{
                assert.equal(carService.partsToBuy(
                [
                    {part: "some", price: 13},
                    {part: "someElse", price:14}
                ],
                ["some", "injectors"]
                ),13)
                
            })
        })
    })
})