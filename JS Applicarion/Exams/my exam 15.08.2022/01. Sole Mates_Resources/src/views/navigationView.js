import { html } from "../../node_modules/lit-html/lit-html.js";



const guestLinks = html`
<div class="guest">
    <a href="/Login">Login</a>
    <a href="/Register">Register</a>
</div>

`
const userLinks = html`
<div class="user">
    <a href="/Add">Add Pair</a>
    <a href="/Logout">Logout</a>
</div>
`

const navigtionTemplate = (user) => html`

<nav>
    <div>
        <a href="/Dashboard">Dashboard</a>
        <a href="/Search">Search</a>
    </div>

    ${user
            ?userLinks
            :guestLinks
        }
    <!-- Logged-in users -->
    <!-- Guest users -->
</nav>
`
export const navigationView = (context) =>{
    return navigtionTemplate(context.user);
}