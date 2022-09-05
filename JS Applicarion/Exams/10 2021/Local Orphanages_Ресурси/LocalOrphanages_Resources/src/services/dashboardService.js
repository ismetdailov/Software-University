import * as request from './requester.js'
// /data/posts?sortBy=_createdOn%20desc
const baseUrl = 'http://localhost:3030/data/posts'

export const getAll = ()=> request.get(`${baseUrl}?sortBy=_createdOn%20desc`)

export const getOne = (postId) => request.get(`${baseUrl}/${postId}`);

export const  create = (postData) => request.post(baseUrl,postData);

export const edit = (postId,posdData) => request.put(`${baseUrl}/${postId}`,posdData)

export const remove = (postId , posdData)=> request.del(`${baseUrl}/${postId}`, posdData)