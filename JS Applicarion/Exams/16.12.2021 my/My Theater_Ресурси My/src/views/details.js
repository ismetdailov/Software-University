import { deleteById, getLikes,  getMyLikes, getTeatherById, likeTeather } from "../api/data.js";
import { html } from "../lib.js";
import { getUserData } from "../util.js";

const detailsTemplate = (theather,isOwner,onDelete,likes,onLike,showButtonLike) =>html `
         <section id="detailsPage">
            <div id="detailsBox">
                <div class="detailsInfo">
                    <h1>Title: ${theather.title}</h1>
                    <div>
                        <img src=${theather.imageUrl} />
                    </div>
                </div>

                <div class="details">
                    <h3>Theater Description</h3>
                    <p>${theather.description}</p>
                    <h4>Date: ${theather.date}</h4>
                    <h4>Author: ${theather.author}</h4>
                    ${albumTeathersTemplate(theather, isOwner,onDelete)}
                    <div  class="buttons">
                ${likesTemplate(onLike,showButtonLike)}
                    <p class="likes">Likes: ${likes}</p>
                    </div>
                  
                </div>
               
            </div>
        </section>`;

        const albumTeathersTemplate = (theather, isOwner,onDelete) =>{
            if (isOwner == true) {
                return   html`
                <a @click=${onDelete} class="btn-delete" href="javascript:void(0)">Delete</a>
            <a  class="btn-edit" href="/edit/${theather._id}">Edit</a>`
            } else {
                return null;
                
            }
        };
        const likesTemplate = (showButtonLike,onLike)=>{
            if (showButtonLike) {
              
                return html`
                <a @click=${onLike} class="btn-like" href="javascript:void(0)">Like</a>
                `;
            }else{
                return null;
            }

        };

export async function detailsPage(ctx) {
    const userData = getUserData();
    const[theather,likes,haslikes] = await Promise.all([
        getTeatherById(ctx.params.id),
        getLikes(ctx.params.id),
       userData ? getMyLikes(ctx.params.id,userData.id) : 0
    ])
    console.log(ctx.params.id, theather,likes,haslikes);
    const isOwner = userData &&  userData.id== theather._ownerId 
    const showButtonLike = isOwner == false && haslikes == false && userData !=null
  const like =0;
    ctx.render(detailsTemplate(theather,isOwner,onDelete,likes,showButtonLike, onLike))

    async function onDelete() {
        const choice = confirm(`Are you sure you want ot delete this Theather ${theather.title}?`)

        if (choice) {
            await deleteById(ctx.params.id)
            ctx.page.redirect('/');
        }
    }
    async function onLike() {
      await likeTeather(ctx.params.id)
      ctx.page.redirect('/details/'+ctx.params.id);
    }

}