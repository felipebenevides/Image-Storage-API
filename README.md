
# Image Storage API (API 1)

Routes:

/upload - POST endpoint to upload images. 

/images - GET endpoint to fetch all uploaded images.

/images/:id - GET endpoint to fetch a specific image by ID.

/images/:id/delete - DELETE endpoint to delete a specific image by ID.

Authentication: Basic JWT support using upikrules as the key.

Database: EF Core Memory storing image data.
