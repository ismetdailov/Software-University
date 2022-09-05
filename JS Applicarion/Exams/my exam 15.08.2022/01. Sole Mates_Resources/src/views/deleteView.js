import * as postService from '../services/dashboardService.js'

export const deletePostView = async (context) => {
    try {
        const shoe = await postService.getOne(context.params.postId)

        let confirmed = confirm(`Do you want to delete the post: ${shoe.title}`);

        if (confirmed) {
            await postService.remove(context.params.postId);
            
            context.page.redirect('/dashboard');
        }
    } catch (error) {
        alert(error)
    }

}