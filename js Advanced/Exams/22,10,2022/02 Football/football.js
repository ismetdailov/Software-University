class footballTeam {
    constructor(clubName, country) {
        this.clubName = clubName
        this.country = country
        this.invitedPlayers = []
    }
    newAdditions(footballPlayers) {
        footballPlayers.forEach(e => {
            let arr = e.split('/')
            let findPlayer = this.invitedPlayers.find(x => x.name === arr[0])
            if (findPlayer) {
                if (arr[2] > findPlayer.playerValue) {
                    this.invitedPlayers.pop(findPlayer);
                    findPlayer.playerValue = arr[2];
                    this.invitedPlayers.push(findPlayer);

                }
            }
            this.invitedPlayers.push({ name: arr[0], age: Number(arr[1]), playerValue: Number(arr[2]) })
        });
        // possible problem unique names
        return `You successfully invite ${this.invitedPlayers.map(x => x.name).join(', ')}.`
    }
    signContract(selectedPlayer) {
        var arr = selectedPlayer.split('/')
        let playOffer = Number(arr[1])
        let findPlayer = this.invitedPlayers.find(x => x.name === arr[0])
        if (findPlayer === undefined) {
            throw new Error(`${arr[0]} is not invited to the selection list!`)
        }
        if (playOffer < findPlayer.playerValue) {
            throw new Error(`The manager's offer is not enough to sign a contract with ${findPlayer.name}, ${findPlayer.playerValue -playOffer} million more are needed to sign the contract!`)
        }
        let index = this.invitedPlayers.findIndex(x=>x.name === findPlayer.name)
        this.invitedPlayers.splice(index,1)
        findPlayer.playerValue = 'Bought'
        this.invitedPlayers.push(findPlayer);
        return `Congratulations! You sign a contract with ${findPlayer.name} for ${playOffer} million dollars.`
    }
    ageLimit(name, age){
        let findPlayer = this.invitedPlayers.find(x => x.name === name)
        if (findPlayer === undefined) {
            throw new Error(`${name} is not invited to the selection list!`)
        }
        if (findPlayer.age < age) {
            let diferent = age - findPlayer.age 
            if (diferent < 5 ) {
                return `${findPlayer.name} will sign a contract for ${diferent} years with ${this.clubName} in ${this.country}!`
            }else{
                return `${findPlayer.name} will sign a full 5 years contract for ${this.clubName} in ${this.country}!`
            }
        }else{
            return `${findPlayer.name} is above age limit!`
        }
    }
    transferWindowResult(){
        let toReturn = [];

        toReturn.push('Players list:')
        this.invitedPlayers.sort((a, b) => a.name.localeCompare(b.name));
             this.invitedPlayers.forEach(e=>{
                toReturn.push(`Player ${e.name}-${e.playerValue}`)
             })
             return toReturn.join('\n')
    }
}

// let fTeam = new footballTeam("Barcelona", "Spain");
// console.log(fTeam.newAdditions(["Kylian Mbappé/23/160", "Lionel Messi/35/50", "Pau Torres/25/52"]));
// let fTeam = new footballTeam("Barcelona", "Spain");
// console.log(fTeam.newAdditions(["Kylian Mbappé/23/160", "Lionel Messi/35/50", "Pau Torres/25/52"]));
// console.log(fTeam.signContract("Lionel Messi/60"));
// console.log(fTeam.signContract("Kylian Mbappé/240"));
// console.log(fTeam.signContract("Barbukov/10"));

// let fTeam = new footballTeam("Barcelona", "Spain");
// console.log(fTeam.newAdditions(["Kylian Mbappé/23/160", "Lionel Messi/35/50", "Pau Torres/25/52"]));
// console.log(fTeam.ageLimit("Lionel Messi", 33 ));
// console.log(fTeam.ageLimit("Kylian Mbappé", 30));
// console.log(fTeam.ageLimit("Pau Torres", 26));
// console.log(fTeam.signContract("Kylian Mbappé/240"));

let fTeam = new footballTeam("Barcelona", "Spain");
console.log(fTeam.newAdditions(["Kylian Mbappé/23/160", "Lionel Messi/35/50", "Pau Torres/25/52"]));
console.log(fTeam.signContract("Kylian Mbappé/240"));
console.log(fTeam.ageLimit("Kylian Mbappé", 30));
console.log(fTeam.transferWindowResult());

