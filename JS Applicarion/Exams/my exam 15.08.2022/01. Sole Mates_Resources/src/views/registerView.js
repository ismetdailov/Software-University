import { html } from "../../node_modules/lit-html/lit-html.js";
import * as userService from '../services/userService.js'

const registerTemplate = (submitHandler) => html`
 <section id="register">
          <div class="form">
            <h2>Register</h2>
            <form @submit=${submitHandler} class="login-form">
              <input
                type="text"
                name="email"
                id="register-email"
                placeholder="email"
              />
              <input
                type="password"
                name="password"
                id="register-password"
                placeholder="password"
              />
              <input
                type="password"
                name="re-password"
                id="repeat-password"
                placeholder="repeat password"
              />
              <button type="submit">login</button>
              <p class="message">Already registered? <a href="#">Login</a></p>
            </form>
          </div>
        </section>
`
export const registerView=(context) =>{
    const submitHandler = (e) =>{
        e.preventDefault();
        let formData = new FormData(e.currentTarget);
        const {email,password}= Object.fromEntries(formData);
        const confPass = formData.get('repeatPassword')
        if (confPass != password) {
            alert('Pass missmatch')
            return;
        }

        userService.register(email,password)
        .then(()=>{
            context.page.redirect('/dashboard')
        })
    }
    context.render(registerTemplate(submitHandler));
}