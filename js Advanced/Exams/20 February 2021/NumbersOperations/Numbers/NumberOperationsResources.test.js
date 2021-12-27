const numberOperations = {
    powNumber: function (num) {
        return num * num;
    },
    numberChecker: function (input) {
        input = Number(input);

        if (isNaN(input)) {
            throw new Error('The input is not a number!');
        }

        if (input < 100) {
            return 'The number is lower than 100!';
        } else {
            return 'The number is greater or equal to 100!';
        }
    },
    sumArrays: function (array1, array2) {

        const longerArr = array1.length > array2.length ? array1 : array2;
        const rounds = array1.length < array2.length ? array1.length : array2.length;

        const resultArr = [];

        for (let i = 0; i < rounds; i++) {
            resultArr.push(array1[i] + array2[i]);
        }

        resultArr.push(...longerArr.slice(rounds));

        return resultArr
    }
};
const{expect, assert}= require('chai')

describe('Test Numbers Operations',()=>{
    describe('Test pow Number',()=>{
        it('test power of pow Number', ()=>{
            expect(numberOperations.powNumber(5)).to.equal(25)
        })
    })
    describe('Test number Checker function',()=>{
        it('test number checker should throw Error', ()=>{
            expect(()=>numberOperations.numberChecker('ddd')).to.throw('The input is not a number!')
            expect(()=>numberOperations.numberChecker(NaN)).to.throw('The input is not a number!')
        })
        it('test number checker should throw', ()=>{
            expect(()=>numberOperations.numberChecker(undefined)).to.throw('The input is not a number!')
        })
        it('test number The number is lower than 100!', ()=>{
            expect(numberOperations.numberChecker(99)).to.equal('The number is lower than 100!')
        })
        it('test number The number is greater or equal to 100!', ()=>{
            expect(numberOperations.numberChecker(100)).to.equal('The number is greater or equal to 100!')
        })
    })
    describe('Test Sum Array',()=>{
        it('test Sum of two arr', ()=>{
            let arr =[3,3]
            expect(numberOperations.sumArrays([1,1],[2,2])).to.eql([ 3, 3 ])
            expect(numberOperations.sumArrays([1,1],[2])).to.eql([ 3, 1 ])
        })
        it('test Sum of arrays', ()=>{
            let arr =[3,3]
            expect(numberOperations.sumArrays([1,1],[0])).to.eql([ 1, 1 ])
        })
    })
})