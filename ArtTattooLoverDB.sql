Create Database ArtTattooLover;
use ArtTattooLover;
CREATE TABLE TattooLover(
TattooLoverID int IDENTITY(1,1) primary key,
Email nvarchar(255),
PhoneNumber nvarchar(255),
Age int
)

CREATE TABLE Studio(
StudioID int IDENTITY(1,1) primary key,
Name nvarchar(255),
Address nvarchar(255),
Phone nvarchar(255),
Website nvarchar(255),
)

Create TABLE Style(
StyleID int IDENTITY(1,1) primary key,
StyleName nvarchar(255),
)
CREATE TABLE Artists(
ArtistID int IDENTITY(1,1) primary key,
Fullname nvarchar(255),
MainStyle int foreign key (MainStyle) REFERENCES Style(StyleID),
Phone nvarchar(255),
StudioID int foreign key (StudioID) REFERENCES Studio(StudioID),
)

CREATE TABLE TattoosDesign(
TattoosDesignID int IDENTITY(1,1) primary key,
TattoosDesignName nvarchar(255),
StyleID int foreign key (StyleID) REFERENCES Style(StyleID),
ImgURI nvarchar(255),
Description nvarchar(255),
ArtistID int foreign key (ArtistID) REFERENCES Artists(ArtistID)
);

CREATE TABLE Service(
ServiceID int IDENTITY(1,1) primary key,
ServiceName nvarchar(255),
Price money,
StudioID int foreign key  (StudioID) REFERENCES Studio(StudioID)
)

CREATE TABLE Staff(
StaffID int IDENTITY(1,1) primary key,
StaffName nvarchar(255),
StaffPhone nvarchar(255),
StudioID int foreign key  (StudioID) REFERENCES Studio(StudioID),
)

CREATE TABLE Schedule(
ScheduleID int IDENTITY(1,1) primary key,
Time date,
ArtistID int foreign key (ArtistID) REFERENCES Artists(ArtistID),
)

--1:waiting 2:progress 3:done 4:cancel
CREATE TABLE Appointments(
AppointmentID int IDENTITY(1,1) primary key,
TotalPrice money,
Status int,
TattooLoverID int foreign key (TattooLoverID) REFERENCES TattooLover(TattooLoverID),
ScheduleID int foreign key (ScheduleID) REFERENCES Schedule(ScheduleID),
StudioID int foreign key  (StudioID) REFERENCES Studio(StudioID)
);

CREATE TABLE AppointmentDetail(
AppointmentDetailID int IDENTITY(1,1) primary key,
Price money,
AppointmentID int foreign key (AppointmentID) REFERENCES Appointments(AppointmentID),
ServiceID int foreign key  (ServiceID) REFERENCES Service(ServiceID)
)

CREATE TABLE Certificate(
certificateID int IDENTITY(1,1)  primary key,
certificateName nvarchar(255),
)

CREATE TABLE Certificate_Artists(
certificateID int foreign key  (certificateID) REFERENCES Certificate(certificateID),
ArtistID int foreign key (ArtistID) REFERENCES Artists(ArtistID)
)

CREATE TABLE Login(
username varchar(255) primary key,
password varchar(255) not null,
TattooLoverID int foreign key (TattooLoverID) REFERENCES TattooLover(TattooLoverID),
ArtistID int foreign key (ArtistID) REFERENCES Artists(ArtistID),
StaffID int foreign key (StaffID) REFERENCES Staff(StaffID),
)

-- --------------------add value -----------------------
INSERT INTO TattooLover(Email,PhoneNumber,Age)
VALUES 
('userna1me@gmail.com','0549584656',35),
('username2@gmail.com','0549584656',25),
('username3@gmail.com','0519584656',27),
('username4@gmail.com','0549584856',19)

insert into Certificate(certificateName)
values ('CPR Certification'),('First Aid Certification'),('Bloodborne Pathogen Certificate'),('OSHA Safety Certificate')

insert into Studio(Address,Name,Phone,Website)
values
('address1','studio1','0362569562','studio1.com'),
('address2','studio2','0362558562','studio2.com'),
('address3','studio3','0366929562','studio3.com')

insert into Style(StyleName) Values ('modern'),('gaming'),('street'),('freestyle'),('ancient')

insert into service(ServiceName,StudioID,Price)
values 
('advise',1,250000),
('invite artist for tattooing',1,300000),
('advise',2,265000)

insert into Staff(StaffName,StaffPhone,StudioID)
values 
('staffstudio1','0685956345',1),
('staff2studio1','0349856521',1),
('staffstudio2','0139737563',2),
('staffs3','0848484848',3)

insert into Artists(Fullname,Phone,StudioID,MainStyle)
values 
('Neas Gronw','0684558741',1,1),
('Ríoghnach Medb','0658742598',2,1),
('Scáthach','0626594874',3,3)

insert into Certificate_Artists(ArtistID,certificateID) values (1,2),(1,1),(2,3),(3,1)

insert into Schedule(ArtistID,Time)
values 
(1,'12-10-2023'),
(2,'10-28-2023'),
(1,'10-25-2023'),
(3,'1-22-2024'),
(1,'11-1-2023'),
(1,'9-1-2023')
insert into TattoosDesign(TattoosDesignName,ArtistID,Description,StyleID)
values 
('design1',1,'Description for design1',1),
('design2',1,'Description for design2',3),
('design3',3,'Description for design3',3),
('design3v2',3,'Description for design3v2',1),
('design',2,'Description for design',2)

insert into Appointments(TattooLoverID,StudioID,ScheduleID,Status,TotalPrice)
values 
(3,1,6,3,550000),
(2,1,4,1,850000)
insert into AppointmentDetail(AppointmentID,Price,ServiceID)
values
(1,1,250000),
(1,2,300000),
(2,1,250000),
(2,2,300000),
(2,2,300000)

INSERT INTO Login(username,password,TattooLoverID,ArtistID,StaffID)
values 
('userna1me','username',1,null,null),
('username2','username',2,null,null),
('username3','username',3,null,null),
('username4','username',4,null,null),
('staffstudio1','staff',null,null,1),
('staff2studio1','staff',null,null,2),
('staffstudio2','staff',null,null,3),
('staffs3','staff',null,null,4),
('neasgronw','artists',null,1,null),
('rioghnachmedb','artists',null,2,null),
('scathach','artists',null,3,null)