# The Great Auction

Summary:

Use of onion layers architecture. 
EF for DAL, ASP.NET MVC for WEB, Ninject for dependencies resolve.

Some of the stuff still not implemented (like unit tests).

Have to follow these to make it work on a random pc for the first time.
Steps:

webconfig -> change datadirectory to /app path/Auction.WEB/AppData;

90% chance ef messed up again so package manager console -> update-database (enable-migrations & add-migration "NewMigration" if needed);

100% chance you cannot set everything how we need in code first so connect to created db, Lots table -> Image column -> change varbinary(8000) to varbinary(max);

Launch.

admin*
login: killer@gmail.com
pass: Abc=123

ceo*
login: dragon34@gmail.com
pass: Boss(0)

user1*
login: naberlin@gmail.com
pass: Choo-400

user2*
login: another@user.mail
pass: Pass+1

but better register if u need a user
