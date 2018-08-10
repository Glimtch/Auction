# The Great Auction

summary:

tests - mess;
half of bll - not implemented;
dal - doubtable stuff;
pl - genius frontend (no);
overall - 4/10.

steps:

webconfig -> change datadirectory to app path/Auction.WEB/AppData;
90% chance ef messed up again so package manager console -> update-database (enable-migrations & add-migration "NewMigration" if needed);
100% chance you cannot set everything how we need in code first so connect to created db, Lots table -> Image column -> change varbinary(8000) to varbinary(max);
launch and hope it works on your pc coz its been a disaster for me.

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
