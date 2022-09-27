class SmartHike {
    constructor(username) {
        this.username = username;
        this.goals = {};
        this.listOfHikes = [];
        this.resources = 100;
    }
    addGoal(peak, altitude) {
        if (peak in this.goals) {
            return `${peak} has already been added to your goals`;
        }
        this.goals[peak] = altitude
        return `You have successfully added a new goal - ${peak}`
    }
    hike(peak, time, difficultyLevel){
        if (peak in this.goals) {
            if (this.goals[peak]<=0) {
                throw new Error(`You don't have enough resources to start the hike`)
            }
        }else{
            throw new Error `${peak} is not in your current goals`;
        }
        let different = this.goals[peak] - time * 10;
        if (different <0) {
            return "You don't have enough resources to complete the hike";
        }
        else{
            var values = Object.values(this.goals)
            values.forEach(element => {
                
            });
        }
    }
}
const user = new SmartHike('Vili');
console.log(user.addGoal('Musala', 2925));
console.log(user.addGoal('Rui', 1706));
console.log(user.hike('Musala', 8, 'hard'));
console.log(user.hike('Rui', 3, 'easy'));
console.log(user.hike('Everest', 12, 'hard'));

