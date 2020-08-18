
-- 购物车类型
CREATE TYPE shop_cart_type AS TABLE(
    good_id int NOT NULL,   --商品编号
    quantity int NOT NULL   --数量
)
GO





-- 创建销售存储过程
CREATE PROC sale(
    @stor_id int = 1,   --门店编号
    @shopcart shop_cart_type READONLY --购物车
)
AS
DECLARE @good_id int --商品编号
DECLARE @quantity int   --商品数量
DECLARE @kucun int  --总库存
DECLARE @batch_id int   --进货批次号
DECLARE @surplus int   --该批次中的库存
DECLARE @order_num int   --订单号
DECLARE @ordergood_id int   --给订单里面的每一种商品一个编号

BEGIN TRAN tran1    --开启事务
INSERT INTO orders(stor_id,order_date) VALUES(@stor_id,GETDATE());  --创建订单号
SET @order_num = SCOPE_IDENTITY()  --自增生成的订单号
DECLARE shopcart_cur CURSOR FOR SELECT * FROM @shopcart;    --定义购物车游标
OPEN shopcart_cur

FETCH NEXT FROM shopcart_cur INTO @good_id,@quantity    --读取购物车里面的第一个商品
WHILE @@FETCH_STATUS = 0
BEGIN

    --获取该商品的总库存
    SELECT @kucun=SUM(surplus) FROM batch_goods,batch,store_repositories
    WHERE batch_goods.batch_id=batch.batch_id
    AND batch.repo_id=store_repositories.repo_id
    AND store_repositories.stor_id=@stor_id
    AND good_id=@good_id;

    --库存不充足，创建订单失败
    IF @kucun<@quantity
    BEGIN
        PRINT '本次购物车中的某些商品库存不充足，创建订单失败'
        RETURN
    END

    --库存充足，继续创建订单
    ELSE
    BEGIN

    -- 创建一条订单详情记录
        INSERT INTO ordergoods(order_num,good_id,ordergood_quantity) VALUES(@order_num,@good_id,@quantity)
        SET @ordergood_id = SCOPE_IDENTITY()    -- 自增生成的订单商品号

    -- 消耗库存
        DECLARE batchgood_cur CURSOR   --该仓库里没卖完的所有批次的指定商品集合游标
        FOR
        SELECT batch_goods.batch_id,surplus FROM batch_goods,batch,store_repositories
        WHERE batch_goods.batch_id=batch.batch_id
        AND batch.repo_id=store_repositories.repo_id
        AND store_repositories.stor_id=@stor_id
        AND good_id=@good_id
        AND batch_goods.surplus>0
        ORDER BY surplus;

        OPEN batchgood_cur
        FETCH NEXT FROM batchgood_cur INTO @batch_id,@surplus
        WHILE @@FETCH_STATUS = 0
        BEGIN
            IF @quantity>=@surplus  --可以把这批货消耗完
            BEGIN
                UPDATE batch_goods SET surplus=0    --清空本批次中该商品剩余库存
                WHERE CURRENT OF batchgood_cur  --利用游标定位当前行
                SET @quantity=@quantity-@surplus    --更新还需要的数量
            END
            ELSE
            BEGIN   --这批货消耗不完
                UPDATE batch_goods SET surplus=@surplus-@quantity    --更新本批次中该商品剩余库存
                WHERE CURRENT OF batchgood_cur
                BREAK   --此时本商品所需要的库存已经满足，不再消耗后面的库存
            END
            INSERT INTO ordergood_batch(ordergood_id,batch_id) VALUES(@ordergood_id,@batch_id)
            FETCH NEXT FROM batchgood_cur INTO @batch_id,@surplus   --读取下一个批次的记录
        END
        CLOSE batchgood_cur 
        DEALLOCATE batchgood_cur
    END

    --读取购物车里面的下一个商品
    FETCH NEXT FROM shopcart_cur INTO @good_id,@quantity

END
CLOSE shopcart_cur
DEALLOCATE shopcart_cur
COMMIT TRAN tran1
PRINT '订单创建成功'











DECLARE @@shopcart AS shop_cart_type    --定义购物车表类型变量

-- 赋值
INSERT INTO @@shopcart SELECT 10001,3
UNION ALL SELECT 10002,2

EXEC sale @stor_id=10001,@shopcart=@@shopcart

