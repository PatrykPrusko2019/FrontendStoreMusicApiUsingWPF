A detailed description of the application with photos is in the directory: FrontEndStoreMusicAPI
/DescriptionOfApp.

FRONTEND WPF APPLICATION FOR STORE MUSIC API APPLICATION:

Used: C#, WPF, .NET 6.0, Entity Framework, MS_SQL, Swagger(homepage) documentation, Microsoft Azure cloud, NLogger, JWT Token, Postman, CORS policy, Middleware.

The desktop application allows you to log in without logging in as a guest, in which case you can only browse all Artists, albums and songs. You can log in as USER -> who can only browse everything, without access to the ContactNumber and ContactEmail fields of the Artist in question. Then you can log in as PREMIUM_USER -> the ability to create your Artists with unique names, so that they don't duplicate in the list already existing with that user. Create albums only for your artist and songs for your album. Names of artists, albums, songs can be duplicated between users, i.e. the first user creates an artist named Iron Maiden 1 with id -> 1, and the second user creates an artist named Iron Maiden 1 with id -> 2 , this situation is acceptable. You can log in as ADMIN -> create , delete , add , view, only if it creates the name of the artist, Album, Song it must be unique.
In the login window, you can register and when you log in, each user has a generated JWT Token, through which authentication is checked to see if they have access to the activity. For example, a user who has the role USER, PREMIUM_USER , when he wants to update a non-his artist, album, song, create a new song, album in a non-his artist, there will be an error message -> Status code: 403 -> Forbidden. If logged in as a guest wants to access an option forbidden to him then he gets a message that he is not logged in and has no access, because he does not have the given JWT Token. 
In the User Details -> there are the details of the user in question and all his artists created, plus additional data ContactEmail and ContactNumber.
In the Show Artists window you can add, update, subtract , see the details of a particular artist. You can use pagination: Record per page -> 5, 10, 15 number of records per page. 1 of 30 -> 1-> current page, 30-> number of pages of all, current page changes relative to the selection of a page with additional buttons '>'-> nextPage, '<'-> PreviousePage, '>>'-> LastPage, '<<'-> FIrstPage. Ability to sort using SortBy-> against 3 columns:  Name, Description, KindOfMusic, SortDirection -> ASC: ascending, DESC: descending sorts by the given column selected. Search button -> searches for a given word by Name column. Everything works when the Search button is pressed. 
The Show Albums and Songs window works similarly, in addition, you can use the Delete All -> button to delete all records.
The Show All Albums -> window automatically displays all the albums in the database.
The Show All Songs -> window automatically displays all the songs in the database.
The Show All Artists -> window automatically displays all the artists in the database.
When you return from the ShowAllSongsWindow to the ShowAlbumsWindow, it automatically displays all the albums of a particular artist.
Using the UpdateCreateArtist, UpdateCreateAlbum, UpdateCreateSong -> window for two actions, to create and to update. Using ShowAllAlbumsWindow to display all albums from the database, or to display all albums of a given artist. Using ShowAllSongsWindow to display all songs from the database, or to display all songs of a given album.
When hovering the mouse cursor over what buttons, text box, this is an additional information about what the button does. The ability to connect the Frontend application directly to the web application , which is on the Azure cloud, at the link : https://musicstore-api-app9.azurewebsites.net , only you need to change the uri field in the directory Utilites/HelperHttpClient.cs. When you run it locally for the first time , it will create records for testing and 3 users also for testing.

Additionally created / changed things in the Backend Application :
    1. Adding a new endpoint -> GET api/login/user/{email} -> to access the user's details and the generated JWT Token, after logging into the main Music Store window.
    2. Adding a new endpoint -> GET api/login/user/{userId}/artist -> to have access after logging into the main Music Store window to all the artists created by a given user and detailed information to display.
    3. Adding a new endpoint -> GET api/song -> retrieves all songs from the database.
    4. Adding a new endpoint -> GET api/album -> retrieves all albums from the database.
    5. Changing the sorting of data, if you do not select ASC (1)-> ascending or DESC (2) descending and SearchWord, it does not sort.
    6. Add a new endpoint -> GET api/artist/{artistId}/album/{albumId}/song/details/{songId} -> to access the details of a particular song with additional fields: AlbumTitle, ArtistId, ArtistName.
    7. Add a new endpoint -> GET api/artist/{artistId}/album/details/{albumId} -> to access the details of a given album with additional fields: ArtistName, ArtistId.
    8. Add a new endpoint -> GET api/artist/details/{Id} -> to access the details of a particular artist with additional fields: ContactEmail, ContactNumber.
