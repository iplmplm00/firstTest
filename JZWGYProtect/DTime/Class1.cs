using Kingdee.BOS.Core.Const;
using Kingdee.BOS.Core.DynamicForm.PlugIn;
using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
using Kingdee.BOS.Core.List.PlugIn;
using Kingdee.BOS.JSON;
using Kingdee.BOS.Util;
using System;
using System.ComponentModel;



[Description("自动刷新列表")] public class AutoRefreshListPlugIn : AbstractListPlugIn
{ private const string TestKey = "JZH_tbTest";
    public override void AfterBindData(EventArgs e)
    { base.AfterBindData(e); KeepAlive(); }
    public void KeepAlive() {
        JSONObject para = new JSONObject();
        para["key"] = TestKey; para["eventName"] = "CustomEvents";
        var data = new JSONObject(); para["data"] = data;
        data["refreshData"] = DateTime.Now; para["delay"] = "5000";
        this.View.AddAction(JSAction.FireCustomRequest, para);
    } public override void CustomEvents(CustomEventsArgs e) {
        base.CustomEvents(e); if (e.Key.EqualsIgnoreCase(TestKey)
            )
        {
            var data = JSONObject.Parse(e.EventArgs);
            if (data != null && data.ContainsKey("refreshData"))
            {
                this.View.Refresh();
            }
        }
    }
}
