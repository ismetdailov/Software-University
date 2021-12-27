import { editMeme, getMemeById } from "../api/data.js";
import { html } from "../lib.js";
const editTemplate = (meme, onSubmit) => html`
<section class="editPage">
    <form @submit=${onSubmit}>
        <fieldset>
            <legend>Edit Album</legend>

            <div  class="container">
                <label for="name" class="vhide">Album name</label>
                <input id="name" name="name" class="name" type="text" .value=${meme.name}>

                <label for="imgUrl" class="vhide">Image Url</label>
                <input id="imgUrl" name="imgUrl" class="imgUrl" type="text" .value=${meme.imgUrl}>

                <label for="price" class="vhide">Price</label>
                <input id="price" name="price" class="price" type="text" .value=${meme.price}>

                <label for="releaseDate" class="vhide">Release date</label>
                <input id="releaseDate" name="releaseDate" class="releaseDate" type="text" .value=${meme.releaseDate}>

                <label for="artist" class="vhide">Artist</label>
                <input id="artist" name="artist" class="artist" type="text" .value=${meme.artist}>

                <label for="genre" class="vhide">Genre</label>
                <input id="genre" name="genre" class="genre" type="text" .value=${meme.genre}>

                <label for="description" class="vhide">Description</label>
                <textarea name="description" class="description" rows="10" cols="10"
                    .value=${meme.description}></textarea>

                <button  class="edit-album"  type="submit">Edit Album</button>
            </div>
        </fieldset>
    </form>
</section>
`;

export async function editPage(ctx) {

    const meme = await getMemeById(ctx.params.id)
    ctx.render(editTemplate(meme, onSubmit));
           
    async function onSubmit(event) {
        event.preventDefault();

        const formData = new FormData(event.target);
        const name = formData.get('name').trim();
        const imgUrl = formData.get('imgUrl').trim();
        const price = formData.get('price').trim();
        const releaseDate = formData.get('releaseDate').trim();
        const artist = formData.get('artist').trim();
        const genre = formData.get('genre').trim()
        const description = formData.get('description').trim()

        if (name === '' || imgUrl === '' || price === '' || releaseDate === '' || artist === '' || genre === '' || description === '') {
            return alert('All fields are required!');
        }

        await editMeme(ctx.params.id, {
            name,
            imgUrl,
            price,
            releaseDate,
            artist,
            genre,
            description,
        });
        ctx.page.redirect('/details/'+ ctx.params.id);
    }
}