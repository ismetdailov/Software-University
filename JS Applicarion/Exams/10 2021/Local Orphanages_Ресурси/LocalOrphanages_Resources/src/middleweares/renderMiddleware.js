import { render } from "../../node_modules/lit-html/lit-html.js";
import { navigationView } from "../views/navigationView.js";


const headerElement = document.querySelector('.header-navigtion');
const contentElement = document.querySelector('#main-content');

const contextRender = (templateResult)=> {
    render(templateResult, contentElement)
}
export const renderNavigation = (context, next)=> {
    render(navigationView(context), headerElement)
    next();
}
export const renderContent =(context, next)=>{
    context.render = contextRender;
    next();
}