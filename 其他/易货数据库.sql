--�׻����ݿ�
create database  YihuoDB

use YihuoDB

if OBJECT_ID('Admin_Info') is not null 
	drop table Admin_Info
go
create table Admin_Info--����Ա��
(
	Admin_ID int primary key ,--����ԱID 
	Admin_Login varchar(20) unique ,--��¼�˺�
	Admin_Pass Varchar(20) not null ,--��¼����
	Admin_Status bit check(Admin_Status = 1 or Admin_Status = 0 or Admin_Status = 2) --����Ա״̬
)
go

insert into Admin_Info values(101,'admin','ny78965',1),
							 (102,'Mking','gd66445',1),
							 (103,'Yang','wy13136',2)
go	

select *from Admin_Info					 


if OBJECT_ID('User_Info') is not null
	drop table User_Info
go
create table User_Info--�û���Ϣ��
(
	"User_ID" int identity(999,1) primary key ,--�û�ID
	User_Login varchar(20) unique , --��¼�˺�
	User_Pass varchar(20) not null, --��¼����
	User_Phone varchar(11) not null , --�ֻ���
	"User_Name" varchar(50) not null , --�û��ǳ�
	User_Status bit check(User_Status = 0 or User_Status = 1) --�û�״̬
)
go

--insert into User_Info values(@User_ID,@User_Login,@User_Pass,@User_Phone,@User_Name,@User_Status)

insert into User_Info values('2045896354','ye78965','14578965874','С��',0),
							('6987456321','yu478965','14789654789','С��',0),
							('5896247896','ling45678','1479632547','С��',0),
							('3698745896','wang2589','15896547896','С��',0),
							('5896321474','Lv478965','14789635478','С��',0),
							('9685478965','xa789658','15896347889','��ΰ',0),
							('1478965785','wan78965','18963578956','�³�',0)
							
go

insert into User_Info values('admin','sy6541230','1547896352','ѧ��',0)

update User_Info set User_Pass='654123' where User_Login='admin'

select * from User_Info

select * from User_Info  where User_Login ='admin'


select * from User_Info where User_Login = '2045896354' and User_Pass= 'ye78965'

if OBJECT_ID('Used_Goods_Type') is not null 
	drop table  Used_Goods_Type
go

create table Used_Goods_Type --��Ʒ���ͱ�
(
	"Type_ID" int primary key identity(1000,1) ,--���ͱ��
	"Type_Name" varchar(50) not null ,--��������
	Type_Description varchar(100) ,--��������
)
go

insert into Used_Goods_Type values('�Ӽ�����','����ʹ��'),
								  ('������Ʒ','���Ӳ�Ʒ'),
								  ('�����鼮','�Ļ��鼮'),
								  ('�˶�����','�˶�װ��'),
								  ('���','���ӱ���'),
								  ('����/�ܱ�','�������ְ�'),
								  ('�������','�����')
go

select *from Used_Goods_Type
select * from User_Info

if OBJECT_ID('Used_Goods_Info') is not null 
	drop table  Used_Goods_Info
go

create table Used_Goods_Info --������Ʒ��
(
	Goods_ID int identity(1000,1) primary key, --��Ʒ���
	Goods_Name varchar(50) not null ,--��Ʒ����
	Goods_Type_ID int foreign key references Used_Goods_Type("Type_ID"),--��Ʒ����
	Goods_Price money not null , --��Ʒ�۸�
	Goods_Seller int foreign key references User_Info("User_ID") ,--�ϼ���
	Goods_Status bit check(Goods_Status = 0 or Goods_Status = 1) , --��Ʒ״̬������/�¼ܣ�	
	Goods_Description varchar(100) ,--��Ʒ����
	Goods_Date datetime default current_timestamp, --�ϼ�����
	Goods_picture varchar(200)
	
)
go                                                                                                                 

select Goods_ID,Goods_Name,Goods_Type_ID,Goods_Price,Goods_Seller,Goods_Status,Goods_Description, Goods_Date ,Goods_picture from Used_Goods_Info where Goods_Type_ID=1001
select * from Used_Goods_Info where Goods_ID=1000
select * from Used_Goods_Info where Goods_ID=1001

 
insert into Used_Goods_Info values('�ȵÿ�',1000,15,999,0,'�ȵÿ���ˮ�����Ȱ���ˮ������ԡ��ԡͰר�ò����','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\R-C.jfif'),
								  ('������ֽ���',1000,10,1000,0,'������ֽ��в�������ֽ���ԡ������ܲ�ֽ�б�','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\O1CN01jXuYXG1XbTk6HIBL3_!!0-item_pic.gif'),
								  ('�����������',1000,20,1000,0,'����������� ����̫��������','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\R-B.jfif'),              --�Ӽ�������Ϣ
								  ('Ь��',1000,40,1000,0,'Ь�ܼ�����Ь��Լ������������Ь��','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\R-C (1).jfif')
go

insert into Used_Goods_Info values('�޼�G502',1001,300,1001,0,'HERO���������������Ϸ���HERO����RGB���羺��� 25600DPI','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\20190715170328_3592.jpg'),
								  ('������EDIFIER',1001,500,1001,0,'����������������ͷ��ʽ����','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\R-C (2).jfif'),
								  ('����S198Pro',1001,400,1001,0,'BOX�����е����','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\R-C (3).jfif'),              --������Ʒ
								  ('��ΪWATCHGT3',1001,1200,1004,0,'��Ϊ�ֱ��˶������ֱ�Ѫ�����46mm��ɫ','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\R-C (4).jfif')
go

insert into Used_Goods_Info values('ʱ���ʷ',1002,20,1004,0,'����������֮һ��ʱ���ʷ','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\012c4e5bfe6547a80121ab5df7a66a.jpg@1280w_1l_2o_100sh.jpg'),
								  ('����',1002,30,1005,0,'���ţ���װ�棬�໪��������','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\view.jfif'),
								  ('ƽ��������',1002,80,1004,0,'ƽ�������磺ȫ���ᣨȫ��2021�棬é����ѧ������Ʒ����������Ĳ��ྭ�䣩','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\v2-245f832654ac6ec8a4f4481439eaf6b0_720w.jpg'),              
								  ('������',1002,50,1002,0,'��ʮ��é����ѧ������Ʒ�������䣨��װ��3�ᣩ','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\bd1671f242588819.jpg') --�����鼮
go

insert into Used_Goods_Info values('����LI-NING',1003,60,1002,0,'���������˶���˿�������˶�����','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\f35bd687e16568c8.jpg'),
								  ('���ʿ',1003,60,1002,0,'���ʿ�������������������԰���ø����������˶�����','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\R-C (5).jfif'),
								  ('KONKA���ѽ�Ĥǹ',1003,80,1002,0,'��Ĥǹ��Ħ���綯���㼡�����������Ƶ�𶯾�Ĥǹ','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\R-C.png'),              
								  ('������',1003,130,1003,0,'������ɳ��ɢ����ʽ���ó���ȭ��ɳ�������̶�ͯ��ȭ��ѵ������','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\285376452794558047402460_x.jpg') --�˶�����
go

insert into Used_Goods_Info values('��ƽ��8��',1004,200,999,0,'������������������������24Ӣ��','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\de5a-kmvwsvy6694030.jpg'),
								  ('���ۿ��������������а�',1004,150,1004,0,'���ۿ������������������а�ţ��','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\5cdd7661N0eadbb05.jpg'),
								  ('�������е�ɽ��˫���',1004,80,1004,0,'�������е�ɽ��˫���60L��Ů�����������˶����а�','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\_-1x-1.jpg'),              
								  ('����LI-NING����',1004,130,1002,0,'���� LI-NING �ܲ�������Ůͨ�ö๦���ֻ�����������ˮ','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\534b292ecf47b4de.jpg') --���
go

insert into Used_Goods_Info values('����',1005,50,1003,0,'���������ȶ���Ԫԭ������̴������������Ҹ��궯���ܱ�','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\6e3b5c7b5e98be7f.jpg'),
								  ('����δ��',1005,10,1003,0,'����δ���ܱ�3D����󺣱�����Ԫvocaloid/V�� ����ѧ������ǽֽװ��','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\c7e23fc28374a48d.jpg'),
								  ('�ְ칫�пڴ�����',1005,300,1002,0,'�������汦������С����Ƥ�����ְ칫�пڴ��������ģ�Ͱڼ� ','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\5aeac352N4cd65844.jpg'),              
								  ('ӣ�Ҽ���',1005,150,1002,0,'ӣ�ң�CHERRY��MX 1.0 ������ POKEMON Ƥ��������','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\572b7c27d343921c.jpg') --�����ܱ�
go

insert into Used_Goods_Info values('���',1006,60,1005,0,'���רҵ��ѧ���ƺ�ѿ��������������������ѧ����','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\60cdf01f11142f14.png'),
								  ('��ɭ��˹',1006,500,1002,0,'��ɭ��Minsine��FD��˹/��ɫ�������ҵ籴˾��ѧ������BASS','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\778757feb9eac503.jpg'),
								  ('�����',1006,650,1002,0,'ӡ�������ҥ���� ��������� ��ѧ����41Ӣ����Ůľ����jita����','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\cacea621ad015d3d.jpg'),              
								  ('�汦��С����',1006,330,1002,0,'С���ٳ�ѧ�߳��˶�ͯʵľ���ſ� �ֹ�����ѧ�����ֿ�����ϰ���� ','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\5a0c0819Nd66d2f13.jpg') --�������
go


select *from Used_Goods_Type
select * from User_Info
select* from Used_Goods_Info

if OBJECT_ID('UG_Concerns') is not null 
	drop table  UG_Concerns
go

create table UG_Concerns --�ο͹�ע��
(
	UG_ID int primary key identity(999,1),--��ע���
	Goods_ID  int foreign key references Used_Goods_Info(Goods_ID) , --��ע��Ʒ���
	"User_ID" int foreign key references User_Info("User_ID") ,--��ע�οͱ��
	
)
go

insert into UG_Concerns values(1017,1004),
							   (1024,1005),
							    (1025,1002)
go

select * from UG_Concerns


if OBJECT_ID('Trade_Record') is not null 
	drop table  Trade_Record
go
create table Trade_Record --���׼�¼��
(
	Trade_ID int primary key identity(999,1) ,--���ױ��
	Trade_Seller int foreign key references User_Info("User_ID") ,--������ұ��
	Trade_Buyers int foreign key references User_Info("User_ID") ,--�����ұ��
	Trade_Date datetime default current_timestamp ,--��������
	Trade_Description varchar(100) ,--���ױ�ע
	Goods_ID int foreign key references Used_Goods_Info(Goods_ID),--������Ʒ
	Trade_Price money not null, --���׽��
)
go

insert into Trade_Record values(1004,1001,'2022/06/14','�ļ�Ǯ',1008,20)
insert into Trade_Record values(999,1003,'2022/06/14','��',1000,15)
insert into Trade_Record values(1000,1002,'2022/06/14','��',1002,20)

select * from Trade_Record 

select *from Trade_Record

if OBJECT_ID('Trade_Conduct') is not null 
	drop table  Trade_Conduct
go

create table Trade_Conduct --���׽��б�
(
	Conduct_ID int identity(999,1) primary key, --���б��
	Trade_Seller int foreign key references User_Info("User_ID") ,--������ұ��
	Trade_Buyers int foreign key references User_Info("User_ID") ,--�����ұ��
	Goods_ID int foreign key references Used_Goods_Info(Goods_ID),--������Ʒ
	Trade_Price money not null, --���׽��
	Trade_Finish bit check(Trade_Finish = 0 or Trade_Finish = 1),--�Ƿ����
	Trade_Abnormal bit check(Trade_Abnormal = 0 or Trade_Abnormal = 1)--�Ƿ��쳣
	
)
go

insert into Trade_Conduct values(1004,1001,1008,20,0,0)
insert into Trade_Conduct values(999,1003,1000,15,0,0)
insert into Trade_Conduct values(1000,1002,1002,20,0,0)

select A.User_ID,A.User_Name,B.Goods_ID,B.Goods_Name,B.Goods_Price,B.Goods_Description,B.Goods_picture,B.Goods_Seller,c.Trade_Finish,c.Trade_Abnormal from User_Info A,Used_Goods_Info B,Trade_Conduct C 
where c.Trade_Seller=A.User_ID and c.Conduct_ID=B.Goods_ID and c.Trade_Finish=0

update Trade_Conduct set Trade_Finish=1

select* from Trade_Conduct
select * from Used_Goods_Info
select * from Trade_Record

if OBJECT_ID('Browse_Record') is not null 
	drop table  Browse_Record
go

create table Browse_Record --�����¼��
(
	Browse_ID int identity(999,1) primary key, --��¼���
	Goods_ID int foreign key references Used_Goods_Info(Goods_ID),--��ע��Ʒ���
	"User_ID" int foreign key references User_Info("User_ID") --��ע�οͱ��
)
go
insert into Browse_Record values(1017,1004),
							   (1024,1005),
							    (1025,1002)
go


if OBJECT_ID('Used_Goods_Sold') is not null 
	drop table  Used_Goods_Sold
go

create table Used_Goods_Sold --������Ʒ��
(
	Sold_ID int identity(999,1) primary key ,--���۱��
	Sold_Goods_ID int foreign key references Used_Goods_Info(Goods_ID) ,--������Ʒ���
	Sold_Date datetime default current_timestamp --�¼�ʱ��
	
)
go

insert into Used_Goods_Sold values(1008,'2022/06/15'),
								  (1000,'2022/06/16'),
								  (1002,'2022/06/18')
go

if OBJECT_ID('Private_Chat_Record') is not null
	drop table Private_Chat_Record
go

create table Private_Chat_Record --˽�ļ�¼��
(
	Record_ID int identity(1000,1) primary key , --˽�ļ�¼���
	"User_FA" int foreign key references User_Info("User_ID") ,--������
	"User_SO" int foreign key references User_Info("User_ID") ,--������
	Chat_HIstory text ,--��Ϣ����
	P_Time datetime ,--����ʱ��
	P_Status bit check(P_Status = 0 or P_Status = 1) --����״̬
)
go

insert into Private_Chat_Record values(1001,1004,'��ð�','2022/06/12',0)
insert into Private_Chat_Record values(1003,999,'��ð�','2022/06/11',0 )
insert into Private_Chat_Record values(1002,1000,'��ð�','2022/06/11',0)

select * from Private_Chat_Record


create table shopping_trolley --���ﳵ��
(
	shopping_Id int identity(999,1)primary key,   --���ﳵ���
	shopping_picture varchar(200),				  --��ƷͼƬ
	shopping_ProId int not null,				  --��Ʒ���
	shopping_ProName varchar(50)not null,		  --��Ʒ����
	"Type_ID" int foreign key references Used_Goods_Type("Type_ID"), --��Ʒ����
	shopping_ProPrice money not null				--��Ʒ�۸�
	
) 

 insert into shopping_trolley values('C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\R-C (1).jfif',1003,'Ь��',1003,40)
 insert into shopping_trolley values('C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\534b292ecf47b4de.jpg',1019,'����LI-NING����',1004,130)
 insert into shopping_trolley values('C:\Users\yw\Desktop\QQ202204290955_OCR\�׻�\image\R-C (5).jfif',1013,'Ь��',1003,60)
