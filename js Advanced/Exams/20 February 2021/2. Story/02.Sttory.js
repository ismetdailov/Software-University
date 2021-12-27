class Story {
    constructor(title, creator) {
        this.title = title,
            this.creator = creator,
            this._comments = [],
            this._likes = []
    }
    get likes() {
        if (this._likes.length === 0) {
            return `${this.title} has 0 likes`
        } else if (this._likes.length == 1) {
            return `${this._likes[0]} likes this story!`
        } else {
           
            return `${this._likes[0]} and ${this._likes.length - 1} others like this story!`
        }
        
    }
    like(username) {
        if (this._likes.includes(username)) {
            throw new Error("You can't like the same story twice!")
        } else if (username === this.creator) {
            throw new Error("You can't like your own story!")
        } else {
            this._likes.push(username)
            return `${username} liked ${this.title}!`
        }
    }
    dislike(username) {
        if (!(this._likes.includes(username))) {
            throw new Error("You can't dislike this story!")
        } else {
            this._likes.pop(username)
            return `${username} disliked ${this.title}`
        }
    }
    comment(username, content, id) {
        if (id === undefined) {
            id =this._comments.length
            id++
            let comentar = {
                Id: id,
                Username: username,
                Content: content,
                Replies: []
            }
            this._comments.push(comentar)

            return `${username} commented on ${this.title}`
        } else {
            let curr = 0
            for (let index = 0; index < this._comments.length; index++) {
                const element = this._comments[index];

                if (element.Id == id) {

                    curr += element.Id
                    if (element.Replies.find(e => e.Id) != undefined) {
                        curr++
                    }
                }
            }
            if (curr === undefined || curr <= 0) {
                curr = 1;
            }
            let idi = `${id}.${curr}`
            let replies = {
                Id: idi,
                Username: username,
                Content: content
            }
            let flag = 0
            for (let index = 0; index < this._comments.length; index++) {
                const element = this._comments[index];
                if (element.Id == id) {
                    element.Replies.push(replies)
                }
                if (element.Id!=undefined) {
                    
                    flag +=element.Replies.Id
                }
               

            }
            if (flag<1) {
                return `${username} commented on ${this.title}`
            }else{

                return `You replied successfully`;
            }
        }

    }
    toString(sortingType) {
        let comts = []
        if (sortingType === 'asc') {

            let comts = []
            this._comments.sort((a, b) => a.Id - b.Id)
            for (let index = 0; index < this._comments.length; index++) {
                const element = this._comments[index];
                element.Replies.sort((c, v) => c.Id - v.Id)
            }

        } else if (sortingType === 'desc') {
            let comts = []
            this._comments.sort((a, b) => b.Id - a.Id)
            for (let index = 0; index < this._comments.length; index++) {
                const element = this._comments[index];
                element.Replies.sort((c, v) => v.Id - c.Id)
            }

        } else if (sortingType === 'username') {

            this._comments.sort((a, b) => a.Username.localeCompare(b.Username))
            for (let index = 0; index < this._comments.length; index++) {
                const element = this._comments[index];
                element.Replies.sort((c, v) => c.Username.localeCompare(v.Username))
            }


        }
        comts.push(`Title: ${this.title}`)
        comts.push(`Creator: ${this.creator}`)
        comts.push(`Likes: ${this._likes.length}`)
        this._comments.forEach(e => {
            let idCo = e.Id
            let userCo = e.Username
            let conCo = e.Content

            comts.push(`-- ${idCo}. ${userCo}: ${conCo}`)

        })
        for (let index = 0; index < this._comments.length; index++) {
            const element = this._comments[index];
            element.Replies.forEach(e => {
                let id = e.Id
                let ser = e.Username
                let cont = e.Content
                comts.push(`--- ${id}. ${ser}: ${cont}`)

            })
        }

        return comts.join('\n')
    }
}
let art = new Story("My Story", "Anny");
art.like("John");
console.log(art.likes);
art.dislike("John");
console.log(art.likes);
art.comment("Sammy", "Some Content");
console.log(art.comment("Ammy", "New Content"));
art.comment("Zane", "Reply", 1);
art.comment("Jessy", "Nice :)");
console.log(art.comment("SAmmy", "Reply@", 1));
console.log()
console.log(art.toString('username'));
console.log()
art.like("Zane");
console.log(art.toString('desc'));
