# CognAC League of Legends Tournament 2015 website

[![Build status](https://ci.appveyor.com/api/projects/status/wm953ob1fprdfc9c?svg=true)](https://ci.appveyor.com/project/StevenReitsma/lolcognac-website)

Run a MongoDB server on the background on the default port without username or password. Since it's a dynamic document store you don't need to insert a database structure, it's created on the fly when a document is first inserted.
Please make sure all methods you use are compatible with Mono, since the website is running on a Linux server with a modified version of Mono 3.99 (https://github.com/StevenReitsma/mono/tree/MVC5) with mod_mono and Apache. I've heard that problems primarily arise when using the async and await keywords. I haven't tested this myself though.
