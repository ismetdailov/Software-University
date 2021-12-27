import * as api from './api.js';

export const login = api.login;
export const register = api.register;
export const logout = api.logout;

export async function getAllTeahters() {
    return  api.get('/data/theaters?sortBy=_createdOn%20desc&distinct=title')
}
export async function getTeatherById(id) {
    return api.get('/data/theaters/' + id);
}
export async function getMyTeathers(userId) {
    return api.get(`/data/theaters?where=_ownerId%3D%22${userId}%22&sortBy=_createdOn%20desc`);
}

export async function createTeather(teather) {
    return api.post('/data/theaters', teather);
}
export async function editTeather(id, theater) {
    return api.put('/data/theaters/' + id, theater)
}
export async function deleteById(id) {
    return api.del('/data/theaters/' + id)
}
export async function likeTeather(theaterId) {
    return api.post('/data/likes',{
        theaterId
    })
}
export async function getLikes(theaterId) {
    return api.get(`/data/likes?where=theaterId%3D%22${theaterId}%22&distinct=_ownerId&count`)
}
export async function getMyLikes(theaterId, userId) {
    return api.get(`/data/likes?where=theaterId%3D%22${theaterId}%22%20and%20_ownerId%3D%22${userId}%22&count`)
}
window.likeTeather = likeTeather
window.getLikes = getLikes
window.getMyLikes = getMyLikes