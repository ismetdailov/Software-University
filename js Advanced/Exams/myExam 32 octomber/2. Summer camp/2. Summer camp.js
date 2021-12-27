class SummerCamp {
    constructor(organizer, location) {
        this.organizer = organizer
        this.location = location
        this.priceForTheCamp = { "child": 150, "student": 300, "collegian": 500 }
        this.listOfParticipants = []

    }
    registerParticipant(name, condition, money) {

        let nn = Object.values(this.listOfParticipants).includes(name)
        if ((condition in this.priceForTheCamp)) {
            if (this.priceForTheCamp[condition] > money) {
                return `The money is not enough to pay the stay at the camp.`
            }
            if (this.listOfParticipants.name != undefined) {
                if (nn) {
                    return `The ${name} is already registered at the camp.`
                }
            }
            if (nn == false) {
                this.listOfParticipants[name] = {}
                this.listOfParticipants[name] = {
                    name,
                    condition,
                    power: 100,
                    wins: 0
                }
                return `The ${name} was successfully registered.`
            }
        }
        else {
            throw new Error("Unsuccessful registration at the camp.")
        }
        

    }
    unregisterParticipant(name) {
        let nn = Object.keys(this.listOfParticipants).includes(name)
        if (nn == true) {
            this.listOfParticipants.pop(name)
            return `The ${name} removed successfully.`
        }
        else {
            throw new Error(`The ${name} is not registered in the camp.`)

        }


    }
    timeToPlay(typeOfGame, participant1, participant2) {
        let nn1 = Object.keys(this.listOfParticipants).includes(participant1)
        let nn2 = Object.keys(this.listOfParticipants).includes(participant2)
        let mn = Object.values(this.listOfParticipants)
        let as = mn.name == participant1
        if (typeOfGame == 'WaterBalloonFights') {
            if (nn1 == false && nn2 == false) {
                throw new Error(`Invalid entered $name/s.`)
            }
            if (Object.values(mn).includes(participant1) && Object.values(mn).includes(participant2)) {
                Object.keys(this.listOfParticipants).forEach(e => {
                    let name = e[1]
                    if (name) {

                    }
                })

                return `The ${name} is winner in the game ${typeOfGame}.`
            } else {
                throw new Error(`Choose players with equal condition.`)
            }




        } else if (typeOfGame == 'Battleship') {
            if (nn1 == false) {
                throw new Error(`Invalid entered $name/s.`)

            }
            if (mn.includes(participant1)) {
                mn = Array.from(this.listOfParticipants).some(e => e.name == participant1 && e.power == power, power += 20)
                return `The ${participant1} successfully completed the game ${typeOfGame}.`
            } else {
                throw new Error(`Choose players with equal condition.`)
            }
        }
    }
    toString() {
        console.log(`{organizer} will take {numberOfParticipants} participants on camping to {location}`)
        Object.values(this.listOfParticipants).forEach(e => {
            e.wins.sort((a, b) => a.localeCompare(b))
        })
    }
}
// const summerCamp = new SummerCamp("Jane Austen", "Pancharevo Sofia 1137, Bulgaria");
// console.log(summerCamp.registerParticipant("Petar Petarson", "student", 200));
// console.log(summerCamp.registerParticipant("Petr Petarson", "student", 300));
// console.log(summerCamp.registerParticipant("Petar Petarson", "student", 300));
// console.log(summerCamp.registerParticipant("Leila Wolfe", "childd", 200));

// const summerCamp = new SummerCamp("Jane Austen", "Pancharevo Sofia 1137, Bulgaria");
// console.log(summerCamp.registerParticipant("Petar Petarson", "student", 300));
// console.log(summerCamp.unregisterParticipant("Petar"));
// console.log(summerCamp.unregisterParticipant("Petar Petarson"));

// const summerCamp = new SummerCamp("Jane Austen", "Pancharevo Sofia 1137, Bulgaria");
// // console.log(summerCamp.registerParticipant("Petar Petarson", "student", 300));
// // console.log(summerCamp.timeToPlay("WaterBalloonFights", "Petar Petarson", "Sara Dickinson"));
// // console.log(summerCamp.timeToPlay("Battleship", "Petar Petarson"));
// console.log(summerCamp.registerParticipant("Sara Dickinson", "child", 200));
// console.log(summerCamp.registerParticipant("Dimitur Kostov", "student", 300));
// console.log(summerCamp.timeToPlay("WaterBalloonFights", "Sara Dickinson", "Dimitur Kostov"));

// const summerCamp = new SummerCamp("Jane Austen", "Pancharevo Sofia 1137, Bulgaria");
// console.log(summerCamp.registerParticipant("Petar Petarson", "student", 300));
// console.log(summerCamp.timeToPlay("Battleship", "Petar Petarson"));
// console.log(summerCamp.registerParticipant("Sara Dickinson", "child", 200));
// console.log(summerCamp.timeToPlay("WaterBalloonFights", "Petar Petarson", "Sara Dickinson"));
// console.log(summerCamp.registerParticipant("Dimitur Kostov", "student", 300));
// console.log(summerCamp.timeToPlay("WaterBalloonFights", "Petar Petarson", "Dimitur Kostov"));

// console.log(summerCamp.toString());

const summerCamp = new SummerCamp("Jane Austen", "Pancharevo Sofia 1137, Bulgaria");
console.log(summerCamp.registerParticipant("Petar Petarson", "student", 300));
console.log(summerCamp.unregisterParticipant("Petar"));
console.log(summerCamp.unregisterParticipant("Petar Petarson"));

