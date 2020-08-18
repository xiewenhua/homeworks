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
