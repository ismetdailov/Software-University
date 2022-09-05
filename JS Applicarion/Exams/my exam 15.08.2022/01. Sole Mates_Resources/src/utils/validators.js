

export const postsIsInvalid= (postData)=>{
    const requiredFields = [
         'brand',
         'model',
         'imageUrl',
         'release',
         'designer',
         'value',
    ]
    return requiredFields.some(x=>!postData[x])
}