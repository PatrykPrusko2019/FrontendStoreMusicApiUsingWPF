A detailed description of the application with photos is in the directory: FrontEndStoreMusicAPI
/DescriptionOfApp.

FRONTEND WPF APPLICATION FOR STORE MUSIC API APPLICATION:

Used: C#, WPF, .NET 6.0, Entity Framework, MS_SQL, Swagger(main) documentation.
Page), Microsoft Azure cloud, NLogger, JWT Token, Postman, CORS policy, Middleware.

The desktop application allows you to browse (without logging in) as a guest, then you can only
Browse all artists, albums and songs. You can log in as USER -> who can only browse everything, 
without access to the ContactNumber and ContactEmail fields of a given Artist. Then you can log in 
as PREMIUM_USER -> the ability to create your Artists with unique names so that they don't 
duplicate in the list that already exists for that user.
Create albums only for your artist and songs for your album. Artist, Album, Song names can 
duplicate between users, that is, the first user will create an artist named Iron Maiden 1 with id -> 1, 
and the second user will create an artist named Iron Maiden 1 with id -> 2, this situation is allowed. 
It is possible to log in as ADMIN -> create, delete, add, view, only if it creates the name of the artist, 
Album, Song then it must be a unique name. In the login window, you can register and after logging 
in, each user has a generated JWT Token through which authentication is checked to see if they 
have access to the activity. For example, a user who has the role USER, PREMIUM_USER when he 
wants to update an artist, album, song not created by him, create a new song, album in an artist not 
created by him, an error message -> Status code: 403 -> Forbidden will occur. If logged in as a guest 
wants to access an option forbidden for him then he gets a message that he is not
logged in and has no access because he does not have the given JWT Token.
In the User Details -> there are details of a given user and the artists he created, plus additional data 
ContactEmail and ContactNumber.
In the Show Artists window, you can add, update, delete, see details of a particular artist. You can use 
pagination: Record per page -> 5, 10, 15 number of records per page. 1 of 30 -> 1-> current page, 30-> 
number of pages of all, current page changes relative to the selection of a page with additional 
buttons '>'-> nextPage, '<'-> PreviousePage, '>>'-> LastPage, '<<'-> FirstPage. Ability to sort using 
SortBy-> against 3 columns: Name, Description, KindOfMusic, SortDirection -> ASC: ascending, DESC: 
descending sorts by the given column selected. Search button -> searches for a given word by Name 
column. Everything works when the Search button is pressed.
In the Show Albums and Songs window it works similarly, in addition you can use the Delete All -> 
button.
deleting all records. The Show All Albums -> window automatically displays all albums from the 
database. The Show All Songs -> window automatically displays all songs from the database. The 
Show All Artists -> window automatically displays all the artists in the database. When you return 
from the ShowAllSongsWindow to the ShowAlbumsWindow, it
automatically display all the albums of a given artist. Using UpdateCreateArtist, UpdateCreateAlbum, 
UpdateCreateSong window -> for two actions, to create and update. Using ShowAllAlbumsWindow 
to display all albums from the database or to
Displaying all the albums of a given artist. Using the ShowAllSongsWindow to
to display all songs in the database or to display all songs of a given
album.
When hovering the mouse cursor over any button, text box, additional information appears what 
the button is responsible for. The ability to connect the Frontend application directly to the web 
application , which is on the Azure cloud, at the link : https://musicstore-api-app9.azurewebsites.net, you just need to change the uri field in the 
Utilites/HelperHttpClient.cs directory. During the first launch locally will create records for testing 
and 3 users also for testing.

Additionally created / changed things in the Backend Application :
    1. Adding a new endpoint -> GET api/login/user/{email} -> to access the user's details and the generated JWT Token, after logging into the main Music Store window.
    2. Adding a new endpoint -> GET api/login/user/{userId}/artist -> to have access after logging into the main Music Store window to all the artists created by a given user and detailed information to display.
    3. Adding a new endpoint -> GET api/song -> retrieves all songs from the database.
    4. Adding a new endpoint -> GET api/album -> retrieves all albums from the database.
    5. Changing the sorting of data, if you do not select ASC (1)-> ascending or DESC (2) descending and SearchWord, it does not sort.
    6. Add a new endpoint -> GET api/artist/{artistId}/album/{albumId}/song/details/{songId} -> to access the details of a particular song with additional fields: AlbumTitle, ArtistId, ArtistName.
    7. Add a new endpoint -> GET api/artist/{artistId}/album/details/{albumId} -> to access the details of a given album with additional fields: ArtistName, ArtistId.
    8. Add a new endpoint -> GET api/artist/details/{Id} -> to access the details of a particular artist with additional fields: ContactEmail, ContactNumber.
