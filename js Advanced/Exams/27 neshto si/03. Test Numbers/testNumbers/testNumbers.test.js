//const testNumbers =require('./testNumbers')
const { expect, assert } = require('chai')


const testNumbers = {
    sumNumbers: function (num1, num2) {
        let sum = 0;

        if (typeof (num1) !== 'number' || typeof (num2) !== 'number') {
            return undefined;
        } else {
            sum = (num1 + num2).toFixed(2);
            return sum
        }
    },
    numberChecker: function (input) {
        input = Number(input);

        if (isNaN(input)) {
            throw new Error('The input is not a number!');
        }

        if (input % 2 === 0) {
            return 'The number is even!';
        } else {
            return 'The number is odd!';
        }

    },
    averageSumArray: function (arr) {

        let arraySum = 0;

        for (let i = 0; i < arr.length; i++) {
            arraySum += arr[i]
        }

        return arraySum / arr.length
    }
};





describe('Test-Numbers', () => {

    describe('Test testNumbers Sum Function',()=>{
        it('Test Sum Numbers', () => {
            expect(testNumbers.sumNumbers(5, 5)).to.equal('10.00')
            expect(testNumbers.sumNumbers('5', '5')).to.equal(undefined)
            expect(testNumbers.sumNumbers('5', 'f')).to.equal(undefined)
            expect(testNumbers.sumNumbers('f', '5')).to.equal(undefined)
        })
        it('Test Sum Numbers', () => {
            expect(testNumbers.sumNumbers(-2, 5)).to.equal('3.00')
            expect(testNumbers.sumNumbers('5', '5')).to.equal(undefined)
            expect(testNumbers.sumNumbers('5', 'f')).to.equal(undefined)
            expect(testNumbers.sumNumbers('f', '5')).to.equal(undefined)
        })
    }),
    describe('Test testNumbers numberChecker Function',()=>{
        it('Test numberChecker check if input is not a Number', () => {
            expect(()=>testNumbers.numberChecker('NaN')).to.throw('The input is not a number!')
            expect(()=>testNumbers.numberChecker('dd')).to.throw('The input is not a number!')
            
        })
        it('Test numberChecker Numbers', () => {
            expect(testNumbers.numberChecker('20')).to.equal('The number is even!')
            expect(testNumbers.numberChecker(20)).to.equal('The number is even!')
            expect(testNumbers.numberChecker('5')).to.equal('The number is odd!')
            expect(testNumbers.numberChecker(5)).to.equal('The number is odd!')
        })
    })
    describe('Test testNumbers 	averageSumArray Function',()=>{
        it('Test 	averageSumArray check array', () => {
            //hi there
            expect(testNumbers.averageSumArray([5,5,5,5])).to.equal(5)
            //hi here
        })
       
    })

})