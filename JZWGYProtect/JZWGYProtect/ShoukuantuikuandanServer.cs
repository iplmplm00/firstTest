using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingdee.BOS.Core.DynamicForm.PlugIn;
using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
using Kingdee.BOS.Orm.DataEntity;
using Kingdee.BOS.ServiceHelper;
using Kingdee.BOS.App.Data;
using Kingdee.BOS;
using Kingdee.BOS.Core.Bill.PlugIn;
using Kingdee.BOS.Core.Bill.PlugIn.Args;
using Kingdee.BOS.Core.Metadata;
using Kingdee.BOS.Core.DynamicForm;
using Kingdee.BOS.Core.DynamicForm.PlugIn.ControlModel;
using Kingdee.BOS.Core.Metadata.EntityElement;
using System.Threading;

namespace JZWGYProtect
{
  public  class ShoukuantuikuandanServer : AbstractBillPlugIn

    {
        [Description("shou表单插件")]
        [Kingdee.BOS.Util.HotUpdate]
        public override void AfterBindData(EventArgs e)
        {
            base.AfterBindData(e);
            if (this.View.OpenParameter.Status.Equals(OperationStatus.EDIT))
            {
                string ss = this.View.Model.GetValue("FCOSTID").ToString();
                this.View.ShowMessage(ss);
                //给备注和备注1,赋值
                //string ss=   this.View.Model.GetValue("FBILLTYPEID").ToString();
                //   if (ss== "Kingdee.BOS.Orm.DataEntity.DynamicObject")
                //   {
                //       this.View.Model.SetValue("FPURPOSEID", "SFKYT02_SYS");

                //   }
                //   this.View.UpdateView("FPURPOSEID");

                //   this.View.ShowMessage(ss);
            }
            else {
                this.View.ShowMessage("111111111111111111111111111111");
            }

     
        }
    }
}
