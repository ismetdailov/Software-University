import{html} from '../lib.js';
import { getAllMemes } from'../api/data.js'
import { getUserData } from '../util.js';

const catalogTemplate = (memes)=> html `
<!--Catalog-->
<section id="catalogPage">
            <h1>All Albums</h1>
         
            ${memes.length == 0
            ? html`<p>No Albums in Catalog!</p>` : memes.map(memeCard)},
                    
                    
<!--No albums in catalog-->
</section>`;
              
				//<!-- Display : If there are no memes in database -->
const memeCard = (meme) => html `
  <div class="card-box">
                <img src=${meme.imgUrl}>
                <div>
                    <div class="text-center">
                        <p class="name">Name: ${meme.name}</p>
                        <p class="artist">Artist: ${meme.artist}</p>
                        <p class="genre">Genre: ${meme.genre}</p>
                        <p class="price">Price: $${meme.price}</p>
                        <p class="date">Release Date: ${meme.releaseDate}</p>
                    </div>
                   <div class="btn-group">
                   ${isLogged() ==true ? html`
                    <a href="/details/${meme._id}" id="details">Details</a>` : ''}   
                    </div>
          
                </div>
            </div>
`;

export async function catalogPage (ctx) {
    const userData = getUserData();

    const memes = await getAllMemes();
    ctx.render(catalogTemplate(memes));
}
function isLogged(){
    if (!sessionStorage.getItem('userData')) {
        return false;
    } 
    return true;
}