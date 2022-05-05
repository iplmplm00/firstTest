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
    [Description("网销部签单明细表单插件")]
    [Kingdee.BOS.Util.HotUpdate]


    public class ZZTC : AbstractBillPlugIn
    {
        public override void BarItemClick(BarItemClickEventArgs e)
        {
            base.BarItemClick(e);

            if (e.BarItemKey.Equals("JZH_tbTest"))
            {
                ZZTCMXBIAO();//整装提成明细表
                ZZTCDYHZBIAO();//整装提成单源汇总表
                ZZTCRYHZBIAO();//整装提成人员汇总表
             
            }
          
           
        }
     

        //整装提成明细表
        public void ZZTCMXBIAO()
        {

            DynamicObject zu = this.View.Model.GetValue("FNAME") as DynamicObject;
            long zus = zu == null ? 0 : Convert.ToInt32(zu["id"]); //组织机构

            Object kehus = this.View.Model.GetValue("FHETONGHAO");//客户编码
  
            Object yuangongs = this.View.Model.GetValue("FEMPNOO");//客户名称
            DynamicObjectCollection ZZTCMX = DBUtils.ExecuteDynamicObject(Context, string.Format(@"/*dialect*/ exec ZZTC '{0}','{1}','{2}'",zus, yuangongs,kehus));//查询全部信息   
            

            if (ZZTCMX.Count > 0)
            {
                this.View.Model.DeleteEntryData("FZZTCMXEntity");//删除整装明细单据体
                for (int i = 0; i < ZZTCMX.Count; i++)
                {
                    this.View.Model.CreateNewEntryRow("FZZTCMXEntity");//新增整装明细单据体行数
                    this.View.Model.SetValue("FSBORGID", ZZTCMX[i][1], i);//分公司
                    this.View.InvokeFieldUpdateService("FSBORGID", i);
                    this.View.Model.SetValue("FFNUMBER", ZZTCMX[i][3], i);//合同号
                    this.View.InvokeFieldUpdateService("FFNUMBER", i);
                    this.View.Model.SetValue("FGCDZ", ZZTCMX[i][4], i);//工程地址
                    this.View.InvokeFieldUpdateService("FGCDZ", i);
                    this.View.Model.SetValue("FCJZCE", ZZTCMX[i][5], i);//成交主材额
                    this.View.InvokeFieldUpdateService("FCJZCE", i);
                    this.View.Model.SetValue("FGANWEI", ZZTCMX[i][6], i);//岗位
                    this.View.InvokeFieldUpdateService("FGANWEI", i);
                    this.View.Model.SetValue("FFEMPNAME", ZZTCMX[i][8], i);//员工姓名
                    this.View.InvokeFieldUpdateService("FFEMPNAME", i);
                    this.View.Model.SetValue("FFEMPNO", ZZTCMX[i][7], i);//员工编码
                    this.View.InvokeFieldUpdateService("FFEMPNO", i);
                    this.View.Model.SetValue("FPERCENT", ZZTCMX[i][9], i);//单源比例
                    this.View.InvokeFieldUpdateService("FPERCENT", i);
                    this.View.Model.SetValue("FKHJS", ZZTCMX[i][10], i);//考核基数
                    this.View.InvokeFieldUpdateService("FKHJS", i);
                    this.View.Model.SetValue("FTCBL", ZZTCMX[i][11], i);//提成比例
                    this.View.InvokeFieldUpdateService("FTCBL", i);
                    this.View.Model.SetValue("FTCMONEY", ZZTCMX[i][14], i);//提成金额
                    this.View.InvokeFieldUpdateService("FTCMONEY", i);
                    this.View.Model.SetValue("FLDTJ", ZZTCMX[i][12], i);//来单途径
                    this.View.InvokeFieldUpdateService("FLDTJ", i);
                    this.View.Model.SetValue("FFZHIDU", ZZTCMX[i][13], i);//制度
                    this.View.InvokeFieldUpdateService("FFZHIDU", i);

                }

                this.View.UpdateView("FZZTCMXEntity");
            }
            else
            {
                this.View.Model.DeleteEntryData("FZZTCMXEntity");////删除整装明细单据体
                this.View.UpdateView("FZZTCMXEntity");//更新同步整装提成明细单据体

            }

        }
        //整装提成单源汇总表
        public void ZZTCDYHZBIAO()
        {
            DynamicObjectCollection ZZTCRYHZ = DBUtils.ExecuteDynamicObject(Context, string.Format(@"/*dialect*/  SELECT* FROM DYHZ UNION ALL SELECT '','','汇总',SUM(HUIZHONG),'','' FROM  DYHZ"));//查询全部信息   
            if (ZZTCRYHZ.Count > 0)
            {
                this.View.Model.DeleteEntryData("FZZTCHZEntity");//删除整装单源汇总单据体
                for (int i = 0; i < ZZTCRYHZ.Count; i++)
                {
                    this.View.Model.CreateNewEntryRow("FZZTCHZEntity");//新增整装单源汇总单据体行数
                    this.View.Model.SetValue("FSBORGIDHZ", ZZTCRYHZ[i][0], i); ;//分公司
                    this.View.InvokeFieldUpdateService("FSBORGIDHZ", i);
                    this.View.Model.SetValue("FFNUMBERHZ", ZZTCRYHZ[i][1], i); ;//合同号
                    this.View.InvokeFieldUpdateService("FFNUMBERHZ", i);
                    this.View.Model.SetValue("FGCDZHZ", ZZTCRYHZ[i][2], i); ;//工程地址
                    this.View.InvokeFieldUpdateService("FGCDZHZ", i);
                    this.View.Model.SetValue("FTCMONEYHZ", ZZTCRYHZ[i][3], i); ;//提成金额
                    this.View.InvokeFieldUpdateService("FTCMONEYHZ", i);
                    this.View.Model.SetValue("FLDTJHZ", ZZTCRYHZ[i][4], i); ;//来单途径
                    this.View.InvokeFieldUpdateService("FLDTJHZ", i);
                    this.View.Model.SetValue("FFZHIDUHZ", ZZTCRYHZ[i][5], i);//制度
                    this.View.InvokeFieldUpdateService("FFZHIDUHZ", i);

                }
                this.View.UpdateView("FZZTCHZEntity");
            }
            else
            {
                this.View.Model.DeleteEntryData("FZZTCHZEntity");////删除整装单源汇总单据体
                this.View.UpdateView("FZZTCHZEntity");//更新同步整装单源汇总单据体
            }
        }
        //整装人员汇总表

        public void ZZTCRYHZBIAO()
        {

            DynamicObjectCollection ZZTCRYHZBIAO = DBUtils.ExecuteDynamicObject(Context, string.Format(@"/*dialect*/
 SELECT* FROM ZRYHZ   WHERE RYHUIZHONG!=0"));

            if (ZZTCRYHZBIAO.Count > 0)
            {
                this.View.Model.DeleteEntryData("FZZTCRYHZEntity");//删除人员汇总单据体
                for (int i = 0; i < ZZTCRYHZBIAO.Count; i++)
                {
                    this.View.Model.CreateNewEntryRow("FZZTCRYHZEntity");//新增人员汇总单据体行数
                    this.View.Model.SetValue("FSBORGIDRYHZ", ZZTCRYHZBIAO[i][0], i);//分公司
                    this.View.Model.SetValue("FFEMPNAMERYHZ", ZZTCRYHZBIAO[i][1], i);//员工姓名
                    this.View.InvokeFieldUpdateService("FFEMPNAMERYHZ", i);
                    this.View.Model.SetValue("FFEMPNORYHZ", ZZTCRYHZBIAO[i][2], i);//员工编码
                    this.View.InvokeFieldUpdateService("FFEMPNORYHZ", i);
                    this.View.Model.SetValue("FTCMONEYHZRYHZ", ZZTCRYHZBIAO[i][3], i);//提成金额汇总
                    this.View.InvokeFieldUpdateService("FTCMONEYHZRYHZ", i);
                    this.View.Model.SetValue("FYHZH", ZZTCRYHZBIAO[i][5], i);//银行账号
                    this.View.InvokeFieldUpdateService("FYHZH", i);
                    this.View.Model.SetValue("FOPENBANKNAME", ZZTCRYHZBIAO[i][6], i);//开户银行
                    this.View.InvokeFieldUpdateService("FOPENBANKNAME", i);
                    this.View.Model.SetValue("FFZHIDURYHZ", ZZTCRYHZBIAO[i][4], i);//制度
                    this.View.InvokeFieldUpdateService("FFZHIDURYHZ", i);
                }
                this.View.UpdateView("FZZTCRYHZEntity");//更新值
            }
            else
            {
                this.View.Model.DeleteEntryData("FZZTCRYHZEntity");//删除人员汇总单据体
                this.View.UpdateView("FZZTCRYHZEntity");//更新同步整装人员汇总单据体
            }
        }




    }
}

