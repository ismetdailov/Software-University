class SummerCamp {
    constructor(organizer, location) {
        this.organizer = organizer;
        this.location = location;
        this.priceForTheCamp = {"child": 150, "student": 300, "collegian": 500};
        this.listOfParticipants = [];

    };

    registerParticipant(name, condition, money) {
        // Petar Petarson", "student", 200

        if (this.priceForTheCamp.hasOwnProperty(condition) === false) {
            throw new Error(`Unsuccessful registration at the camp.`);
        }

        if (this.listOfParticipants.find(x => x.name === name)) {
            return `The ${name} is already registered at the camp.`;
        }

        if (money < this.priceForTheCamp[condition]) {
            return `The money is not enough to pay the stay at the camp.`;
        }

        this.listOfParticipants.push({
            name: name,
            condition: condition,
            power: 100,
            wins: 0,
        });

        return `The ${name} was successfully registered.`;
    }

    unregisterParticipant(name) {

        if (!this.listOfParticipants.find(x => x.name === name)) {
            throw new Error(`The ${name} is not registered in the camp.`);
        }

        let currentParticipant = this.listOfParticipants.find(x => x.name === name);
        let index = this.listOfParticipants.indexOf(currentParticipant);

        this.listOfParticipants.splice(index, 1);

        return `The ${name} removed successfully.`;
    }

    timeToPlay(typeOfGame, participant1, participant2) {

        let firstParticipant = this.listOfParticipants.find(x => x.name === participant1);
        let secondParticipant = this.listOfParticipants.find(x => x.name === participant2);

        if (typeOfGame === 'Battleship') {

            if (firstParticipant === undefined) {
                throw new Error(`Invalid entered name/s.`);
            }

            firstParticipant.power += 20
            return `The ${firstParticipant.name} successfully completed the game ${typeOfGame}.`;

        } else if (typeOfGame === 'WaterBalloonFights') {
            if (firstParticipant === undefined || secondParticipant === undefined) {
                throw new Error(`Invalid entered name/s.`);
            }

            if (firstParticipant.condition !== secondParticipant.condition) {
                throw new Error(`Choose players with equal condition.`);
            }
            let firstParticipantPower = firstParticipant.power;
            let secondParticipantPower = secondParticipant.power;

            if (firstParticipantPower > secondParticipantPower) {
                firstParticipant.wins += 1;
                return `The ${firstParticipant.name} is winner in the game ${typeOfGame}.`;

            } else if (secondParticipantPower > firstParticipantPower) {

                secondParticipant.wins += 1;
                return `The ${secondParticipant.name} is winner in the game ${typeOfGame}.`;

            } else {
                return `There is no winner.`;
            }
        }
    }

    toString() {

        let result = [];

        result.push(`${this.organizer} will take ${this.listOfParticipants.length} participants on camping to ${this.location}`);

        this.listOfParticipants = this.listOfParticipants.sort((a, b) => b.wins - a.wins);
        this.listOfParticipants.forEach(x => result.push(`${x.name} - ${x.condition} - ${x.power} - ${x.wins}`));

        return result.join('\n');
    }
}
const summerCamp = new SummerCamp("Jane Austen", "Pancharevo Sofia 1137, Bulgaria");
console.log(summerCamp.registerParticipant("Petar Petarson", "student", 300));
console.log(summerCamp.timeToPlay("Battleship", "Petar Petarson"));
console.log(summerCamp.registerParticipant("Sara Dickinson", "child", 200));
console.log(summerCamp.timeToPlay("WaterBalloonFights", "Petar Petarson", "Sara Dickinson"));
console.log(summerCamp.registerParticipant("Dimitur Kostov", "student", 300));
console.log(summerCamp.timeToPlay("WaterBalloonFights", "Petar Petarson", "Dimitur Kostov"));
