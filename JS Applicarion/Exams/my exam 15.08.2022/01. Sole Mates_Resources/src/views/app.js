// import page  from '../../node_modules/page/page.mjs';
import page  from '../../node_modules/page/page.mjs';
import { authMiddleware } from '../middleweares/authMiddawear.js';
import { renderNavigation, renderContent } from '../middleweares/renderMiddleware.js';
import { createView } from './createView.js';
import { dashboardView } from './dashboardView.js';
import { deletePostView } from './deleteView.js';
import { detailsView } from './detailsView.js';
import { editPostView } from './editPostView.js';
import { homeView } from './homeView.js';
import { loginView } from './loginView.js';
import { logoutView } from './logoutView.js';
import { registerView } from './registerView.js';
import { searchView } from './searchView.js';




page(authMiddleware)
page(renderNavigation)
page(renderContent)

page('/',homeView)
page('/login',loginView)
page('/register',registerView)
page('/dashboard',dashboardView)
page('/logout',logoutView)
page('/add',createView)
page('/search', searchView)
page('/shoes/:postId', detailsView)
page('/shoes/:postId/edit',editPostView)
page('/shoes/:postId/delete', deletePostView)

page.start();