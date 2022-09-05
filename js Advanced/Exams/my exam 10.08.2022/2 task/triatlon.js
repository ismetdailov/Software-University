class Triathlon {
    //TODO: Implement this class…
    constructor(competitionName) {
        this.competitionName = competitionName
        this.participants = {};
        this.listOfFinalists = [];

    }
    addParticipant(participantName, participantGender) {
        if (participantName in this.participants) {
            throw new Error(`${participantName} has already been added to the list`);
        }
        Object.assign(this.participants , { [participantName]: participantGender });
        return `A new participant has been added - ${participantName}`;
    };
    completeness(participantName, condition) {
        if (participantName in this.participants == false) {
            throw new Error(`${participantName} is not well prepared and cannot finish any discipline`);
        }else if(participantName in this.participants){
            if(condition<30){
                throw new Error(`${participantName} is not well prepared and cannot finish any discipline`)
            }
            let conditionStatus = condition / 30
            conditionStatus = parseInt(conditionStatus, 10)
            if(conditionStatus<=2){
                return `${participantName} could only complete ${conditionStatus} of the disciplines`
            }else{
                let participant = Object.entries(this.participants)
                let partici = participant.find(p=> p[0] == participantName)
                this.listOfFinalists.push(participant)
                return `Congratulations, ${participantName} finished the whole competition`
            }
        }
      
    };
    rewarding(participantName) {
        const lsit = this.listOfFinalists.some(p=>p.find(x=>x[0] == participantName))
        if(lsit == false){
           return `${participantName} is not in the current finalists list`
        }else{
            return `${participantName} was rewarded with a trophy for his performance`;
        }
        
    }
    showRecord(criteria) {
        if(criteria == "all"){
            const participans = this.listOfFinalists.map(p=>p.map(x=>x[0]));
            const  partici = participans.map(p=>p).sort()
            const  partici1 = partici.map(p=>`${p}`)
            const names = `${partici.join('\n')}`
            return [
                `List of all ${competitionName} finalists:`,
                `${partici.join('\n')}`,
            ].join('\n')
        }
        const gender = this.listOfFinalists.some(p=>p.find(x=>x[1] == criteria))
        if(this.listOfFinalists.length == 0){
            return `There are no ${participantGender}'s that finished the competition`
        }
        if(gender == true){
            const gender = this.listOfFinalists.some(p=>p.some(x=>x[1] == criteria))
            return `${gender[0]} is the first ${criteria} that finished the ${this.competitionName} triathlon`;

        }
    }
}
// const contest = new Triathlon("Dynamos");
// console.log(contest.addParticipant("Peter", "male"));
// console.log(contest.addParticipant("Sasha", "female"));
// console.log(contest.addParticipant("Peter", "male"));
// const contest = new Triathlon("Dynamos");
// console.log(contest.addParticipant("Peter", "male"));
// console.log(contest.addParticipant("Sasha", "female"));
// console.log(contest.addParticipant("George", "male"));
// console.log(contest.completeness("Peter", 100));
// console.log(contest.completeness("Sasha", 70));
// console.log(contest.completeness("George", 20));
// const contest = new Triathlon("Dynamos");
// console.log(contest.addParticipant("Peter", "male"));
// console.log(contest.addParticipant("Sasha", "female"));
// console.log(contest.completeness("Peter", 100));
// console.log(contest.completeness("Sasha", 70));
// console.log(contest.rewarding("Petaaaaaaar"));
// console.log(contest.rewarding("Sasha"));
const contest = new Triathlon("Dynamos");
console.log(contest.addParticipant("Peter", "male"));
console.log(contest.addParticipant("Sasha", "female"));
console.log(contest.completeness("Peter", 100));
console.log(contest.completeness("Sasha", 90));
console.log(contest.rewarding("Peter"));
console.log(contest.rewarding("Sasha"));
console.log(contest.showRecord("all"));
