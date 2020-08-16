-- 仓库表
CREATE TABLE repositories
(
    repo_id int IDENTITY(10000,1) NOT NULL,
    repo_address nchar(50) NOT NULL,
    PRIMARY KEY(repo_id)
)
GO

-- 门店表
CREATE TABLE stores
(
    stor_id int IDENTITY(10000,1) NOT NULL,
    stor_address nchar(50) NOT NULL,
    PRIMARY KEY(stor_id)
)
GO

-- 商品表
CREATE TABLE goods
(
    good_id int IDENTITY(10000,1) NOT NULL,
    good_name nchar(50) NOT NULL,
    PRIMARY KEY(good_id)
)
GO

-- 商品快照表
-- 应该添加触发器，当商品表发生UPDATE或者INSERT前，往商品快照表写入记录
CREATE TABLE snapshots
(
    snapshot_id int IDENTITY(10000,1) NOT NULL,
    snapshot_time datetime NOT NULL,
    good_id int NOT NULL,
    good_name nchar(50) NOT NULL,
    PRIMARY KEY(snapshot_id)
)
GO
ALTER TABLE snapshots ADD FOREIGN KEY(good_id) REFERENCES goods(good_id)
GO

-- 编写触发器，当添加新商品或者更新已有商品属性时，生成商品快照
CREATE TRIGGER Generated_Snapshot
ON goods
FOR UPDATE,INSERT AS
INSERT INTO snapshots(snapshot_time,good_id,good_name)
SELECT GETDATE(),good_id,good_name
FROM inserted
GO

-- 供应商表
CREATE TABLE vendors
(
    vend_id int IDENTITY(10000,1) NOT NULL,
    PRIMARY KEY(vend_id)
)
GO

-- 每个门店有一个仓库
CREATE TABLE store_repositories
(
    stor_id int NOT NULL,
    repo_id int NOT NULL,
    PRIMARY KEY(stor_id, repo_id)
)
GO
ALTER TABLE  store_repositories ADD FOREIGN KEY(stor_id) REFERENCES stores(stor_id)
GO
ALTER TABLE store_repositories ADD FOREIGN KEY(repo_id) REFERENCES repositories(repo_id)
GO

-- 一个仓库可以保存多种商品,stock是库存数量
CREATE TABLE repo_goods
(
    repo_id int NOT NULL,
    good_id int NOT NULL,
    stock int NOT NULL,
    PRIMARY KEY(repo_id, good_id)
)
GO
ALTER TABLE repo_goods ADD FOREIGN KEY(repo_id) REFERENCES repositories(repo_id)
GO
ALTER TABLE repo_goods ADD FOREIGN KEY(good_id) REFERENCES goods(good_id)
GO

-- 仓库进货批次号 批次号-仓库号-进货时间-供应商号 batch_id:进货批次号
CREATE TABLE batch
(
    batch_id int IDENTITY(10000,1) NOT NULL,
    repo_id int NOT NULL,
    batch_date datetime NOT NULL,
    vend_id int NOT NULL,
    PRIMARY KEY(batch_id)
)
GO
ALTER TABLE batch ADD FOREIGN KEY(repo_id) REFERENCES repositories(repo_id)
GO
ALTER TABLE batch ADD FOREIGN KEY(vend_id) REFERENCES vendors(vend_id)
GO

-- 每一次进货可以进多种商品    添加本批次该商品剩余数量（surplus）
CREATE TABLE batch_goods
(
    batch_id int NOT NULL,
    good_id int NOT NULL,
    purchase_price float NOT NULL,
    purchase_quantity int NOT NULL,
    surplus int NOT NULL,
    PRIMARY KEY(batch_id, good_id)
)
GO
ALTER TABLE batch_goods ADD FOREIGN KEY(good_id) REFERENCES goods(good_id)
GO
ALTER TABLE batch_goods ADD FOREIGN KEY(batch_id) REFERENCES batch(batch_id)
GO

-- 门店可以销售多种商品
CREATE TABLE storegood
(
    storegood_id int IDENTITY(10000,1) NOT NULL,
    stor_id int NOT NULL,
    good_id int NOT NULL,
    PRIMARY KEY(storegood_id)
)
GO
ALTER TABLE storegood ADD FOREIGN KEY(stor_id) REFERENCES stores(stor_id)
GO
ALTER TABLE storegood ADD FOREIGN KEY(good_id) REFERENCES goods(good_id)
GO

-- 每一种商品在同一个门店一个时刻只有一种售价
CREATE TABLE saleprices
(
    storegood_id int NOT NULL,
    sale_date datetime NOT NULL,
    sale_price float NOT NULL,
    PRIMARY KEY(storegood_id, sale_date)
)
GO
ALTER TABLE saleprices ADD FOREIGN KEY(storegood_id) REFERENCES storegood(storegood_id)
GO

-- 门店销售订单号   !!!加一个订单总金额（反范式） !!!不加了
CREATE TABLE orders
(
    order_num int IDENTITY(10000,1) NOT NULL,
    stor_id int NOT NULL,
    order_date datetime NOT NULL
    PRIMARY KEY(order_num)
)
GO
ALTER TABLE orders ADD FOREIGN KEY(stor_id) REFERENCES stores(stor_id)

-- 一个订单可以有多种商品
CREATE TABLE ordergoods
(
    ordergood_id int IDENTITY(10000,1) NOT NULL,
    order_num int NOT NULL,
    good_id int NOT NULL,
    ordergood_quantity int NOT NULL,
    PRIMARY KEY(ordergood_id)
)
GO
ALTER TABLE ordergoods ADD FOREIGN KEY(order_num) REFERENCES orders(order_num)
GO
ALTER TABLE ordergoods ADD FOREIGN KEY(good_id) REFERENCES goods(good_id)
GO

-- 收款方式
CREATE TABLE paytype
(
    paytype_id int IDENTITY(10000,1) NOT NULL,
    paytype_name nchar(50) NOT NULL,
    PRIMARY KEY(paytype_id)
)
GO

-- 订单的支付方式
CREATE TABLE orderpaytype
(
    order_num int NOT NULL,
    paytype_id int NOT NULL,
    PRIMARY KEY(order_num, paytype_id)
)
GO
ALTER TABLE orderpaytype ADD FOREIGN KEY(paytype_id) REFERENCES paytype(paytype_id)
GO
ALTER TABLE orderpaytype ADD FOREIGN KEY(order_num) REFERENCES orders(order_num)
GO

-- 销售订单的商品是哪些进货批次
CREATE TABLE ordergood_batch
(
    ordergood_id int NOT NULL,
    batch_id int NOT NULL,
    PRIMARY KEY(ordergood_id, batch_id)
)
GO
ALTER TABLE ordergood_batch ADD FOREIGN KEY(ordergood_id) REFERENCES ordergoods(ordergood_id)
GO
ALTER TABLE ordergood_batch ADD FOREIGN KEY(batch_id) REFERENCES batch(batch_id)
GO

-- =================插入示例数据，为后面演示需要用到====================

-- 为了测试存储过程，需要在相关表中插入几行记录
SET IDENTITY_INSERT repositories ON
GO
INSERT INTO repositories(repo_id, repo_address)
VALUES(10001, '西丽'),
    (10002, '荔枝世界')
GO
SET IDENTITY_INSERT repositories OFF
GO

SET IDENTITY_INSERT goods ON
GO
INSERT INTO goods(good_id, good_name)
VALUES(10001, '东芝固态TYC9099'),
    (10002, '三星固态YOU767'),
    (10003, '闪迪固态LSHHS7838')
GO
SET IDENTITY_INSERT goods OFF
GO

SET IDENTITY_INSERT vendors ON
GO
INSERT INTO vendors(vend_id)
VALUES(1001),
    (1002),
    (1003)
GO
SET IDENTITY_INSERT vendors OFF

SET IDENTITY_INSERT stores ON
INSERT INTO stores(stor_id,stor_address)
VALUES(10001,'广州门店'),
(10002,'深圳门店')
SET IDENTITY_INSERT stores OFF

-- =================这是第一次作业与第二次作业的分割线==================

-- 创建入库商品类型。因为入库的可能是多种商品, SQL Server 支持Table类型（MySQL是不支持的）
CREATE TYPE goodlisttype AS TABLE(
    good_id int NOT NULL,
    purchase_price float NOT NULL,
    purchase_quantity int NOT NULL
)
GO

-- 创建入库单的存储过程
CREATE PROC ruku(
    @batch_id int,
    @repo_id int,
    @batch_date datetime,
    @vend_id int,
    @goodlist goodlisttype  READONLY
)
AS
BEGIN
    BEGIN TRANSACTION
    SET IDENTITY_INSERT batch ON
    INSERT INTO
    batch(batch_id, repo_id, batch_date, vend_id)
    VALUES
        (@batch_id, @repo_id, @batch_date, @vend_id)
    SET IDENTITY_INSERT batch OFF
    INSERT INTO
    batch_goods
        (
        batch_id,
        good_id,
        purchase_price,
        purchase_quantity,
        surplus
        )
    SELECT
        @batch_id,
        good_id,
        purchase_price,
        purchase_quantity,
        good_id
    FROM
        @goodlist
    COMMIT TRANSACTION
END
GO

-- 使用存储过程分两步
-- 1、声明并赋值Table类型的变量，用于存放商品的编号、价格、数量
DECLARE @@goodlist AS goodlisttype
INSERT INTO @@goodlist
    SELECT 10001, 543, 365
UNION ALL
    SELECT 10002, 899, 500
UNION ALL
    SELECT 10003, 788, 1000
-- 2、执行存储过程
EXEC ruku @batch_id =10001,@repo_id= 10001,@batch_date='2020-07-03',@vend_id=1001,@goodlist=@@goodlist
GO

-- 成本低的优先出货(自动的)
-- 创建售货单的存储过程
-- 购物车类型
CREATE TYPE shop_cart_type AS TABLE(
    good_id int NOT NULL,
    quantity int NOT NULL
)
GO

CREATE PROC sale(
    @shop_cart shop_cart_type READONLY,
    @stor_id int
)
AS
DECLARE @order_id int,@good_id int,@quantity int;
DECLARE shopcart_cur CURSOR FOR SELECT * FROM @shop_cart;
-- 存放仓库中该商品的总数量
DECLARE @repo_q int;
BEGIN TRANSACTION
OPEN shopcart_cur;
FETCH NEXT FROM shopcart_cur INTO @goood_id,@quantity
WHILE @@FETCH_STATUS =0
BEGIN
    SELECT @repo_q = SUM(surplus) FROM batch_goods
    WHERE good_id=@good_id
    AND batch_id IN (
        SELECT batch_id FROM batch
        WHERE repo_id=(
            SELECT repo_id FROM store_repositories
            WHERE stor_id=@stor_id
        )
    )
END
INSERT INTO orders(stor_id,order_date) VALUES(@stor_id,GETDATE())
SET @order_id=SCOPE_IDENTITY()
INSERT INTO ordergoods(order_num,good_id,ordergood_quantity)
SELECT @order_id,good_id,quantity FROM @shop_cart
COMMIT TRANSACTION
GO

-- 使用销售存储过程
DECLARE @@shop_cart AS shop_cart_type
INSERT INTO @@shop_cart
SELECT 10001,10
EXEC sale @shop_cart=@@shop_cart,@stor_id=10001

