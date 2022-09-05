import * as authService from '../services/authService.js';

export const authMiddleware = (context,next)=>{
    context.user = authService.getUser();
    next();
}