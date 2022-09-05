

export const postsIsInvalid= (postData)=>{
    const requiredFields = [
         'title',
         'description',
         'imageUrl',
         'address',
         'phone',
    ]
    return requiredFields.some(x=>!postData[x])
}