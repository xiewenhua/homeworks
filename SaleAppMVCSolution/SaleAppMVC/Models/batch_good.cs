//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SaleAppMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class batch_good
    {
        public int batch_id { get; set; }
        public int good_id { get; set; }
        public double purchase_price { get; set; }
        public int purchase_quantity { get; set; }
        public int surplus { get; set; }
    }
}