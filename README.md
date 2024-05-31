A Web API that allows users to upload a file to the server and download it back via the http protocol.
- Users must be authenticated and authorized. (JWT)
- There are two types of users: regular and admin.
  Regular users can only download their own documents, while admin can download everything.
- The connection to the postgre base is via EF.
