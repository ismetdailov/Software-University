import { html, nothing } from "../../node_modules/lit-html/lit-html.js";

import * as albumService from '../services/dashboardService.js'

const postDetails =(postId) => html`
                    <div class="btn-wrapper">
                        <a href="/posts/${postId}" class="details-btn btn">Details</a>
                    </div>
`

const postTemplate = (post, withDetails=true) => html`
                <div class="post">
                    <h2 class="post-title">${post.title}</h2>
                    <img class="post-image" src="${post.imageUrl}" alt="Kids clothes">
                    ${postDetails(post._id)}
                </div>
`
const dashBoardTemplate = (posts,user) => html`
<section id="dashboard-page">
    <h1 class="title">All Posts</h1>

    <!-- Display a div with information about every post (if any)-->
    <div class="all-posts">
        ${posts.map(x => postTemplate(x, Boolean(user)))}
    </div>
    <!-- Display an h1 if there are no posts -->
    ${posts.length == 0 
    ?html`<h1 class="title no-posts-title">No posts yet!</h1>`
    :nothing
    }
</section>
`

export const dashboardView = (context)=>{
    albumService.getAll()
        .then(albums => {
         context.render(dashBoardTemplate(albums, context.albumService))
        })
}