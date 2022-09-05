import { html, nothing } from "../../node_modules/lit-html/lit-html.js";

import * as postService from '../services/dashboardService.js'
import { postTemplate } from "./templates/postTemplate.js";

const dashBoardTemplate = (shoes,user) => html`

<section id="dashboard">
          <h2>Collectibles</h2>
          <ul class="card-wrapper">
            <!-- Display a li with information about every post (if any)-->
            ${shoes.map(x => postTemplate(x, Boolean(user)))}
          </ul>

          <!-- Display an h2 if there are no posts -->
          ${shoes.length == 0 
    ?html`<h2>There are no items added yet.</h2>`
    :nothing
    }
          
        </section>

`


export const dashboardView = (context)=>{
    postService.getAll()
        .then(shoes => {
         context.render(dashBoardTemplate(shoes, context.postService))
        })
}