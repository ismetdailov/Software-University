import page  from '../../node_modules/page/page.mjs';
import { authMiddleware } from '../middleweares/authMiddawear.js';
import { renderNavigation, renderContent } from '../middleweares/renderMiddleware.js';
import { createView } from './createView.js';
import { dashboardView } from './dashBoardView.js';
import { deletePostView } from './deleteView.js';
import { detailsView } from './detailsView.js';
import { editPostView } from './editPostView.js';
// import { homeView } from './homeView.js';
import { loginView } from './LoginView.js';
import { logoutView } from './logoutView.js';
import { registerView } from './registerView.js';

page(authMiddleware);
page(renderNavigation)
page(renderContent);

page('/', dashboardView)
page('/login',loginView)
page('/register',registerView)
page('/logout', logoutView)
page('/create', createView)
page('/posts/:postId', detailsView)
page('/posts/:postId/edit',editPostView)
page('/posts/:postId/delete', deletePostView)
// page('/dashboard',dashboardView)
page.start();