import { deleteById, getMemeById } from "../api/data.js";
import { html } from "../lib.js";
import { getUserData } from "../util.js";

const detailsTemplate = (meme,isOwner,onDelete) =>html `
  <section id="detailsPage">
            <div class="wrapper">
                <div class="albumCover">
                    <img src=${meme.imgUrl}>
                </div>
                <div class="albumInfo">
                    <div class="albumText">

                        <h1>Name: ${meme.name}</h1>
                        <h3>Artist: ${meme.artist}</h3>
                        <h4>Genre: ${meme.genre}</h4>
                        <h4>Price: $${meme.price}</h4>
                        <h4>Date: ${meme.releaseDate}</h4>
                        <p>Description: ${meme.description}</p>
                    </div>
                    <!-- Only for registered user and creator of the album-->
                 
                    <div class="actionBtn">
                   ${albumControlTemplate(meme,isOwner,onDelete)}
                    </div>
                   
                </div>
            </div>
        </section>`;

        const albumControlTemplate = (meme, isOwner,onDelete) =>{
            if (isOwner == true) {
                return   html`
                <a href="/edit/${meme._id}" class="edit">Edit</a>
                <a @click=${onDelete} href="javascript void(0)" class="remove">Delete</a>`;
            } else {
                return null;
                
            }
        };

export async function detailsPage(ctx) {
    const meme = await getMemeById(ctx.params.id)
    console.log(ctx.params.id);
    const userData = getUserData();
    const isOwner = userData && meme._ownerId == userData.id
    ctx.render(detailsTemplate(meme,isOwner,onDelete))

    async function onDelete() {
        const choice = confirm('Are you sure you want ot delete this album}?')

        if (choice) {
            await deleteById(ctx.params.id)
            ctx.page.redirect('/');
        }
    }
}