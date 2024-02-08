use SecureAppDB
go

DECLARE @i int = 1;
DECLARE @id varchar(10)

WHILE @i < 6
BEGIN
  set @id=cast(@i as nvarchar);

  Insert into dbo.[User](id,name,password) values('u'+@id,'test'+@id,'111111');
  Insert into dbo.UserProfile(id,UserId,dob,Adhar) values('p'+@id,'u'+@id,DATEADD(year, -20, CAST(GETDATE() AS date)),'100000000000') 
  
   set @i=@i+1;
END

 select * from dbo.[User]
  select * from dbo.UserProfile
	

