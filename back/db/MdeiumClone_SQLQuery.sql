use MediumCloneDb


drop table [dbo].[Register]
go
create table [dbo].[CurrentUser]
(
   [Id] [int] identity(1,1) not null,
   [username] [nvarchar](200) not null,
   [email] [nvarchar](300) not null,
   [createdAt] [nvarchar](50) not null,
   [updatedAt] [nvarchar](50) not null,
   [bio] [nvarchar](2000) null,
   [image] [nvarchar](150) null
   constraint [PK_CurrentUser] primary key clustered
   (
      [Id] asc
   )
)
go
alter table [dbo].[CurrentUser]
add [password] nvarchar(1000)
go
alter table [dbo].[CurrentUser]
alter column [password] nvarchar(1000) not null
go
select *
from CurrentUser
go

create procedure SelectAllCurentUsers
as 
   select *
   from [dbo].[CurrentUser]
go

execute SelectAllCurentUsers
go

create procedure SelectCurentUserById @Id int
as
  select *
  from [dbo].[CurrentUser]
  where Id = @Id
go
execute SelectCurentUserById @Id = 2
go
create procedure AddCurentUser (
  @username nvarchar(200), 
  @email nvarchar(300), 
  @password nvarchar(1000),
  @createdAt nvarchar(50),
  @updateAt nvarchar(50), 
  @bio nvarchar(2000), 
  @image nvarchar(150)
)
as
   begin
	   insert into [dbo].[CurrentUser] (username, email, [password], createdAt, updatedAt, bio, [image])
			  values (@username, @email, @password, @createdAt, @updateAt, @bio, @image)
   end
go
drop procedure AddCurentUser
go
execute AddCurentUser @username = 'pavle123', @email = 'pako@gmail.com', @password='PaKo222#',
						@createdAt='21-12-2019', @updateAt = '01-08-2021', @bio='My biography was deleted. SORRY!!!', @image = null
							
go

delete from [dbo].[CurrentUser]
go