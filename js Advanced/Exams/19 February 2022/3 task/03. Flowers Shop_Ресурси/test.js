let {expect, assert} = require('chai')
let {flowerShop} = require("../03. Flowers Shop_Ресурси/flowerShop.js");

describe("FlowerShop", ()=>{
    describe("calcPriceOfFlowers", ()=>{
        it("calcPriceOfFlowers should throw Error when flower is not a string", ()=>{
            assert.Throw(()=> flowerShop.calcPriceOfFlowers(5,5,5), "Invalid input!")
        }),
         it("calcPriceOfFlowers should throw Error when flower is price is string ", ()=>{
            assert.Throw(()=> flowerShop.calcPriceOfFlowers("string",'str',5), "Invalid input!")
        }),
        it("calcPriceOfFlowers should throw Error when flower is quantity is string ", ()=>{
            assert.Throw(()=> flowerShop.calcPriceOfFlowers("string",4,'str'), "Invalid input!")
        })
        it("calcPriceOfFlowers valid  ", ()=>{
            assert.equal(flowerShop.calcPriceOfFlowers("5",5,5),"You need $25.00 to buy 5!")
        })
    })
    describe("checkFlowersAvailable", ()=>{
        it("checkFlowersAvailable ", ()=>{
            assert.equal(flowerShop.checkFlowersAvailable('5', ['5','5']), "The 5 are available!")
        })
        it("checkFlowersAvailable  when fwower is missing", ()=>{
            assert.equal(flowerShop.checkFlowersAvailable('6', ['5','5']), "The 6 are sold! You need to purchase more!")
        })
    })
    describe("sellFlowers", ()=>{

        it("sellFlowers  ", ()=>{
            assert.throw(()=>flowerShop.sellFlowers(5,5), "Invalid input!")
        })
        it("sellFlowers  should throw error when space is string ", ()=>{
            assert.throw(()=>flowerShop.sellFlowers(5,'5'), "Invalid input!")
        })
        it("sellFlowers  should throw error when space is < 0 ", ()=>{
            assert.throw(()=>flowerShop.sellFlowers(['5'],-1), "Invalid input!")
        })
        it("sellFlowers  should throw error when space is bigger than arr.lenght", ()=>{
            assert.throw(()=>flowerShop.sellFlowers(['5','6'],5), "Invalid input!")
        })
        it("sellFlowers  shouldd return ", ()=>{
            assert.equal(flowerShop.sellFlowers(['5','6','7'],2), "5 / 6")
        })
    })
})