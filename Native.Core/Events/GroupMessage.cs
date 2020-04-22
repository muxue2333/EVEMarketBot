﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Sdk.Cqp.Interface;
using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Model;
using Native.Sdk.Cqp;
using Native.Core.Domain;

namespace Nekonya.Events
{
    public class GroupMessage : IGroupMessage
    {
        void IGroupMessage.GroupMessage(object sender, CQGroupMessageEventArgs e)
        {
            string msg = e.Message.Text;
            if (msg.ToLower().StartsWith(".jita ")) //吉他市场查询
            {
                Jitas.Jitas.Instance.QueryFromGroup(e);
                e.Handler = true; //大概是中止pipeline的用途？
                return;
            }

            if(msg.ToLower().StartsWith(".bind ")) //绑定俗称词库
            {
                var result = Jitas.Jitas.Instance.BindCommonlyName(msg, e.FromQQ);
                if (!string.IsNullOrEmpty(result))
                {
                    var at_msg = e.FromQQ.CQCode_At();
                    e.FromGroup.SendGroupMessage(at_msg, result);
                }
                e.Handler = true;
                return;
            }

            if (msg.ToLower().StartsWith(".unbind ")) //移除俗称词库
            {
                var result = Jitas.Jitas.Instance.RemoveCommonlyName(msg, e.FromQQ);
                if (!string.IsNullOrEmpty(result))
                {
                    var at_msg = e.FromQQ.CQCode_At();
                    e.FromGroup.SendGroupMessage(at_msg, result);
                }
                e.Handler = true;
                return;
            }

            if (msg.ToLower().StartsWith(".addadmin ")) //移除俗称词库
            {
                var result = Jitas.Jitas.Instance.AddAdmin(msg, e.FromQQ);
                if (!string.IsNullOrEmpty(result))
                {
                    var at_msg = e.FromQQ.CQCode_At();
                    e.FromGroup.SendGroupMessage(at_msg, result);
                }
                e.Handler = true;
                return;
            }
        }
    }
}
