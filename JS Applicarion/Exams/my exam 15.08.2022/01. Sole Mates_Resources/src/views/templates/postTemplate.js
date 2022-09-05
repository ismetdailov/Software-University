import { html, nothing } from "../../../node_modules/lit-html/lit-html.js";


 const postDetails =(shoeId) => html`
<a class="details-btn" href="/shoes/${shoeId}}">Details</a>
`
export const postTemplate = (shoe, withDetails=true) => html`
<li class="card">
<img src="${shoe.imageUrl}" alt="travis" />
<p>
<strong>Brand: </strong><span class="brand">${shoe.brand}</span>
</p>
<p>
<strong>Model: </strong
><span class="model">${shoe.model}</span>
</p>
<p><strong>Value:</strong><span class="value">${shoe.value}</span>$</p>
${postDetails(shoe._id)}
</li>

`
