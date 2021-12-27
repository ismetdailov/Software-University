import { page, render } from './lib.js'
import { homePage } from './views/home.js';
import { logout } from './api/data.js';
import { loginPage } from './views/login.js';
import { getUserData } from './util.js';
import { registerPage } from './views/register.js';
import { catalogPage } from './views/catalog.js';
import { createPage } from './views/create.js';
import { detailsPage } from './views/details.js';
import { editPage } from './views/edit.js';
import { searchPage } from './views/search.js';


// import { loginPage } from './views/login.js';
// import { registerPage } from './views/register.js';
// import { detailsPage } from './views/details.js';
// import { editPage } from './views/edit.js';
// import { profilePage } from './views/profile.js';

const root = document.getElementById('main-content');
document.querySelectorAll('li')[5].addEventListener('click', onLogout);

page(decorateContext);
page('/', homePage);
page('/catalog', catalogPage);
page('/details/:id', detailsPage);
page('/login', loginPage);
page('/register', registerPage);
page('/create', createPage)
page('/edit/:id', editPage)
page('/search', searchPage)
// page('/profile', profilePage)

updateUserNav();
page.start();

function decorateContext(ctx, next) {
    //tozi render e ot lib html
    ctx.render = (content) => render(content, root)
    ctx.updateUserNav = updateUserNav

    next();
}

function onLogout() {
    logout();
    updateUserNav();
    page.redirect('/');

}

function updateUserNav() {
    const userData = getUserData();
    if (userData) {
        document.querySelectorAll('li')[4].style.display = 'inline-block';
        document.querySelectorAll('li')[5].style.display = 'inline-block';
        document.querySelectorAll('li')[2].style.display = 'none';
        document.querySelectorAll('li')[3].style.display = 'none';
    } else {
        document.querySelectorAll('li')[4].style.display = 'none';
        document.querySelectorAll('li')[5].style.display = 'none';
        document.querySelectorAll('li')[2].style.display = 'inline-block';
        document.querySelectorAll('li')[3].style.display = 'inline-block';
    }
}