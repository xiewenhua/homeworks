-- 创建入库商品类型。因为入库的可能是多种商品, SQL Server 支持Table类型（MySQL是不支持的）
CREATE TYPE goodlisttype AS TABLE(
    good_id int NOT NULL,   -- 商品编号
    purchase_price float NOT NULL,  -- 进货价格
    purchase_quantity int NOT NULL  --数量
)
GO

-- 创建入库单的存储过程
CREATE PROC ruku(
    @repo_id int,   -- 仓库编号
    @vend_id int,   -- 供应商编号 
    @goodlist goodlisttype READONLY -- 进货详情
)
AS
DECLARE @batch_id int   -- 进货批次号
BEGIN TRANSACTION

-- 生成批次号
INSERT INTO batch
    (repo_id, batch_date, vend_id)
VALUES
    (@repo_id, GETDATE(), @vend_id);
SET @batch_id = SCOPE_IDENTITY();   -- 获取自增ID

-- 插入进货清单
INSERT INTO batch_goods
    (batch_id, good_id, purchase_price, purchase_quantity, surplus)
SELECT @batch_id, good_id, purchase_price, purchase_quantity, purchase_quantity
FROM @goodlist

COMMIT TRANSACTION
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
EXEC ruku @repo_id= 10001,@vend_id=1001,@goodlist=@@goodlist
GO
