--易货数据库
create database  YihuoDB

use YihuoDB

if OBJECT_ID('Admin_Info') is not null 
	drop table Admin_Info
go
create table Admin_Info--管理员表
(
	Admin_ID int primary key ,--管理员ID 
	Admin_Login varchar(20) unique ,--登录账号
	Admin_Pass Varchar(20) not null ,--登录密码
	Admin_Status bit check(Admin_Status = 1 or Admin_Status = 0 or Admin_Status = 2) --管理员状态
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
create table User_Info--用户信息表
(
	"User_ID" int identity(999,1) primary key ,--用户ID
	User_Login varchar(20) unique , --登录账号
	User_Pass varchar(20) not null, --登录密码
	User_Phone varchar(11) not null , --手机号
	"User_Name" varchar(50) not null , --用户昵称
	User_Status bit check(User_Status = 0 or User_Status = 1) --用户状态
)
go

--insert into User_Info values(@User_ID,@User_Login,@User_Pass,@User_Phone,@User_Name,@User_Status)

insert into User_Info values('2045896354','ye78965','14578965874','小亮',0),
							('6987456321','yu478965','14789654789','小米',0),
							('5896247896','ling45678','1479632547','小明',0),
							('3698745896','wang2589','15896547896','小红',0),
							('5896321474','Lv478965','14789635478','小美',0),
							('9685478965','xa789658','15896347889','刘伟',0),
							('1478965785','wan78965','18963578956','陈成',0)
							
go

insert into User_Info values('admin','sy6541230','1547896352','学哩',0)

update User_Info set User_Pass='654123' where User_Login='admin'

select * from User_Info

select * from User_Info  where User_Login ='admin'


select * from User_Info where User_Login = '2045896354' and User_Pass= 'ye78965'

if OBJECT_ID('Used_Goods_Type') is not null 
	drop table  Used_Goods_Type
go

create table Used_Goods_Type --商品类型表
(
	"Type_ID" int primary key identity(1000,1) ,--类型编号
	"Type_Name" varchar(50) not null ,--类型名称
	Type_Description varchar(100) ,--类型描述
)
go

insert into Used_Goods_Type values('居家日用','家中使用'),
								  ('电子商品','电子产品'),
								  ('闲置书籍','文化书籍'),
								  ('运动户外','运动装备'),
								  ('箱包','箱子背包'),
								  ('动漫/周边','动漫及手办'),
								  ('玩具乐器','贝多芬')
go

select *from Used_Goods_Type
select * from User_Info

if OBJECT_ID('Used_Goods_Info') is not null 
	drop table  Used_Goods_Info
go

create table Used_Goods_Info --二手商品表
(
	Goods_ID int identity(1000,1) primary key, --商品编号
	Goods_Name varchar(50) not null ,--商品名称
	Goods_Type_ID int foreign key references Used_Goods_Type("Type_ID"),--商品类型
	Goods_Price money not null , --商品价格
	Goods_Seller int foreign key references User_Info("User_ID") ,--上架者
	Goods_Status bit check(Goods_Status = 0 or Goods_Status = 1) , --商品状态（在售/下架）	
	Goods_Description varchar(100) ,--商品描述
	Goods_Date datetime default current_timestamp, --上架日期
	Goods_picture varchar(200)
	
)
go                                                                                                                 

select Goods_ID,Goods_Name,Goods_Type_ID,Goods_Price,Goods_Seller,Goods_Status,Goods_Description, Goods_Date ,Goods_picture from Used_Goods_Info where Goods_Type_ID=1001
select * from Used_Goods_Info where Goods_ID=1000
select * from Used_Goods_Info where Goods_ID=1001

 
insert into Used_Goods_Info values('热得快',1000,15,999,0,'热得快烧水器电热棒烧水棒大功率浴盆浴桶专用不锈钢','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\R-C.jfif'),
								  ('卫生间纸巾盒',1000,10,1000,0,'卫生间纸巾盒厕所免打孔纸巾架浴室置物架厕纸盒壁','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\O1CN01jXuYXG1XbTk6HIBL3_!!0-item_pic.gif'),
								  ('卫生间置物架',1000,20,1000,0,'卫生间置物架 免打孔太空铝三角','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\R-B.jfif'),              --居家日用信息
								  ('鞋架',1000,40,1000,0,'鞋架简易拖鞋简约宿舍寝室置物鞋架','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\R-C (1).jfif')
go

insert into Used_Goods_Info values('罗技G502',1001,300,1001,0,'HERO主宰者有线鼠标游戏鼠标HERO引擎RGB鼠标电竞鼠标 25600DPI','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\20190715170328_3592.jpg'),
								  ('漫步者EDIFIER',1001,500,1001,0,'蓝牙无线主动降噪头戴式耳机','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\R-C (2).jfif'),
								  ('腹灵S198Pro',1001,400,1001,0,'BOX白轴机械键盘','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\R-C (3).jfif'),              --电子商品
								  ('华为WATCHGT3',1001,1200,1004,0,'华为手表运动智能手表血氧检测46mm黑色','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\R-C (4).jfif')
go

insert into Used_Goods_Info values('时间简史',1002,20,1004,0,'霍金三部曲之一，时间简史','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\012c4e5bfe6547a80121ab5df7a66a.jpg@1280w_1l_2o_100sh.jpg'),
								  ('活着',1002,30,1005,0,'活着（精装版，余华代表作）','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\view.jfif'),
								  ('平凡的世界',1002,80,1004,0,'平凡的世界：全三册（全新2021版，茅盾文学奖获奖作品，激励青年的不朽经典）','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\v2-245f832654ac6ec8a4f4481439eaf6b0_720w.jpg'),              
								  ('人世间',1002,50,1002,0,'第十届茅盾文学奖获奖作品：人世间（套装共3册）','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\bd1671f242588819.jpg') --闲置书籍
go

insert into Used_Goods_Info values('李宁LI-NING',1003,60,1002,0,'计数跳绳运动钢丝神室内运动长绳','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\f35bd687e16568c8.jpg'),
								  ('多德士',1003,60,1002,0,'多德士仰卧起坐健身器材仰卧板家用辅助器腹肌运动健身','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\R-C (5).jfif'),
								  ('KONKA康佳筋膜枪',1003,80,1002,0,'筋膜枪按摩器电动迷你肌肉放松器深层高频震动颈膜枪','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\R-C.png'),              
								  ('爱倍健',1003,130,1003,0,'爱倍健沙袋散打立式家用成人拳击沙包不倒翁儿童跆拳道训练器材','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\285376452794558047402460_x.jpg') --运动户外
go

insert into Used_Goods_Info values('地平线8号',1004,200,999,0,'行李箱男拉杆箱旅行密码箱24英寸','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\de5a-kmvwsvy6694030.jpg'),
								  ('卡蜜卡单肩男手提旅行包',1004,150,1004,0,'卡蜜卡单肩出差包男手提旅行包牛津布','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\5cdd7661N0eadbb05.jpg'),
								  ('户外休闲登山包双肩包',1004,80,1004,0,'户外休闲登山包双肩包60L男女大容量背包运动旅行包','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\_-1x-1.jpg'),              
								  ('李宁LI-NING腰包',1004,130,1002,0,'李宁 LI-NING 跑步腰包男女通用多功能手机包拉链防泼水','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\534b292ecf47b4de.jpg') --箱包
go

insert into Used_Goods_Info values('抱枕',1005,50,1003,0,'抱枕长条夹腿二次元原神等身魈达达利亚钟离胡桃甘雨动漫周边','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\6e3b5c7b5e98be7f.jpg'),
								  ('初音未来',1005,10,1003,0,'初音未来周边3D立体大海报二次元vocaloid/V家 动漫学生宿舍墙纸装饰','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\c7e23fc28374a48d.jpg'),
								  ('手办公仔口袋妖怪',1005,300,1002,0,'动漫神奇宝贝宠物小精灵皮卡丘手办公仔口袋妖怪玩具模型摆件 ','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\5aeac352N4cd65844.jpg'),              
								  ('樱桃键盘',1005,150,1002,0,'樱桃（CHERRY）MX 1.0 宝可梦 POKEMON 皮卡丘主题','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\572b7c27d343921c.jpg') --动漫周边
go

insert into Used_Goods_Info values('竹笛',1006,60,1005,0,'竹笛专业初学精制横笛苦竹竹笛演奏笛乐器零基础学生笛','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\60cdf01f11142f14.png'),
								  ('名森贝斯',1006,500,1002,0,'名森（Minsine）FD贝斯/黑色经典四弦电贝司初学者入门BASS','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\778757feb9eac503.jpg'),
								  ('雅马哈',1006,650,1002,0,'印尼进口民谣吉他 雅马哈吉他 初学入门41英寸男女木吉它jita乐器','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\cacea621ad015d3d.jpg'),              
								  ('奇宝居小提琴',1006,330,1002,0,'小提琴初学者成人儿童实木入门款 手工调音学生新手考级练习提琴 ','2022/06/13','C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\5a0c0819Nd66d2f13.jpg') --玩具乐器
go


select *from Used_Goods_Type
select * from User_Info
select* from Used_Goods_Info

if OBJECT_ID('UG_Concerns') is not null 
	drop table  UG_Concerns
go

create table UG_Concerns --游客关注表
(
	UG_ID int primary key identity(999,1),--关注编号
	Goods_ID  int foreign key references Used_Goods_Info(Goods_ID) , --关注商品编号
	"User_ID" int foreign key references User_Info("User_ID") ,--关注游客编号
	
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
create table Trade_Record --交易记录表
(
	Trade_ID int primary key identity(999,1) ,--交易编号
	Trade_Seller int foreign key references User_Info("User_ID") ,--外键卖家编号
	Trade_Buyers int foreign key references User_Info("User_ID") ,--外键买家编号
	Trade_Date datetime default current_timestamp ,--交易日期
	Trade_Description varchar(100) ,--交易备注
	Goods_ID int foreign key references Used_Goods_Info(Goods_ID),--交易商品
	Trade_Price money not null, --交易金额
)
go

insert into Trade_Record values(1004,1001,'2022/06/14','改价钱',1008,20)
insert into Trade_Record values(999,1003,'2022/06/14','无',1000,15)
insert into Trade_Record values(1000,1002,'2022/06/14','无',1002,20)

select * from Trade_Record 

select *from Trade_Record

if OBJECT_ID('Trade_Conduct') is not null 
	drop table  Trade_Conduct
go

create table Trade_Conduct --交易进行表
(
	Conduct_ID int identity(999,1) primary key, --进行编号
	Trade_Seller int foreign key references User_Info("User_ID") ,--外键卖家编号
	Trade_Buyers int foreign key references User_Info("User_ID") ,--外键买家编号
	Goods_ID int foreign key references Used_Goods_Info(Goods_ID),--交易商品
	Trade_Price money not null, --交易金额
	Trade_Finish bit check(Trade_Finish = 0 or Trade_Finish = 1),--是否完成
	Trade_Abnormal bit check(Trade_Abnormal = 0 or Trade_Abnormal = 1)--是否异常
	
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

create table Browse_Record --浏览记录表
(
	Browse_ID int identity(999,1) primary key, --记录编号
	Goods_ID int foreign key references Used_Goods_Info(Goods_ID),--关注商品编号
	"User_ID" int foreign key references User_Info("User_ID") --关注游客编号
)
go
insert into Browse_Record values(1017,1004),
							   (1024,1005),
							    (1025,1002)
go


if OBJECT_ID('Used_Goods_Sold') is not null 
	drop table  Used_Goods_Sold
go

create table Used_Goods_Sold --已售商品表
(
	Sold_ID int identity(999,1) primary key ,--已售编号
	Sold_Goods_ID int foreign key references Used_Goods_Info(Goods_ID) ,--已售商品编号
	Sold_Date datetime default current_timestamp --下架时间
	
)
go

insert into Used_Goods_Sold values(1008,'2022/06/15'),
								  (1000,'2022/06/16'),
								  (1002,'2022/06/18')
go

if OBJECT_ID('Private_Chat_Record') is not null
	drop table Private_Chat_Record
go

create table Private_Chat_Record --私聊记录表
(
	Record_ID int identity(1000,1) primary key , --私聊记录编号
	"User_FA" int foreign key references User_Info("User_ID") ,--发送者
	"User_SO" int foreign key references User_Info("User_ID") ,--接收者
	Chat_HIstory text ,--消息内容
	P_Time datetime ,--发送时间
	P_Status bit check(P_Status = 0 or P_Status = 1) --接收状态
)
go

insert into Private_Chat_Record values(1001,1004,'你好啊','2022/06/12',0)
insert into Private_Chat_Record values(1003,999,'你好啊','2022/06/11',0 )
insert into Private_Chat_Record values(1002,1000,'你好啊','2022/06/11',0)

select * from Private_Chat_Record


create table shopping_trolley --购物车表
(
	shopping_Id int identity(999,1)primary key,   --购物车编号
	shopping_picture varchar(200),				  --商品图片
	shopping_ProId int not null,				  --商品编号
	shopping_ProName varchar(50)not null,		  --商品名称
	"Type_ID" int foreign key references Used_Goods_Type("Type_ID"), --商品类型
	shopping_ProPrice money not null				--商品价格
	
) 

 insert into shopping_trolley values('C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\R-C (1).jfif',1003,'鞋架',1003,40)
 insert into shopping_trolley values('C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\534b292ecf47b4de.jpg',1019,'李宁LI-NING腰包',1004,130)
 insert into shopping_trolley values('C:\Users\yw\Desktop\QQ202204290955_OCR\易货\image\R-C (5).jfif',1013,'鞋架',1003,60)
