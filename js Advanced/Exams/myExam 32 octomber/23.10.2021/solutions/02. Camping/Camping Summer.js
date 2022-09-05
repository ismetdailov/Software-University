class SummerCamp {
    constructor(organizer, location) {
        this.organizer = organizer;
        this.location = location;
        this.priceForTheCamp = { child: 150, student: 300, collegian: 500 };
        this.listOfParticipants = [];
    }
    registerParticipant(name, condition, money) {
        if (this.priceForTheCamp.hasOwnProperty(condition)) {
            var neshto = this.listOfParticipants.some(x => x.name === name);
            if (this.listOfParticipants.some(x => x.name === name)) {
                return `The ${name} is already registered at the camp.`;
            }
            let obj = {}
            if (money === 150 && condition === 'child') {
                obj = { name, condition, money };
            } else if (money === 300 && condition === 'student') {
                obj = { name, condition, money };
            } else if (money === 500 && condition === 'collegian') {
                obj = { name, condition, money };
            } else {
                return `The money is not enough to pay the stay at the camp.`;
            }
            this.listOfParticipants.push(obj);
            return `The ${name} was successfully registered.`;
        } else {
            throw new Error(`Unsuccessful registration at the camp.`);
        }
    }
    unregisterParticipant(name) {
        if (!this.listOfParticipants.some(x => x.name === name)) {
            throw new Error(`The ${name} is not registered in the camp.`);
        }else{
           this.listOfParticipants = this.listOfParticipants.filter(x=>x.name != name);
            return `The ${name} removed successfully.`;
        }
    }
    timeToPlay (typeOfGame, participant1, participant2){
        
    }

}
const summerCamp = new SummerCamp("Jane Austen", "Pancharevo Sofia 1137, Bulgaria");
// console.log(summerCamp.registerParticipant("Petar Petarson", "student", 200));
// console.log(summerCamp.registerParticipant("Petar Petarson", "student", 300));
// console.log(summerCamp.registerParticipant("Petar Petarson", "student", 300));
// console.log(summerCamp.registerParticipant("Leila Wolfe", "childd", 200));
console.log(summerCamp.registerParticipant("Petar Petarson", "student", 300));
console.log(summerCamp.unregisterParticipant("Petar"));
console.log(summerCamp.unregisterParticipant("Petar Petarson"));
