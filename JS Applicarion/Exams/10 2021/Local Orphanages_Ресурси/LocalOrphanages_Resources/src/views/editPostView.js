import { html, nothing } from "../../node_modules/lit-html/lit-html.js";

import * as postService from "../services/dashboardService.js"

import { postsIsInvalid } from '../utils/validators.js'

const editTemplate = (post, submitHandler)=> html`
   <!-- Edit Page (Only for logged-in users) -->
   <section id="edit-page" class="auth">
            <form @submit=${submitHandler} method="POST" id="edit">
                <h1 class="title">Edit Post</h1>

                <article class="input-group">
                    <label for="title">Post Title</label>
                    <input type="title" name="title" id="title" value=${post.title}>
                </article>

                <article class="input-group">
                    <label for="description">Description of the needs </label>
                    <input type="text" name="description" id="description" value=${post.description}>
                </article>

                <article class="input-group">
                    <label for="imageUrl"> Needed materials image </label>
                    <input type="text" name="imageUrl" id="imageUrl" value=${post.imageUrl}>
                </article>

                <article class="input-group">
                    <label for="address">Address of the orphanage</label>
                    <input type="text" name="address" id="address" value=${post.address}>
                </article>

                <article class="input-group">
                    <label for="phone">Phone number of orphanage employee</label>
                    <input type="text" name="phone" id="phone" value=${post.phone}>
                </article>

                <input type="submit" class="btn submit" value="Edit Post">
            </form>
        </section>
`
export const editPostView = (context) => {
    const postId = context.params.postId;
    const submitHandler = (e)=>{
        e.preventDefault();
        const postData =  Object.fromEntries(new FormData(e.currentTarget));
        if (postsIsInvalid(postData)) {
            alert('All fields should be filled!!!') 
        }
        postService.edit(postId, postData)
        .then(()=>{
            context.page.redirect(`/posts/${postId}`)
        })
    }
    postService.getOne(postId)
    .then(post => {
        context.render(editTemplate(post, submitHandler))
    })
}