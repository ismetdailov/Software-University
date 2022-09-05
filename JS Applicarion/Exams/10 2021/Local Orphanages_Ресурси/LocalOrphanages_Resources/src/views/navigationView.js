import { html } from "../../node_modules/lit-html/lit-html.js";

const guestLinks = html`
<div id="guest">
        <a href="/Login">Login</a>
        <a href="/Register">Register</a>
    </div>
`
const userLinks = html`
<div id="user">
        <a href="/Posts">My Posts</a>
        <a href="/Create">Create Post</a>
        <a href="/Logout">Logout</a>
    </div>
`
const navigtionTemplate = (user) => html`

<nav>
    <a href="/">Dashboard</a>

    <!-- Logged-in users -->
        ${user
            ?userLinks
            :guestLinks
        }

    <!-- Guest users -->
    
</nav>
`
export const navigationView = (context) =>{
    return navigtionTemplate(context.user);
}