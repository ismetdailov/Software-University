import { Book } from "./Book"

export const Booklist = (props) => {
    return(
        <ul>
       <Book title = {props.book[0].title}/>
       <Book/>
       <Book/>
       <Book/>
        </ul>
    )
}