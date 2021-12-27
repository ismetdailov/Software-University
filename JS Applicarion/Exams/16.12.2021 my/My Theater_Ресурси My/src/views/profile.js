import {  getMyTeathers } from "../api/data.js";
import { html } from "../lib.js";
import { getUserData } from "../util.js";


const profileTemplate = (teathers) =>html `
 <section id="profilePage">
            <div class="userInfo">
                <div class="avatar">
                    <img src="./images/profilePic.png">
                </div>
                <h2>steven@abv.bg</h2>
            </div>
            <div class="board">
                <!--If there are event-->
                
                ${teathers.length == 0 ?  html`<div class="no-events">
                    <p>This user has no events yet!</p>
                </div>` :teathers.map(theater) }
                <!--If there are no event-->
                
            </div>
        </section>
`;
const theater = (teather) => html `
   <div class="eventBoard">
                    <div class="event-info">
                        <img src=${teather.imageUrl}>
                        <h2>${teather.title}</h2>
                        <h6>${teather.date}</h6>
                        <a href="/details/${teather._id}" class="details-button">Details</a>
                    </div>
                </div>`;

export async function profilePage(ctx) {
    const userData = getUserData();
    const teathers = await getMyTeathers(userData.id)
    ctx.render(profileTemplate(teathers));
}