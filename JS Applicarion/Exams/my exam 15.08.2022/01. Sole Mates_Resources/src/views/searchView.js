import { html,  } from "../../node_modules/lit-html/lit-html.js";

import * as postService from "../services/dashboardService.js"
import { postTemplate } from "./templates/postTemplate.js";

const searchTemplate = (searchHandler, posts) => html`
  <section id="search">
          <h2>Search by Brand</h2>

          <form class="search-wrapper cf">
            <input
              id="#search-input"
              type="text"
              name="search"
              placeholder="Search here..."
              required
            />
            <button type="submit" @click=${searchHandler}>Search</button>
          </form>

          <h3>Results:</h3>

          <div id="search-container">
            <ul class="card-wrapper">
              <!-- Display a li with information about every post (if any)-->
                ${posts.length > 0
                ? posts.map(x=> postTemplate(x))
                : html`<h2>There are no results found.</h2>`
                }
            </ul>

            <!-- Display an h2 if there are no posts -->
            <!-- <h2>There are no results found.</h2> -->
          </div>
        </section>
`

export const searchView =(context) => {
    const searchHandler = (e) => {
        let searchElement = document.getElementById('#search-input')
        postService.search(searchElement.value)
        .then(shoes=>{
          context.render(searchTemplate(searchHandler,shoes))

        })
    }
    context.render(searchTemplate(searchHandler,[]))
}