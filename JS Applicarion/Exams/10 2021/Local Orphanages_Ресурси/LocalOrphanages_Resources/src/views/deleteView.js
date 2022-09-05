import * as postService from '../services/dashboardService.js'

export const deletePostView = async (context) => {
    try {
        const post = await postService.getOne(context.params.postId)

        let confirmed = confirm(`Do you want to delete the post: ${post.title}`);

        if (confirmed) {
            await postService.remove(context.params.postId);
            
            context.page.redirect('/');
        }
    } catch (error) {
        alert(error)
    }


}