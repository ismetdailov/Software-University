import { html, nothing } from "../../node_modules/lit-html/lit-html.js";

import * as postService from "../services/dashboardService.js"
const detailsTemplate = (post, isOwner) => html`
    <section id="details-page">
        <h1 class="title">Post Details</h1>
    
        <div id="container">
            <div id="details">
                <div class="image-wrapper">
                    <img src="${post.imageUrl}" alt="Material Image" class="post-image">
                </div>
                <div class="info">
                    <h2 class="title post-title">${post.title}</h2>
                    <p class="post-description">Description: ${post.description}</p>
                    <p class="post-address">Address: ${post.address}</p>
                    <p class="post-number">Phone number: ${post.phone}</p>
                    <p class="donate-Item">Donate Materials: 0</p>
    
                    <!--Edit and Delete are only for creator-->
                    <div class="btns">
                        ${ isOwner
                            ?html`
                        <a href="/posts/${post._id}/edit" class="edit-btn btn">Edit</a>
                        <a href="/posts/${post._id}/delete" class="delete-btn btn">Delete</a> `
                        :nothing
                       }
                        <!--Bonus - Only for logged-in users ( not authors )-->
                        <a href="#" class="donate-btn btn">Donate</a>
                    </div>
    
                </div>
            </div>
        </div>
    </section>
`

export const detailsView = (context) => {
    postService.getOne(context.params.postId)
        .then(post => {
        let isOwner = post._ownerId==context.user._id;

            context.render(detailsTemplate(post, context.user));
        })
}