const testNumbers = require('./testNumbers')
const {expect ,assert} = require('chai')

describe('Test-Numbers',()=>{

    it('Test Sum Numbers', ()=>{
        expect(testNumbers.sumNumbers(5,5)).to.eqal(10)
    })

})