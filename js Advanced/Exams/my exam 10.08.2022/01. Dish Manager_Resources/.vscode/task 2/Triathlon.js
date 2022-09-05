class Triathlon {
    //TODO: Implement this class…
    constructor(competitionName) {
        this.competitionName = competitionName
        this.participants = {};
        this.listOfFinalists = [];

    }
    addParticipant(participantName, participantGender) {
        if (Object.keys(this.participants).includes(participantName)) {
            throw new Error('${participantName} has already been added to the list');
        }
        this.participants = { participantName: participantGender };
        return `A new participant has been added - ${participantName}`;
    };
    completeness(participantName, condition) {
        if (Object.keys(this.participants).includes(participantName) == false) {
            throw new Error(`${participantName} is not well prepared and cannot finish any discipline`);
        }else if(Object.keys(this.participants).includes(participantName)){
            if(condition<30){
                throw new Error(`${participantName} is not well prepared and cannot finish any discipline`)
            }
        }
        else{
            let conditionStatus =Number( condition / 30)
            if(conditionStatus<=2){
                return `${participantName} could only complete ${completedCount} of the disciplines`
            }else{
                let participant = Object.entries(this.participants).includes(participantName)
                this.listOfFinalists.push(participant)
                return `Congratulations, ${participantName} finished the whole competition`
            }
        }
    };
    rewarding(participantName) {

    }
    showRecord(criteria) {

    }
}
const contest = new Triathlon("Dynamos");
console.log(contest.addParticipant("Peter", "male"));
console.log(contest.addParticipant("Sasha", "female"));
console.log(contest.addParticipant("Peter", "male"));
