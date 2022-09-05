import { html } from "../../node_modules/lit-html/lit-html.js";
import * as dashboardService from '../services/dashboardService.js'
import {postsIsInvalid} from '../utils/validators.js'

const createTemplate = (submitHandler) => html`
<section id="create">
          <div class="form">
            <h2>Add item</h2>
            <form @submit=${submitHandler}  class="create-form">
              <input
                type="text"
                name="brand"
                id="shoe-brand"
                placeholder="Brand"
              />
              <input
                type="text"
                name="model"
                id="shoe-model"
                placeholder="Model"
              />
              <input
                type="text"
                name="imageUrl"
                id="shoe-img"
                placeholder="Image url"
              />
              <input
                type="text"
                name="release"
                id="shoe-release"
                placeholder="Release date"
              />
              <input
                type="text"
                name="designer"
                id="shoe-designer"
                placeholder="Designer"
              />
              <input
                type="text"
                name="value"
                id="shoe-value"
                placeholder="Value"
              />

              <button type="submit">post</button>
            </form>
          </div>
        </section>
`
export const createView = (context) => {
    const submitHandler = (e) => {
        e.preventDefault();
   
        const postData =  Object.fromEntries(new FormData(e.currentTarget));
        if (postsIsInvalid(postData)) {
            alert('All fields should be required!!!!!!')
            return;
        }
        dashboardService.create(postData)
            .then(() => {
                context.page.redirect('/dashboard')
            }).catch(err => {
                alert(err);
            });
    }
    context.render(createTemplate(submitHandler));
}