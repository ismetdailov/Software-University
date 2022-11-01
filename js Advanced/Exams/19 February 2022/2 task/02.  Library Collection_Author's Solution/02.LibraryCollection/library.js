class LibraryCollection {
    constructor(capacity) {
        this.capacity = capacity;
        this.books = [];
    }
    addBook(bookName, bookAuthor) {

        if (this.capacity <= 0) {
            throw new Error('Not enough space in the collection.');
        }
        this.books.push({ bookName: bookName, bookAuthor: bookAuthor, payed: false })
        this.capacity --;
        return `The ${bookName}, with an author ${bookAuthor}, collect.`
    }
    payBook(bookName) {
        let findetBook = this.books.find(x => x.bookName === bookName)
        if (findetBook === undefined) {
            throw new Error(`${bookName} is not in the collection.`)
        }
        if (findetBook.payed === true) {
            throw new Error(`${bookName} has already been paid.`)
        }
        findetBook.payed = true;
        return `${bookName} has been successfully paid.`
    }
    removeBook(bookName) {
        let findetBook = this.books.find(x => x.bookName === bookName)
        if (findetBook === undefined) {
            throw new Error(`The book, you're looking for, is not found.`);
        }
        if (findetBook.payed === false) {
          throw new Error(`${bookName} need to be paid before removing from the collection.` )
        }
        this.books.pop(findetBook);
        return  `${bookName} remove from the collection.`                                                                        
    }
    getStatistics(bookAuthor) {
        if (bookAuthor === undefined) {
            throw new Error(`The book collection has ${emptySlots} empty spots left.`);
        }
        if (this.books.length === 0) {
            throw new Error(`${bookAuthor} is not in the collection.`)
        }
      this.books = this.books.sort((a, b) => a - b);
      let toReturn = ''
        this.books.forEach(e => {
            toReturn +=`${e.bookName} == ${e.bookAuthor} - ${e.payed === true ? 'Has Paid' : 'Not Paid' }.\n`
        })
        return toReturn
    }
}
// const library = new LibraryCollection(2)
// console.log(library.addBook('In Search of Lost Time', 'Marcel Proust'));
// console.log(library.addBook('Don Quixote', 'Miguel de Cervantes'));
// console.log(library.addBook('Ulysses', 'James Joyce'));

// const library = new LibraryCollection(2)
// library.addBook('In Search of Lost Time', 'Marcel Proust');
// console.log(library.payBook('In Search of Lost Time'));
// console.log(library.payBook('Don Quixote'));

// const library = new LibraryCollection(2)
// library.addBook('In Search of Lost Time', 'Marcel Proust');
// library.addBook('Don Quixote', 'Miguel de Cervantes');
// library.payBook('Don Quixote');
// console.log(library.removeBook('Don Quixote'));
// console.log(library.removeBook('In Search of Lost Time'));

// const library = new LibraryCollection(2)
// console.log(library.addBook('Don Quixote', 'Miguel de Cervantes'));
// console.log(library.getStatistics('Miguel de Cervantes'));


// let library = new LibraryCollection(2);

// library.addBook("In Search of Lost Time", "Marcel Proust");
// library.addBook('Don Quixote', 'Miguel de Cervantes');
// console.log(library.payBook('Don Quixote'))
// console.log(library.removeBook());
// console.log(library.removeBook('In Search of Lost Time'));

// let library = new LibraryCollection(2);

// console.log(library.addBook("In Search of Lost Time", "Marcel Proust"))
// console.log(library.payBook('In Search of Lost Time'))
// console.log(library.payBook('Don Quixote'))

// let library = new LibraryCollection(2);

// library.addBook("In Search of Lost Time", "Marcel Proust");
// library.payBook('In Search of Lost Time');
// library.payBook('Don Quixote')
let library = new LibraryCollection(2);

library.addBook("In Search of Lost Time", "Marcel Proust");
library.addBook('Don Quixote');
library.payBook('Don Quixote');

library.removeBook('Don Quixote');
library.removeBook('In Search of Lost Time');