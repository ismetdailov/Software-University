import { html, nothing } from "../../node_modules/lit-html/lit-html.js";

import * as postService from "../services/dashboardService.js"

import { postsIsInvalid } from '../utils/validators.js'


const editTemplate = (post, submitHandler)=> html`
   <section id="edit">
          <div class="form">
            <h2>Edit item</h2>
            <form @submit=${submitHandler} method="POST" class="edit-form">
              <input
                type="text"
                name="brand"
                id="shoe-brand"
                placeholder="Brand"
                value=${post.brand}
              />
              <input
                type="text"
                name="model"
                id="shoe-model"
                placeholder="Model"
                value=${post.model}
              />
              <input
                type="text"
                name="imageUrl"
                id="shoe-img"
                placeholder="Image url"
                value=${post.imageUrl}
              />
              <input
                type="text"
                name="release"
                id="shoe-release"
                placeholder="Release date"
                value=${post.release}
              />
              <input
                type="text"
                name="designer"
                id="shoe-designer"
                placeholder="Designer"
                value=${post.designer}
              />
              <input
                type="text"
                name="value"
                id="shoe-value"
                placeholder="Value"
                value=${post.value}
              />

              <button type="submit">post</button>
            </form>
          </div>
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
            context.page.redirect(`data/shoes/${postId}`)
        })
    }
    postService.getOne(postId)
    .then(post => {
        context.render(editTemplate(post, submitHandler))
    })
}